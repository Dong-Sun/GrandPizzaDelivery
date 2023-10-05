using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class FieldOfViewPoliceCar : MonoBehaviour, IGetBool
{
	public float ViewRadius;	// �þ� ������ ����
	[Range(0,360)]
	public float ViewAngle; // �þ� ���� ũ��

	public LayerMask TargetMask;
	public LayerMask ObstacleMask;

	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();

	private Coroutine findTargetsWithDelayCoroutine;

	public float MeshResolution;
	public float EdgeDstThreshold;
	public int EdgeResolveIterations;

	public MeshFilter ViewMeshFilter;
	
	private Mesh viewMesh;

	private bool isFindPlayer = false;

	private void Start()
	{
		viewMesh = new Mesh();
		viewMesh.name = "View Mesh";
		ViewMeshFilter.mesh = viewMesh;

		findTargetsWithDelayCoroutine = StartCoroutine(FindTargetsWithDelay());
	}
	private void LateUpdate()
	{
		DrawFieldOfView();
	}
	private IEnumerator FindTargetsWithDelay()
	{
		while(true)
		{
			yield return Constant.OneTime;
			yield return Constant.OneTime;
			isFindPlayer = FindVisibleTargets();
		}
	}
	/// <summary>
	/// ��ǥ���� ã�� �Լ�.��ǥ���� TargetLayer�� �����Ǿ��ִ�.
	/// </summary>
	private bool FindVisibleTargets()
	{
		//visibleTargets.Clear();
		Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, ViewRadius, TargetMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;	// ��ǥ���� transform
			Vector3 dirToTarget = (target.position - transform.position).normalized;	// �ش� ��ü ���� ��ǥ���� �ִ� ����
			if (Vector3.Angle(transform.right, dirToTarget) < ViewAngle / 2)
			{
				// �ش� ��ü�� ��ǥ������ �Ÿ�
				float dstToTarget = Vector3.Distance(transform.position, target.position);
				
				// ��ü�� ���� ���̸� �߻����� �� ���ع� ���̾�(ObstacleMask)�� �޸� ������Ʈ�� ���� ��쿡�� visibleTargets�� �߰��� 
				//Debug.DrawRay(transform.position, dirToTarget * dstToTarget,Color.green);
				if (!Physics2D.Raycast (transform.position, dirToTarget, dstToTarget, ObstacleMask))
				{
					//visibleTargets.Add(target);
					return true;
				}
			}
		}
		return false;
	}
	/// <summary>
	/// FieldOfView�� �׷��ش�.
	/// </summary>
	private void DrawFieldOfView()
	{
		// ���� ����
		int stepCount = Mathf.RoundToInt(ViewAngle * MeshResolution);
		// ���� ������ ���� ������ ����
		float stepAngleSize = ViewAngle / stepCount;
		// �ﰢ���� ���� �������� ����
		List<Vector3> viewPoints = new List<Vector3>();
		ViewCastInfo oldViewCast = new ViewCastInfo();
		for (int i = 0; i < stepCount; i++)
		{
			// transform.eulerAngles.z�� ���� ���� �ٶ󺸴� ����. ViewAngle/2�� ���� �þ߰� �� �κ��� ǥ��.
			// �ű⼭���� stepAngleSize * i�� �����ָ� ���κк��� ������ �������� ������ ���� �� ����.
			float angle = -transform.eulerAngles.z - ViewAngle / 2 + stepAngleSize * i;
			//Debug.DrawLine(transform.position, transform.position + DirFromAngle(angle, true) * ViewRadius, Color.red);
			ViewCastInfo newViewCast = ViewCast(angle);

			if (i > 0)
			{
				bool edgeDstThresholdExceeded = Mathf.Abs(oldViewCast.Dst - newViewCast.Dst) > EdgeDstThreshold;
				if (oldViewCast.Hit != newViewCast.Hit || (oldViewCast.Hit && newViewCast.Hit && edgeDstThresholdExceeded))
				{
					// �𼭸� ��ġ�� ���� ������ �޾ƿ´�.
					EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
					// viewPoints�� ������ ��´�. �ﰢ���� ����µ� ���� ���̴�.
					if (edge.PointA != Vector3.zero)
					{
						viewPoints.Add(edge.PointA);
					}
					if (edge.PointB != Vector3.zero)
					{
						viewPoints.Add(edge.PointB);
					}
				}
			}

			viewPoints.Add(newViewCast.Point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 2) * 3];
		// vertices[0]�� �ﰢ������ ������.
		vertices[0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++)
		{
			// ���� �� ��ġ�� ����
			vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
			// �ﰢ�� ������ ����. �ﰢ���� ������ �����ϱ� ������ ����
			if (i < vertexCount - 2)
			{
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals();
	}
	/// <summary>
	/// �𼭸��� ã�� �Լ�
	/// </summary>
	/// <param name="minViewCast"> ��ֹ��� �ε����� ��� �� ����ĳ��Ʈ ����</param>
	/// <param name="maxViewCast"> ��ֹ��� �ε����� ���� ����ĳ��Ʈ ����</param>
	/// <returns></returns>
	private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
	{
		float minAngle = minViewCast.Angle;
		float maxAngle = maxViewCast.Angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		// minViewCast �� maxViewCast ���� ��򰡿� ������ �𼭸��� ã�´�. ������ ����Ž������ �������� ã�¹���̴�.
		for (int i = 0; i< EdgeResolveIterations; i++)
		{
			// �� ���� ������ �߰��� ��´�.
			float angle = (minAngle + maxAngle) / 2;
			// �߰� ������ ���� ViewCastInfo�� �����Ѵ�.
			ViewCastInfo newViewCast = ViewCast(angle);

			bool edgeDstThresholdExceeded = Mathf.Abs(minViewCast.Dst - newViewCast.Dst) > EdgeDstThreshold;

			// ������ �ش� ������ ���̸� ���� �ÿ� minViewCast�� ���ع� ������ ���ٸ� min ���� �ٲ��ش�.
			// ���ع��� ���ٸ� max ���� �ٲ��ش�.
			if (newViewCast.Hit == minViewCast.Hit && !edgeDstThresholdExceeded)
			{
				minAngle = angle;
				minPoint = newViewCast.Point;
			}
			else
			{
				maxAngle = angle;
				maxPoint = newViewCast.Point;
			}
		}
		// ���� ���� ��ȯ�Ѵ�.
		return new EdgeInfo(minPoint, maxPoint);
	}

	/// <summary>
	/// �޾ƿ� ������ ���̸� ���� ���ع��� ���� ���� ���� ���� �����Ͽ� ViewCastInfo ����ü�� ��ȯ�Ѵ�.
	/// </summary>
	/// <param name="globalAngle">�޾ƿ� ����</param>
	/// <returns></returns>
	private ViewCastInfo ViewCast(float globalAngle)
	{
		Vector3 dir = DirFromAngle(globalAngle, true);
		RaycastHit2D hit = Physics2D.Raycast(transform.position,dir,ViewRadius,ObstacleMask);
		
		// ObstacleMask�� ���� ������Ʈ�� ���̰� ����� �� ���� ����
		if (hit)
		{
			return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
		}
		else
		{
			return new ViewCastInfo(false, transform.position + dir * ViewRadius, ViewRadius, globalAngle);
		}
	}

	/// <summary>
	/// ������ ���� �ٶ󺸴� ������ ��ȯ��
	/// </summary>
	/// <param name="angleInDegrees"></param>
	/// <param name="angleIsGlobal"></param>
	/// <returns></returns>
	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			// ������Ʈ ������ ���� ������ ���� ��������
			angleInDegrees -= transform.eulerAngles.z;
		}
		return new Vector3(Mathf.Sin((90 + angleInDegrees) * Mathf.Deg2Rad), Mathf.Cos((90 + angleInDegrees) * Mathf.Deg2Rad), 0);
	}
	/// <summary>
	/// �÷��̾ ã�Ҵ��� ���θ� ��ȯ�Ѵ�.
	/// </summary>
	/// <returns></returns>
	public bool GetBool()
	{
		return isFindPlayer;
	}

	public struct ViewCastInfo
	{
		public bool Hit;
		public Vector3 Point;
		public float Dst;
		public float Angle;

		public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
		{
			Hit = hit;
			Point = point;
			Dst = dst;
			Angle = angle;
		}
	}

	public struct EdgeInfo
	{
		public Vector3 PointA;
		public Vector3 PointB;

		public EdgeInfo(Vector3 pointA, Vector3 pointB)
		{
			PointA = pointA;
			PointB = pointB;
		}
	}


}

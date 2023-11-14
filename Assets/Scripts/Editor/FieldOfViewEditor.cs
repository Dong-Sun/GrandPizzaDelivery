using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
// �Ѽ�ȣ �ۼ�

/// <summary>
/// FieldOfViewPoliceCarŬ������ ���� Ŀ���� ������
/// </summary>
[CustomEditor (typeof (FieldOfViewPoliceCar))]
public class FieldOfViewEditor : Editor
{
	private void OnSceneGUI()
	{
		FieldOfViewPoliceCar fow = (FieldOfViewPoliceCar)target;
		// ������� �׸�
		Handles.color = Color.white;
		// ���� �׸�. ������ ù��° ���ڰ�, �߽� ���� forward. right ���������� �������� Ŀ��. ������ 360��. ������ ���̴� fow.ViewRadius
		Handles.DrawWireArc(fow.transform.localPosition, Vector3.forward, Vector3.right, 360, fow.ViewRadius);
		Vector3 viewAngleA = fow.DirFromAngle(-fow.ViewAngle / 2, false);
		Vector3 viewAngleB = fow.DirFromAngle(fow.ViewAngle / 2, false);

		Handles.DrawLine(fow.transform.localPosition, fow.transform.localPosition + viewAngleA * fow.ViewRadius);
		Handles.DrawLine(fow.transform.localPosition, fow.transform.localPosition + viewAngleB * fow.ViewRadius);

		Handles.color = Color.red;
		foreach (Transform visibleTarget in fow.visibleTargets)
		{
			Handles.DrawLine(fow.transform.position, visibleTarget.position);
		}
	}
}

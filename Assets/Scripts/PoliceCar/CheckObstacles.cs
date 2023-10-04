using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�
public class CheckObstacles : MonoBehaviour, ICheckCol
{
	private IUpdateCheckList iUpdateCheckList;

	private List<Collider2D> col2DList = new List<Collider2D>();	// ���� �ݶ��̴��� �����ǰ� �ִ� ������Ʈ ��

	private int checkNum;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("WallObstacle") &&
			collision.gameObject.layer != LayerMask.NameToLayer("MoveObstacle"))
		{
			return;
		}

		//Debug.Log(collision.gameObject.layer);

		// ó���� ���ؼ��� �ݶ��̴� ���� ����Ʈ�� �߰�
		if (col2DList.Count == 0)
		{
			iUpdateCheckList.UpdateCheck(checkNum, true);
		}
		col2DList.Add(collision);

	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.layer != LayerMask.NameToLayer("WallObstacle") &&
			collision.gameObject.layer != LayerMask.NameToLayer("MoveObstacle"))
		{
			return;
		}

		col2DList.Remove(collision);
		// ��� �ݶ��̴��� �������� ��쿡�� �ݶ��̴� ���� ����Ʈ���� ����
		if (col2DList.Count == 0)
		{
			iUpdateCheckList.UpdateCheck(checkNum, false);
		}
	}

	public void InitNumber(int num, IUpdateCheckList iUpdateCheckList)
	{
		checkNum = num;
		this.iUpdateCheckList = iUpdateCheckList;
	}
}

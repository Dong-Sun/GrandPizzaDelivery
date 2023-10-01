using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class MapWallColider : MonoBehaviour
{
	public enum Arrow { UP, DOWN, RIGHT, LEFT };
	public Arrow ArrowState;

	private Transform trans;

	private void Awake()
	{
		trans = this.gameObject.transform;
	}
	/// <summary>
	/// �÷��̾ ���� ���� ������ �ðܳ���.
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerMove>() != null)
		{
			while (true)
			{
				if (ArrowState == Arrow.RIGHT)  // ������ ��
				{
					collision.transform.position += new Vector3(-0.01f, 0f);
					if (collision.transform.position.x <= trans.position.x - 0.5f)
					{
						break;
					}
				}
				else if (ArrowState == Arrow.LEFT)  // ���� ��
				{
					collision.transform.position += new Vector3(0.01f, 0f);
					if (collision.transform.position.x >= trans.position.x + 0.5f)
					{
						break;
					}
				}
				else if (ArrowState == Arrow.UP)    // ���� ��
				{
					collision.transform.position += new Vector3(0f, -0.01f);
					if (collision.transform.position.y <= trans.position.y - 0.5f)
					{
						break;
					}
				}
				else if (ArrowState == Arrow.DOWN)    // �Ʒ��� ��
				{
					collision.transform.position += new Vector3(0f, 0.01f);
					if (collision.transform.position.y >= trans.position.y + 0.5f)
					{
						break;
					}
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCarCollisionCheck : MonoBehaviour
{
    private IPoliceCarControl iPoliceCarControl;
    private List<IPoliceCarControl> otherIPoliceCarIsBehaviourList = new List<IPoliceCarControl>();
    //�������� �ٸ� ���������� �浹�� ����� �ִ��� üũ�Ѵ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹�� ����� �ִٸ� �ڵ����� �ൿ�� �����Ѵ�.
        if (collision.gameObject.GetComponent<IPoliceCarControl>() != null)
        {
            otherIPoliceCarIsBehaviourList.Add(collision.gameObject.GetComponent<IPoliceCarControl>());
            CheckPriority();
        }
    }

    private void CheckPriority()
    {
        if (iPoliceCarControl == null) { return; }

        if (otherIPoliceCarIsBehaviourList.FindIndex(a => a.GetPoliceCarCode() > iPoliceCarControl.GetPoliceCarCode()) != -1)
        {
            iPoliceCarControl.SetIsBehaviour(false);
            Debug.Log("�۵�1");
        }
        else
        {
            iPoliceCarControl.SetIsBehaviour(true);
            Debug.Log("�۵�2");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IPoliceCarControl>() != null)
        {
            otherIPoliceCarIsBehaviourList.Remove(collision.gameObject.GetComponent<IPoliceCarControl>());
            Invoke("CheckPriority", 1f);
        }
    }

    public void SetIPoliceCarIsBehaviour(IPoliceCarControl iPoliceCarIsBehaviour)
    {
        this.iPoliceCarControl = iPoliceCarIsBehaviour;
    }
}

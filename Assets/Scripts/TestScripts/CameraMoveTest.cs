using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѽ�ȣ �ۼ�

public class CameraMoveTest : MonoBehaviour
{
    private Vector2 vec;

    // Update is called once per frame
    /// <summary>
    /// ī�޶� �׽�Ʈ
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.None))
        {
            Debug.Log("�۵�4");
        }
        this.gameObject.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 0.3f, Input.GetAxis("Vertical") * 0.3f));
    }
}

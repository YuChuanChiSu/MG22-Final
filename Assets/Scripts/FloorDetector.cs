using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( (collision.tag == "Foot" && !CharacterController.Instance.isHandstand) ||
            (collision.tag == "Head" && CharacterController.Instance.isHandstand))
        {
            // �ɹ����
            if(collision.tag == "Map")
            {

            }
            // ������Ծ��������>0�������ǰ����Ϊ��Ծ�����
            MoveController.JumpCount = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Foot" && !CharacterController.Instance.isHandstand) ||
            (collision.tag == "Head" && CharacterController.Instance.isHandstand))
        {
            // �������
            if (collision.tag != "Map")
            {

            }
            // ���������Ϊ��Ծ��ɵģ�������̲�Ӧ���ܹ���Ծ
            if (MoveController.JumpCount == 0)
                MoveController.JumpCount = MoveController.MaxJumpCount;
        }
    }
}

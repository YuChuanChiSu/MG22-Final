using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    public static float leaveY;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( (collision.tag == "Foot" && !CharacterController.Instance.isHandstand) ||
            (collision.tag == "Head" && CharacterController.Instance.isHandstand))
        {
            // �ɹ����
            if(collision.tag == "Map")
            {

            }
            float deltaY = CharacterController.Instance.transform.position.y - leaveY;
            //Debug.Log("�߶Ȳ" + deltaY);
            if (deltaY < -6 && CharacterController.Instance.Form == CharacterModel.CharacterForm.Ice)
            {
                SndPlayer.Play("�ƶ�\\����̬\\����");
                ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "�����ı��飬�������Լ���...��Ҫ����������";
                CharacterController.Instance.HP = 0;
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
            leaveY = CharacterController.Instance.transform.position.y;
            // ���������Ϊ��Ծ��ɵģ�������̲�Ӧ���ܹ���Ծ
            if (MoveController.JumpCount == 0)
                MoveController.JumpCount = MoveController.MaxJumpCount;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
            MoveController.MapTouching = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            MoveController.MapTouching = false;
    }
}

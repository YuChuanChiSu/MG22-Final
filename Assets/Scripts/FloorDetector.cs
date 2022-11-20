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
            // 成功落地
            if(collision.tag == "Map")
            {

            }
            float deltaY = CharacterController.Instance.transform.position.y - leaveY;
            //Debug.Log("高度差：" + deltaY);
            if (deltaY < -6 && CharacterController.Instance.Form == CharacterModel.CharacterForm.Ice)
            {
                SndPlayer.Play("移动\\冰形态\\掉落");
                ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "骄傲的冰块，不允许自己下...不要往下跳啦！";
                CharacterController.Instance.HP = 0;
            }
            // 重置跳跃段数，若>0则表面先前是因为跳跃而落地
            MoveController.JumpCount = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.tag == "Foot" && !CharacterController.Instance.isHandstand) ||
            (collision.tag == "Head" && CharacterController.Instance.isHandstand))
        {
            // 脱离地面
            if (collision.tag != "Map")
            {

            }
            leaveY = CharacterController.Instance.transform.position.y;
            // 如果不是因为跳跃造成的，这个过程不应该能够跳跃
            /**if (MoveController.JumpCount == 0)
                MoveController.JumpCount = MoveController.MaxJumpCount;**/
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
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

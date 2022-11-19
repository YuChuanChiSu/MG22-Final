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
            // 成功落地

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

            // 如果不是因为跳跃造成的，这个过程不应该能够跳跃
            if (MoveController.JumpCount == 0)
                MoveController.JumpCount = MoveController.MaxJumpCount;
        }
    }
}

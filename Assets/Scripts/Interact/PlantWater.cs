using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantWater : InteractBase
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public override bool Interact()
    {
        if (CharacterController.Instance.HP < 10)
        {
            // ÌáÐÑÑªÁ¿²»×ã
            return false;
        }
        spriteRenderer.color = Color.green;
        CharacterController.Instance.HP -= 10;
        return true;
    }
}

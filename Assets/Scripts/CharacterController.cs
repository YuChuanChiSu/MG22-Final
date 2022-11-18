using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance { get; private set; }

    public CharacterForm Form = CharacterForm.Water;
    public CharacterState State = CharacterState.Still;
    public Temperature Temperature = Temperature.Standard;
    public bool isHandstand = false;
    public long HP = 100;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public CharacterAnimator characterAnimator;
    [HideInInspector]
    public CharacterFormChanger characterFormChanger;
    [HideInInspector]
    public MoveController moveController;
    [HideInInspector]
    public HPController hpController;

    private void Awake()
    {
        characterAnimator = gameObject.AddComponent<CharacterAnimator>();
        characterFormChanger = gameObject.AddComponent<CharacterFormChanger>();
        moveController = gameObject.AddComponent<MoveController>();
        hpController = gameObject.AddComponent<HPController>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        Instance = this;
    }
}

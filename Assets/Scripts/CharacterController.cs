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

    public SpriteRenderer spriteRenderer;
    public CharacterAnimator characterAnimator;
    public CharacterFormChanger characterFormChanger;
    public MoveController moveController;
    public HPController hpController;

    private void Awake()
    {
        characterAnimator = new CharacterAnimator(this);
        characterFormChanger = new CharacterFormChanger(this);
        moveController = new MoveController(this);
        hpController = new HPController(this);

        spriteRenderer = GetComponent<SpriteRenderer>();

        Instance = this;
    }
}

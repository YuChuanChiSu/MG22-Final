using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{
    public static CharacterController Instance { get; private set; }

    public Animator animator;
    public CharacterForm Form = CharacterForm.Water;
    public CharacterState State = CharacterState.Still;
    public Temperature Temperature = Temperature.Standard;
    public bool isHandstand = false;
    public float MoveSpeed = 5, JumpDegree = 25;   
    public long HP = 100;
    public bool isHurt;
    public GameObject Hp0;
    public GameObject Hp20;
    public GameObject Hp40;
    public GameObject Hp60;
    public GameObject Hp80;
    public GameObject Hp100;


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
    private void Update()
    {
        Movement();

        if (HP == 100)
        {
            Hp0.SetActive(false);
            Hp20.SetActive(false);
            Hp40.SetActive(false);
            Hp60.SetActive(false);
            Hp80.SetActive(false);
            Hp100.SetActive(true);
        }else if(HP == 80)
        {
            Hp80.SetActive(true);
            Hp100.SetActive(false);
        }else  if(HP == 60)
        {
            Hp80.SetActive(false);
            Hp60.SetActive(true);
        }else if(HP == 40)
        {
            Hp60.SetActive(false);
            Hp40.SetActive(true);
        }else if(HP == 20){
            Hp40.SetActive(false);
            Hp20.SetActive(true);
        }else if(HP == 0)
        {
            Hp20.SetActive(false);
            Hp0.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facemove = Input.GetAxisRaw("Horizontal");
        if(horizontalmove != 0)
        {
            animator.SetFloat("walking", Mathf.Abs(facemove));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ddl")
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

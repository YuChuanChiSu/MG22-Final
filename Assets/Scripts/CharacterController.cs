using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GenericToolKit.Mvvm;

/// <summary>
/// ���������
/// </summary>
public class CharacterController : ObservableMonoBehavior
{
    /// <summary>
    /// ��ǰ����ʵ��
    /// </summary>
    public static CharacterController Instance { get; private set; }
    public static long LastHP = 80;

    public Animator animator;
    public Rigidbody2D rb;
    public LayerMask layerMask;
    public Collider2D collider;

    /// <summary>
    /// ������̬
    /// </summary>
    public CharacterForm Form = CharacterForm.Water;
    /// <summary>
    /// ����״̬
    /// </summary>
    public CharacterState State = CharacterState.Still;
    /// <summary>
    /// �¶�
    /// </summary>
    public  Temperature Temperature = Temperature.Standard;
    /// <summary>
    /// �Ƿ��ڵ���״̬
    /// </summary>
    public bool isHandstand = false;

    [Tooltip("�ƶ��ٶ�")]
    public float MoveSpeed = 5;
    [Tooltip("��Ծ����")]
    public float JumpDegree = 25;

    /// <summary>
    /// Ѫ��
    /// </summary>
    [Tooltip("Ѫ��")]
    [SerializeField]
    private long _hp = 80;
    public long HP
    {
        get => _hp;
        set => SetProperty(ref _hp, value);
    }

    public bool isHurt;
    public GameObject Hp0;
    public GameObject Hp20;
    public GameObject Hp40;
    public GameObject Hp60;
    public GameObject Hp80;
    public GameObject Hp100;
    public float y_max;


    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public CharacterAnimator characterAnimator;
    [HideInInspector]
    public CharacterFormChanger characterFormChanger;
    [HideInInspector]
    public MoveController moveController;

    private void Awake()
    {
        HP = LastHP;

        characterAnimator = gameObject.AddComponent<CharacterAnimator>();
        characterFormChanger = gameObject.AddComponent<CharacterFormChanger>();
        moveController = gameObject.AddComponent<MoveController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        
        Instance = this;
    }
    private void Update()
    {
        if (!isHurt)
        {
            Movement();
        }
        

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
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facemove = Input.GetAxisRaw("Horizontal");
        float ymove = Input.GetAxisRaw("Vertical");
        if(Form == CharacterForm.Water)
        {
            animator.SetBool("water", true);
            if (!isHurt)
            {
                if (horizontalmove != 0)
                {
                    animator.SetBool("idle_water",false);
                    animator.SetFloat("walking", Mathf.Abs(facemove));
                    animator.SetBool("jumping_water", false);
                }

                if (State == CharacterState.Jump)
                {
                    animator.SetBool("idle_water", false);
                    animator.SetBool("jumping_water", true);
                    animator.SetFloat("walking", 0);

                }
                if (horizontalmove == 0)
                {
                    animator.SetBool("idle_water", true);
                    animator.SetFloat("walking", Mathf.Abs(facemove));
                    animator.SetBool("jumping_water", false);
                }

            }

        }
        if (Form == CharacterForm.Ice)
        {
            animator.SetBool("ice",true);
            if (!isHurt)
            {
                if (horizontalmove != 0)
                {
                    animator.SetBool("idle_ice", false);
                    animator.SetFloat("walking_ice", Mathf.Abs(facemove));
                    animator.SetBool("jumping_ice", false);
                }

                if (State == CharacterState.Jump)
                {
                    animator.SetBool("idle_ice", false);
                    animator.SetBool("jumping_ice", true);
                    animator.SetFloat("walking_ice", 0);

                }
                if (horizontalmove == 0)
                {
                    animator.SetBool("idle_ice", true);
                    animator.SetFloat("walking_ice", Mathf.Abs(facemove));
                    animator.SetBool("jumping_ice", false);
                }
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)

    {
        Vector2 p = transform.position;
        y_max = rb.velocity.y;
        if(collision.tag == "Ddl")
        {
            ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "时间亘河之外...是什么？";
            HP = 0;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(collision.tag == "Hurt")
        {
            ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "自食...恶果...";
            HP = 0; // 策划改需求针刺即死 -= 20;
        }
        /**Debug.Log(rb.velocity.y);
        if (Form == CharacterForm.Ice && collision.tag == "Map"&& rb.velocity.y<-18)
        {
            ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "骄傲的冰块，不允许自己下...不要往下跳啦！";
            HP = 0;
        }**/

    }

    private void OnDestroy()
    {
        LastHP = HP;
        Dispose();
    }

    
}

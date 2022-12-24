using System.Collections;
using System.Collections.Generic;
using static CharacterModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GenericToolKit.Mvvm;
using Unity.VisualScripting;

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
    public Collider2D _collider;

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
    public GameObject state_ice;
    public GameObject state_mist;
    public GameObject state_water;
    public bool isDeath = true;
    public long D = 0;
    public long IceDeath = 0;
    public long FireDeath = 0;
    public long OutDeath1 = 0;
    public long OutDeath2 = 0;

    public bool ice = true;
    public bool mist = true;
    public bool water = false;
    public long Switch = 0;
    public bool fireHurt = false;

    public float delayTime = 1;

    public GameObject[] FormEffect1, FormEffect2;

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

    private void Start()
    {
        PropertyChanged += ServiceLocator.Instance.SavePoint.OnHPChanged;
    }
    private void Update()
    {
        if (Time.time >= delayTime)
        {
            Globle.Time += Time.time;
            delayTime += Time.time;
        }
        OutDeath2 = OutDeath1 / 3;
        Globle.GOutDeath2 = Globle.GOutDeath1 / 3;
        IceDeath = D - OutDeath2 - FireDeath -Globle.GWaterDeath;
        for(int i = 0;i < 3; i++)
        {
            FormEffect1[i].SetActive(i == (int)Form);
            FormEffect2[i].SetActive(i == (int)Form);
        }

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
            isDeath = true;
        }else if(HP == 80)
        {
            Hp0.SetActive(false);
            Hp20.SetActive(false);
            Hp40.SetActive(false);
            Hp60.SetActive(false);
            Hp80.SetActive(true);
            Hp100.SetActive(false);
            isDeath = true;
        }
        else  if(HP == 60)
        {
            Hp0.SetActive(false);
            Hp20.SetActive(false);
            Hp40.SetActive(false);
            Hp80.SetActive(false);
            Hp60.SetActive(true);
            Hp100.SetActive(false);
            isDeath = true;
        }
        else if(HP == 40)
        {
            Hp0.SetActive(false);
            Hp20.SetActive(false);
            Hp60.SetActive(false);
            Hp40.SetActive(true);
            Hp80.SetActive(false);
            Hp100.SetActive(false);
            isDeath = true;
        }
        else if(HP == 20){
            Hp0.SetActive(false);
            Hp40.SetActive(false);
            Hp20.SetActive(true);
            Hp60.SetActive(false);
            Hp80.SetActive(false); 
            Hp100.SetActive(false);
            isDeath = true;
        }
        else if(HP == 0)
        {
            
            Hp40.SetActive(false);
            Hp60.SetActive(false);
            Hp80.SetActive(false);
            Hp100.SetActive(false);
            Hp20.SetActive(false);
            Hp0.SetActive(true);
            if(isDeath == true)
            {
                Globle.Death += 1;
                D += 1;
                isDeath = false;
                fireHurt = true;
            }
           
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
        }

    }
    void Movement()
    {

        float horizontalmove = State == CharacterState.Walk ? 1.0f : 0f; //Input.GetAxis("Horizontal");
        float facemove = State == CharacterState.Walk ? 1.0f : 0f; //Input.GetAxisRaw("Horizontal");
        float ymove = Input.GetAxisRaw("Vertical");
        if(Form == CharacterForm.Water)
        {
            if(water == true)
            {
                Globle.GSwitch += 1;
                Switch += 1;
                water = false;
                ice = true;
                mist = true;
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                state_water.SetActive(true);
            }
            
            
            
            animator.SetBool("water", true);
            animator.SetBool("ice", false);
            animator.SetBool("mist", false);
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
                if (horizontalmove == 0 )
                {
                    animator.SetBool("idle_water", true);
                    animator.SetFloat("walking", Mathf.Abs(facemove));
                    animator.SetBool("jumping_water", false);
                }

            }

        }
        if (Form == CharacterForm.Ice)
        {
            if (ice == true)
            {
                Globle.GSwitch += 1;
                Switch += 1;
                ice = false;
                water = true;
                mist = true;
            }
            
            animator.SetBool("ice",true);
            animator.SetBool("water", false);
            animator.SetBool("mist", false);
            if (!isHurt)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    state_ice.SetActive(true);
                }
                if (horizontalmove != 0 )
                {
                    animator.SetBool("idle_ice", false);
                    animator.SetFloat("walking_ice", Mathf.Abs(facemove));
                    animator.SetBool("jumping_ice", false);
                }

                if (State == CharacterState.Jump&& !_collider.IsTouchingLayers(layerMask))
                {
                    animator.SetBool("idle_ice", false);
                    animator.SetBool("jumping_ice", true);
                    animator.SetFloat("walking_ice", 0);

                }
                if (horizontalmove == 0 )
                {
                    animator.SetBool("idle_ice", true);
                    animator.SetFloat("walking_ice", Mathf.Abs(facemove));
                    animator.SetBool("jumping_ice", false);
                }
            }

        }
        if (Form == CharacterForm.Mist)
        {
            if (mist == true)
            {
                Globle.GSwitch += 1;
                Switch += 1;
                mist = false;
                water = true;
                ice = true;
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                state_mist.SetActive(true);
            }
            animator.SetBool("ice", false);
            animator.SetBool("water", false);
            animator.SetBool("mist", true);
            animator.SetBool("idle_water",false);
            if (!isHurt)
            {
                if (horizontalmove != 0)
                {
                    animator.SetBool("idle_mist", false);
                    animator.SetFloat("walking_mist", Mathf.Abs(facemove));
                    animator.SetBool("jumping_mist", false);
                }

                if (State == CharacterState.Jump && !_collider.IsTouchingLayers(layerMask))
                {
                    animator.SetBool("idle_mist", false);
                    animator.SetBool("jumping_mist", true);
                    animator.SetFloat("walking_mist", 0);

                }
                if (horizontalmove == 0 )
                {
                    animator.SetBool("idle_mist", true);
                    animator.SetFloat("walking_mist", Mathf.Abs(facemove));
                    animator.SetBool("jumping_mist", false);
                }
            }

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("相撞：" + collision.name);
        Vector2 p = transform.position;
        y_max = rb.velocity.y;
        if(collision.gameObject.name.ToLower().StartsWith("dead line"))
        {
            ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "时间亘河之外...是什么？";
            HP = 0;
            OutDeath1 += 1;
            Globle.GOutDeath1 += 1;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        /**Debug.Log(rb.velocity.y);
        if (Form == CharacterForm.Ice && collision.tag == "Map"&& rb.velocity.y<-18)
        {
            ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "骄傲的冰块，不允许自己下...不要往下跳啦！";
            HP = 0;
        }**/
        if (collision.gameObject.name.ToLower().StartsWith("firehurt"))
        {
            ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "自食...恶果...";
            HP = 0; // 策划改需求针刺即死 -= 20;
            if(fireHurt == true)
            {
                FireDeath = FireDeath + 1;
                fireHurt = false;
                Globle.GFireDeath = FireDeath + 1;
            }
            
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("相撞：" + collision.gameObject.name);
        if (collision.gameObject.name.ToLower().StartsWith("firehurt"))
        {
            ServiceLocator.Instance.HidePanel.GetHPEmptyText().text = "自食...恶果...";
            HP = 0; // 策划改需求针刺即死 -= 20;
            
        }
    }

    private void OnDestroy()
    {
        //ServiceLocator.Instance.SavePoint.Goodbye();
        Debug.Log("已卸载。");
        LastHP = HP;
        Dispose();
        Instance = null;
    }

    
}

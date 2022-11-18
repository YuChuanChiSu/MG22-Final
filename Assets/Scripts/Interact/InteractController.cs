using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����������
/// </summary>
public class InteractController : MonoBehaviour
{
    private static GameObject _ftipPrefab;
    public static GameObject FTipPrefab
    {
        get
        {
            if (_ftipPrefab == null)
                _ftipPrefab = Resources.Load<GameObject>("Prefabs\\FTipPrefab");
            return _ftipPrefab;
        }
    }
    /// <summary>
    /// ���㽻�����
    /// </summary>
    public static InteractController Active { get; private set; }

    [HideInInspector]
    public GameObject FTipInstance;
    /// <summary>
    /// ������
    /// </summary>
    [HideInInspector]
    public InteractBase Interactor;
    /// <summary>
    /// �Ƿ��ѽ���
    /// </summary>
    [HideInInspector]
    public bool Interacted = false;
    private void Awake()
    {
        Interactor = GetComponent<InteractBase>();
    }
    /// <summary>
    /// �����Ƿ�ɽ���
    /// </summary>
    /// <returns></returns>
    public bool CanActive()
    {
        return !Interacted && Interactor.CanActive();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && CanActive())
        {
            if (Active != null) Active.LostFocus();
            Active = this;
            FTipInstance = Instantiate(FTipPrefab, transform.position + new Vector3(0, transform.localScale.y, 0) / 2, Quaternion.identity);
            FTipInstance.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Active == this)
        {
            LostFocus();
        }
    }
    /// <summary>
    /// ʧȥ��������
    /// </summary>
    public void LostFocus()
    {
        FTipInstance.GetComponent<Animator>().SetFloat("Speed", -2.0f);
        FTipInstance.GetComponent<Animator>().Play("FTipEnter", 0, 1.0f);
        Active = null;
        FTipInstance = null;
    }
    private void Update()
    {
        if (PlotController.PlotLock) return;
        if (Active != this) return;
        if (Input.GetKeyUp(KeyCode.F))
        {
            if(Interactor.Interact())
                Interacted = true;
            if (!CanActive()) LostFocus();
        }
    }
}
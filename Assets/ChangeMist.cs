using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMist : MonoBehaviour
{
    public GameObject mist;

    public float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= 1)
        {
            mist.SetActive(false);
            timer = 0f; // ��ʱ1��
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlatformController : MonoBehaviour
{
    public GameObject NextPlatform;
    public bool isRootPlatform;
    private void Awake()
    {
        transform.parent.gameObject.SetActive(isRootPlatform);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (NextPlatform != null)
                NextPlatform.SetActive(true);
        }
    }
}

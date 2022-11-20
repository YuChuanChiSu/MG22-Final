using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private static Transform originParent;
    private static GameObject activePlatform;

    public float MoveSpeed;
    private int direction = 1;
    private float leftX, rightX;
    private void Awake()
    {
        leftX = transform.parent.Find("LeftBorder").transform.position.x + GetComponent<Collider2D>().bounds.size.x / 2;
        rightX = transform.parent.Find("RightBorder").transform.position.x - GetComponent<Collider2D>().bounds.size.x / 2;
        transform.parent.Find("LeftBorder").gameObject.SetActive(false);
        transform.parent.Find("RightBorder").gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (originParent != null)
                originParent = collision.gameObject.transform.parent;
            collision.gameObject.transform.parent = transform;
            activePlatform = gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (activePlatform != gameObject) return;
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = originParent;
            originParent = null;
        }
    }
    private void Update()
    {
        transform.position += new Vector3(MoveSpeed * direction * Time.deltaTime, 0, 0);
        if (transform.position.x <= leftX)
        {
            direction = -direction;
            Vector3 pos = transform.position;
            pos.x = leftX;
            transform.position = pos;
        }
        if (transform.position.x >= rightX)
        {
            direction = -direction;
            Vector3 pos = transform.position;
            pos.x = rightX;
            transform.position = pos;
        }
    }
}

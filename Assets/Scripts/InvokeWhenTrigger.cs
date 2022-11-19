using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InvokeWhenTrigger : MonoBehaviour
{
    public UnityEvent<BaseEventData> Event;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Event.Invoke(null);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchEvent2D : MonoBehaviour
{
    [SerializeField]
    new string tag = "Player";
    [SerializeField]
    UnityEvent onTouch;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == tag)
        {
            onTouch.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == tag)
        {
            onTouch.Invoke();
        }
    }
}

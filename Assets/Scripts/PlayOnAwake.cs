using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// QUI16000158 | James Quinney
public class PlayOnAwake : MonoBehaviour
{
    [SerializeField]
    UnityEvent onAwake;

    // Called before Start
    void Awake()
    {
        onAwake.Invoke();
    }
}

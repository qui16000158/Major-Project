using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

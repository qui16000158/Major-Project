using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// A serializable class can be modified and saved using the inspector
[System.Serializable]
public class Dialogue
{
    // This is all of the text that will display one by one
    public List<string> conversation = new List<string>();
    // This event will be invoked when there is no dialogue left
    public UnityEvent onDialogueEnded;
    [System.NonSerialized]
    public int currentIndex = -1;
}

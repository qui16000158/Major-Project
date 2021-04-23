using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// QUI16000158 | James Quinney
// This is the container class which is serialized, this can be saved to, and loaded from
[System.Serializable]
public class PData
{
    public Stats stats = new Stats();
}

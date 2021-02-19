using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the container class which is serialized, this can be saved to, and loaded from
[System.Serializable]
public class PData
{
    public float posX, posY, posZ;
    public Vector3 Position {
        get
        {
            return new Vector3(posX, posY, posZ);
        }
        set
        {
            posX = value.x;
            posY = value.y;
            posZ = value.z;
        }
    }
}

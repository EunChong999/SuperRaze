using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointTime 
{
    public Vector3 position;
    public Quaternion rotation;

    public PlayerPointTime(Vector3 _position, Quaternion _rotation)
    {
        position = _position;
        rotation = _rotation;
    }
}

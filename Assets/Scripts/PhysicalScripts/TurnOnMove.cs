using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnMove : MonoBehaviour
{
    [SerializeField] private enum FaceDirection { Right};

    private Direction[] directions;
}

[System.Serializable]
public class Direction
{
    public Vector3 direction;
    public Quaternion rotation;
}
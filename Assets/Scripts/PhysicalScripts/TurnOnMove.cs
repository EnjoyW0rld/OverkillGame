using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnMove : MonoBehaviour
{
    [SerializeField] private enum FaceDirection { Right};

    private Direction[] directions;
    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Direction
{
    public Vector3 direction;
    public Quaternion rotation;
}
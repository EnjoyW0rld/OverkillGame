using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineForObjects : MonoBehaviour
{

    [SerializeField] Vector2 leftLocalPosition;
    [SerializeField] Vector2 rightLocalPosition;

    public Vector3 GetLeftGlobalPosition()
    {
        return new Vector3(leftLocalPosition.x, leftLocalPosition.y, 0) + this.transform.position;
    }

    public Vector3 GetRightGlobalPosition()
    {
        return new Vector3(rightLocalPosition.x, rightLocalPosition.y, 0) + this.transform.position;
    }

    //Draws the line from left to right, so it's easier to see where the objects will move between
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(GetLeftGlobalPosition(), GetRightGlobalPosition());
    }
}

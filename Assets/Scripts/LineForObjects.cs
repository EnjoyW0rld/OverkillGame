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

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(GetLeftGlobalPosition(), GetRightGlobalPosition());
    }
}

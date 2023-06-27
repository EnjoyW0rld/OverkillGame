using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDirectionFlying : MonoBehaviour
{
    public void LookInDirection(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }
}

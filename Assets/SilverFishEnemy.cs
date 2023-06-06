using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectMoveOnLine))]
public class SilverFishEnemy : MonoBehaviour
{
    private ObjectMoveOnLine objectOnLine;
    private void Start()
    {

        objectOnLine = GetComponent<ObjectMoveOnLine>();
    }

    private void Update()
    {
        if (objectOnLine.GetDirection())
        {
            transform.rotation = Quaternion.identity;
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
    }

}

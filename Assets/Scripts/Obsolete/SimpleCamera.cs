using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        Vector3 targ = ((target.position + offset) * .01f) + transform.position * .99f;
        //targ.z = o;
        transform.position = targ;
    }
}

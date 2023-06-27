using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offseter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform body;
    [SerializeField] private Vector3 offset;

    private Vector3 originalBodyPos;

    private void Start()
    {
        originalBodyPos = transform.position;
    }
    private void Update()
    {
        Vector3 newOff = body.position - originalBodyPos;

        transform.position = rb.position;// + offset - newOff;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, .2f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private float groundDist = 0.1f;
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (body == null) Debug.LogError("No rigid body found");
    }

    void Update()
    {
        if (body.velocity.y <= 0.1 && isGrounded())
        {
            body.velocity = Vector3.zero;
        }
    }
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDist);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundDist));
    }
}

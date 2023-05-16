using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private float groundDist = 0.1f;
    private bool isGrounded;
    [SerializeField]private bool isFlying;
    private Rigidbody body;
    private LegConnection[] legs;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (body == null) Debug.LogError("No rigid body found");

        legs = FindObjectsOfType<LegConnection>();
        if (legs == null || legs.Length != 2) Debug.LogError("Wrong amount of legs found");
    }

    void Update()
    {
        isGrounded = IsGrounded();
        isFlying = IsFlying();
        if (body.velocity.y <= 0.1 && isGrounded)
        {
            body.velocity = Vector3.zero;
        }
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDist);
    }
    private bool IsFlying()
    {
        if (isGrounded) return false;
        for (int i = 0; i < legs.Length; i++)
        {
            if (legs[i].GetGrounded()) return false;
        }
        return true;
    }

    //Get functions
    public bool GetFlying() => isFlying;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundDist));
    }
}

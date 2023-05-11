using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class LegConnection : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float maxDist;
    [SerializeField] private float speed = 100;
    [SerializeField] private float verticalAcceleration;
    [SerializeField] private float rayCastDist = 0.1f;
    [SerializeField] private bool isGrounded;
    private Rigidbody legRb;
    private Vector3 currentVelocity;

    private void Start()
    {
        legRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        if (isGrounded) legRb.velocity = Vector3.zero;

        Vector3 velocity = GetDirection();
        float dist = Vector3.Distance(body.transform.position, transform.position);
        //currentVelocity += Vector3.up * verticalAcceleration * Time.deltaTime;
        if (isGrounded && velocity.y < 0)
        {
            //print("Acc up");
            //body.velocity += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (dist < maxDist)
        {
            //currentVelocity += velocity;
            legRb.velocity += velocity;
        }
        else
        {
            Vector3 prevVel = legRb.velocity;
            legRb.velocity = Vector3.zero;
            float diff = maxDist - dist;
            Vector3 backDir = (transform.position - body.transform.position).normalized;

            if (backDir.y < 0)
            {

                // Get direction in circle where to move
                Vector3 crossDir = backDir.x > 0 ? Vector3.Cross(backDir, transform.forward) : Vector3.Cross(backDir, -transform.forward);
                // Applying velocity to the direction where leg should move
                if (prevVel.y < 0)
                    legRb.velocity = prevVel.magnitude * crossDir;//Vector3.Cross(backDir, transform.forward);
            }
            //getting leg back to the circle
            transform.position += backDir * diff * 1.01f;
        }
    }

    private Vector3 GetDirection()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 dir = Vector3.up * inputY + Vector3.right * inputX;
        return dir * Time.deltaTime * speed;
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayCastDist);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayCastDist);
    }
}

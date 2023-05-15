using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class LegConnection : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float maxDist;
    [SerializeField] private float speed = 100;
    [SerializeField, Tooltip("How strong is leg")] private float verticalAcceleration;
    [SerializeField] private float rayCastDist = 0.1f;
    [SerializeField] private bool isGrounded;
    private Rigidbody legRb;
    //private Vector3 currentVelocity;

    //DEBUG VARIABLES
    [SerializeField] private bool wasd;

    private void Start()
    {
        legRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        if (isGrounded && legRb.velocity.y <= 0) legRb.velocity = Vector3.zero;


        //Get the velocity where player is aiming their controller
        Vector3 velocity = GetDirection();
        //Get distance from body to leg
        float dist = Vector3.Distance(body.transform.position, transform.position);

        Vector3 backDir = (transform.position - body.transform.position).normalized; //Direction back to the body
        //When on the ground on pushing down
        if (isGrounded && velocity.y < 0)
        {
            print(backDir);
            //TO DO: add body move in the direction
            body.velocity += Time.deltaTime * verticalAcceleration * (-backDir * .2f + Vector3.up * .8f);
        }
        //Check if leg is too far from body
        if (dist < maxDist)
        {
            //currentVelocity += velocity;
            legRb.velocity += velocity;
        }
        else
        {
            Vector3 prevVel = legRb.velocity; //Save previous velocity
            legRb.velocity = Vector3.zero; //Make current velocity zero
            float diff = maxDist - dist; //how much leg is too far away

            //If leg is higher then the body
            if (backDir.y < 0)
            {
                // Get direction in circle where to move
                Vector3 crossDir = backDir.x > 0 ? Vector3.Cross(backDir, transform.forward) : Vector3.Cross(backDir, -transform.forward);
                // Applying velocity to the direction where leg should move
                if (prevVel.y < 0)
                    legRb.velocity = prevVel.magnitude * crossDir;//Vector3.Cross(backDir, transform.forward);
            }
            backDir.z = 0;
            //getting leg back to the circle
            transform.position += backDir * diff * 1.01f;
        }
    }

    private Vector3 GetDirection()
    {
        //DO NOT DELETE THIS ----
        //float inputX = Input.GetAxis("Horizontal");
        //float inputY = Input.GetAxis("Vertical");
        //Vector3 dir = Vector3.up * inputY + Vector3.right * inputX;
        Vector3 dir = DebugInput();


        return dir * Time.deltaTime * speed;
    }

    private Vector3 DebugInput()
    {
        Vector3 res = Vector3.zero;
        if (wasd)
        {
            if (Input.GetKey(KeyCode.A))
            {
                res += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                res += Vector3.right;
            }
            if (Input.GetKey(KeyCode.S))
            {
                res += Vector3.down;
            }
            if (Input.GetKey(KeyCode.W))
            {
                res += Vector3.up;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                res += Vector3.left;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                res += Vector3.right;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                res += Vector3.down;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                res += Vector3.up;
            }
        }
        return res;
    }

    private bool IsGrounded()
    {
        //int layerMask
        int layerMask = ~LayerMask.GetMask("Body");
        return Physics.Raycast(transform.position, Vector3.down, rayCastDist, layerMask);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayCastDist);
    }
}
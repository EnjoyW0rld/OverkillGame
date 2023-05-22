using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [SerializeField] private float groundDist = 0.1f;
    [SerializeField] private float standingGroundDist = 0.1f;
    [SerializeField] private float gravity = -0.001f;
    [Header("Jump variables")]
    [SerializeField] private float jumpModifier = 5;
    [SerializeField] private float jumpThreshold;

    [Header("Postion")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isStanding;
    [SerializeField] private bool isFlying;
    private bool jumped;
    private Rigidbody body;
    private LegConnection[] legs;
    private Vector3 currentVelocity;

    //DEBUG OPTION
    [SerializeField] private bool debug;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        if (body == null) Debug.LogError("No rigid body found");

        legs = FindObjectsOfType<LegConnection>();
        if (legs == null || legs.Length != 2) Debug.LogError("Wrong amount of legs found");
    }

    void Update()
    {
        //isGrounded = IsGrounded();
        //isFlying = IsFlying();
        CheckGround();
        if (body.velocity.y <= 0.1 && isGrounded)
        {
            body.velocity = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        if (!isGrounded) currentVelocity.y += gravity;
        else
        {
            currentVelocity.x = 0;
            if (currentVelocity.y < 0)
                currentVelocity.y = 0;
        }
        if (isStanding || isFlying)
        {
            body.position += currentVelocity;
        }
        else
        {
            body.position += new Vector3(0, currentVelocity.y, 0);
        }
        currentVelocity *= .9f;

    }
    [Obsolete("Old ground check function")]
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDist);
    }
    private void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out RaycastHit hit);
        if (hit.collider != null)
        {
            if (hit.distance <= groundDist)
            {
                jumped = false;
                isGrounded = true;
                isStanding = false;
            }
            else if (hit.distance > standingGroundDist)
            {
                if (LegsInTheAir())
                {
                    if (!jumped && currentVelocity.magnitude > jumpThreshold)
                    {
                        currentVelocity *= jumpModifier;
                        jumped = true;
                    }
                    isFlying = true;
                    isGrounded = false;
                    isStanding = false;
                    if (debug) print("Jumped, current magnitude is " + currentVelocity.magnitude);
                }
                else
                {
                    isStanding = true;
                    isGrounded = false;
                    isFlying = false;
                }
            }
            else
            {
                isFlying = false;
                isStanding = false;
                isGrounded = false;
            }
            //isFlying = false;
        }
        else
        {
            //isFlying = true;
            //isGrounded = true;
            //isStanding = true;

            return;

            bool legsOnGround = false;
            for (int i = 0; i < legs.Length; i++)
            {
                if (legs[i].GetGrounded())
                {
                    legsOnGround = true;
                    break;
                }
            }
            if (legsOnGround)
            {
                jumped = false;
                isStanding = true;
                isFlying = false;
                isGrounded = false;
            }
            else
            {
                if (!jumped && currentVelocity.magnitude > 0.1f)
                {
                    currentVelocity *= jumpModifier;
                    jumped = true;
                }
                isFlying = true;
                //isStanding = false;
                isGrounded = false;
            }
        }
        //return Physics.Raycast(transform.position, Vector3.down, standingGroundDist);
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
    private bool LegsInTheAir()
    {
        for (int i = 0; i < legs.Length; i++)
        {
            if (legs[i].GetGrounded())
            {
                return false;
            }
        }
        return true;
    }

    public void ModifyVelocity(Vector3 vel) => currentVelocity += vel;
    //Get functions
    public bool GetFlying() => isFlying;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * standingGroundDist));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundDist));
    }
}
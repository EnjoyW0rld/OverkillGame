using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete("Old body controller script, current is JumpFrog.cs")]
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

    /// <summary>
    /// Fucntion to check body position in the world
    /// Modyfies 3 bool - isGrounded, isStanding and isFlying
    /// Applies jump modifier to the body here
    /// </summary>
    private void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out RaycastHit hit);
        //If spotted something
        if (hit.collider != null)
        {
            //If player body is on the ground
            if (hit.distance <= groundDist)
            {
                jumped = false;
                isGrounded = true;
                isStanding = false;
            }
            //If body is higher then standing distance
            else if (hit.distance > standingGroundDist)
            {
                //Check to see if body is flying
                if (LegsInTheAir())
                {
                    //To implement some code to be executed exectly at the moment player jumped, do it here
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
        }
        //Code for else, decided to leave it here for now
        /**
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
         */
    }
    /// <summary>
    /// Checks if both legs are in the air
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Call this function to modify body velocity from the other script
    /// </summary>
    /// <param name="vel">velocity to add</param>
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody))]
public class LegConnection : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float maxDist;
    [SerializeField] private float speed = 100;
    [SerializeField, Tooltip("How strong is leg")] private float verticalAcceleration;
    [SerializeField] private float gravity;
    [SerializeField] private float rayCastDist = 0.1f;
    [SerializeField] private bool isGrounded;
    //private bool jumped;
    private Rigidbody legRb;
    private BodyController bodyController;
    private Vector3 currentVelocity;
    //private Vector3 prevBodyVel;
    private Gamepad currentGamepad;
    //DEBUG VARIABLES
    [SerializeField] private bool wasd;

    private void Start()
    {
        legRb = GetComponent<Rigidbody>();
        bodyController = body.GetComponent<BodyController>();
        if (bodyController == null) Debug.LogError("No body controller was found!");

        currentGamepad = Gamepad.current;
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        //if (isGrounded && legRb.velocity.y <= 0) legRb.velocity = Vector3.zero;
        //if (isGrounded && jumped) jumped = true;

        //Get the velocity where player is aiming their controller
        Vector3 velocity = GetDirection();
        //Get distance from body to leg
        //float dist = Vector3.Distance(body.transform.position, transform.position);
        if (isGrounded) velocity.x = 0;
        //Vector3 backDir = (transform.position - body.transform.position).normalized; //Direction back to the body
        //When on the ground on pushing down
        if (isGrounded && velocity.y < 0)
        {
            //TO DO: add body move in the direction
            //body.velocity += Time.deltaTime * verticalAcceleration * (-backDir * .3f + Vector3.up * .7f);
            //bodyController.ModifyVelocity(Time.deltaTime * verticalAcceleration * (-backDir * .3f + Vector3.up * .7f));
        }
        /**
        if (bodyController.GetFlying())
        {
            if (!jumped)
            {
                legRb.velocity += body.velocity;
                jumped = true;
            }
            //Check if leg is too far from body
            if (dist < maxDist)
            {
                legRb.velocity += velocity;

            }
            else
            {
                float diff = maxDist - dist;
                transform.position += new Vector3(backDir.x, backDir.y, 0) * diff * 1.01f;
            }
        }
        else
        {
            jumped = false;
            if (dist < maxDist)
            {

                legRb.velocity += velocity;
                /**
                if (!jumped && !isGrounded)
                {
                    jumped = true;
                    legRb.velocity += body.velocity;
                }
                 /**
            }
            else
            {
                HandleOutOfRange(dist, backDir);
            }
        }
        /**/

        currentVelocity += velocity;
        //legRb.velocity = Vector3.zero;
        //transform.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        legRb.position += currentVelocity;


        float dist = Vector3.Distance(body.transform.position, transform.position);
        Vector3 backDir = (transform.position - body.transform.position).normalized; //Direction back to the body

        if (isGrounded && currentVelocity.y < 0)
        {
            //bodyController.ModifyVelocity(Time.deltaTime * verticalAcceleration * (-backDir * .3f + Vector3.up * .7f));
            bodyController.ModifyVelocity((Vector3.up * .1f + -backDir * .9f) * verticalAcceleration);
        }

        if (dist > maxDist)
        {
            float diff = maxDist - dist; //how much leg is too far away
            legRb.position += backDir * diff;
        }
        currentVelocity *= .9f;
        //currentVelocity.x *= .9f;
        if (!isGrounded) currentVelocity.y += gravity;
        else
        {
            if (currentVelocity.y < 0)
            {
                currentVelocity.y = 0;
            }
        }
    }

    /// <summary>
    /// Will be executed when distance from body to leg is bigger then maxDist
    /// </summary>
    /// <param name="dist"></param>
    /// <param name="backDir"></param>
    private void HandleOutOfRange(float dist, Vector3 backDir)
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
            {
                legRb.velocity = prevVel.magnitude * crossDir;//Vector3.Cross(backDir, transform.forward);
            }
        }
        backDir.z = 0;
        //if(body.velocity.y)
        //getting leg back to the circle
        transform.position += backDir * diff * 1.1f;
    }

    private Vector3 GetDirection()
    {
        Vector3 dir = Vector3.zero;
        //DO NOT DELETE THIS ----
        if (currentGamepad != null)
        {
            Vector2 stickValue = wasd ? currentGamepad.leftStick.ReadValue() : currentGamepad.rightStick.ReadValue();
            dir += new Vector3(stickValue.x, stickValue.y, 0);
        }
        else
        {
            //float inputX = Input.GetAxis("Horizontal");
            //float inputY = Input.GetAxis("Vertical");
            //Vector3 dir = Vector3.up * inputY + Vector3.right * inputX;
            dir = DebugInput();

        }


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

    //Get functions
    public bool GetGrounded() => isGrounded;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(body.transform.position, transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayCastDist);
    }
    private void OnValidate()
    {
        foreach (var obj in FindObjectsOfType<LegConnection>())
        {
            if (obj != this)
            {
                obj.speed = speed;
                obj.maxDist = maxDist;
                obj.verticalAcceleration = verticalAcceleration;
                obj.rayCastDist = rayCastDist;
                obj.gravity = gravity;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class is for leg, there should be exectly two instances in the scene
/// </summary>
public class LegJump : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private float maxDist;
    [SerializeField] private float speed = 100;
    [SerializeField, Tooltip("How strong is leg")] private float verticalAcceleration;
    [SerializeField] private float gravity;
    [SerializeField] private float rayCastDist = 0.1f;
    [SerializeField,Tooltip("Read only")] private bool isGrounded;

    private Rigidbody legRb;
    private BodyController bodyController;
    private Vector3 currentVelocity;
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

        //Get the velocity where player is aiming their controller
        Vector3 velocity = GetDirection();

        if (isGrounded) velocity.x = 0; //If player on the ground, do not move to the side
        //When on the ground on pushing down
        
        currentVelocity += velocity;
    }

    private void FixedUpdate()
    {
        legRb.position += currentVelocity;


        float dist = Vector3.Distance(body.transform.position, transform.position);
        Vector3 backDir = (transform.position - body.transform.position).normalized; //Direction back to the body


        //If leg is too far from the body
        if (dist > maxDist)
        {
            float diff = maxDist - dist; //how much leg is too far away
            legRb.position += backDir * diff;
        }

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
    /// Function to get user input
    /// </summary>
    /// <returns>Vector3 with applied Time.deltaTime and speed variable</returns>
    private Vector3 GetDirection()
    {
        Vector3 dir = Vector3.zero;
        if (currentGamepad != null)
        {
            Vector2 stickValue = wasd ? currentGamepad.leftStick.ReadValue() : currentGamepad.rightStick.ReadValue();
            dir += new Vector3(stickValue.x, stickValue.y, 0);
        }
        else
        {
            dir = DebugInput();

        }


        return dir * Time.deltaTime * speed;
    }
    /// <summary>
    /// Fallback system for keyboard input
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Function to modify distance according to the point where leg is right now
    /// </summary>
    [ContextMenu("Set max distance")]
    private void SetMaxDist()
    {
        maxDist = Vector3.Distance(transform.position, body.position);
        //OnValidate();
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

}
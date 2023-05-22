using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.HighDefinition;

/// <summary>
/// This class is for leg, there should be exectly two instances in the scene
/// </summary>
public class LegConnection : MonoBehaviour
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
    private Vector3 inputVelocity;
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
        inputVelocity = Vector3.zero;
        //Get the velocity where player is aiming their controller
        Vector3 velocity = GetDirection();
        inputVelocity = velocity;
        if (isGrounded) velocity.x = 0; //If player on the ground, do not move to the side
        //When on the ground on pushing down
        
        currentVelocity += velocity;
    }

    private void FixedUpdate()
    {
        legRb.position += currentVelocity;


        float dist = Vector3.Distance(body.transform.position, transform.position);
        Vector3 backDir = (transform.position - body.transform.position).normalized; //Direction back to the body

        //If on the ground and pushing down
        if (isGrounded && inputVelocity.y < 0)
        {
            Debug.LogError("eeee");
            bodyController.ModifyVelocity((Vector3.up * .1f + -backDir * .9f) * verticalAcceleration);
        }

        //If leg is too far from the body
        if (dist > maxDist)
        {
            float diff = maxDist - dist; //how much leg is too far away
            legRb.position += backDir * diff;
        }
        currentVelocity *= .9f;

        if (!isGrounded) currentVelocity.y += gravity;
        else
        {
            if (currentVelocity.y < 0)
            {
                currentVelocity.y = 0;
            }
        }
    }

    [System.Obsolete("Old way to handle out of range body")]
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
            //float inputX = Input.GetAxis("Horizontal");
            //float inputY = Input.GetAxis("Vertical");
            //Vector3 dir = Vector3.up * inputY + Vector3.right * inputX;
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
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayCastDist, layerMask))
        {
            if (hit.collider.isTrigger) return false;

            return true;
        }
        return false;
    }

    /// <summary>
    /// Function to modify distance according to the point where leg is right now
    /// </summary>
    [ContextMenu("Set max distance")]
    private void SetMaxDist()
    {
        maxDist = Vector3.Distance(transform.position, body.position);
        OnValidate();
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
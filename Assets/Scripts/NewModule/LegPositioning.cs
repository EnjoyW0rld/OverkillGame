using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class LegPositioning : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool wasd;
    [SerializeField] private float _groundDist = 0.1f;

    private JumpFrog bodyJump;
    private Rigidbody rb;
    [SerializeField] private bool isGrounded;
    private float maxDist;
    private Gamepad gamepad;
    private bool twoGamepads;

    [SerializeField] UnityEvent onLegsMovingStart;
    [SerializeField] UnityEvent onLegsMovingStop;


    private void Start()
    {
        bodyJump = FindObjectOfType<JumpFrog>();
        if (bodyJump == null) Debug.LogError("No body found!");
        rb = GetComponent<Rigidbody>();
        maxDist = bodyJump.GetMaxDist();
    }

    void Update()
    {
        Vector3 input = GetInput();
        if (input.magnitude != 0 && !isGrounded)
        {
            rb.velocity += new Vector3(0, -rb.velocity.y, 0);
            onLegsMovingStart?.Invoke();
        }
        else onLegsMovingStop?.Invoke();
        rb.position += input * Time.deltaTime * _speed;
    }
    private void FixedUpdate()
    {
        float currentDist = Vector3.Distance(bodyJump.transform.position, transform.position);
        Vector3 backVector = (bodyJump.transform.position - transform.position).normalized;

        if (currentDist > maxDist)
        {
            rb.position += -backVector * (maxDist - currentDist) * 1.1f;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _groundDist);
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
    private Vector3 GetInput()
    {

        Vector3 res = Vector3.zero;
        if (gamepad == null)
        {
            res = DebugInput();
        }
        else
        {
            if (twoGamepads)
            {
                Vector2 stickValue = gamepad.leftStick.ReadValue();
                res += new Vector3(stickValue.x, stickValue.y, 0);

            }
            else
            {
                Vector2 stickValue = wasd ? gamepad.leftStick.ReadValue() : gamepad.rightStick.ReadValue();
                res += new Vector3(stickValue.x, stickValue.y, 0);
            }
        }
        return res;
    }
    [ContextMenu("Print distance")]
    private void PrintDist()
    {
        print(Vector3.Distance(FindObjectOfType<JumpFrog>().transform.position, transform.position));
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    //Public functions
    public bool GetGrounded() => isGrounded;
    public void SetGamepad(Gamepad gamepad, bool twoGamepads)
    {
        this.gamepad = gamepad;
        this.twoGamepads = twoGamepads;
    }
}
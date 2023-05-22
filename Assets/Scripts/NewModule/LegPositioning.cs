using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LegPositioning : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool wasd;
    [SerializeField] private float _groundDist = 0.1f;

    private JumpFrog _bodyJump;
    private Rigidbody _rb;
    [SerializeField] private bool _isGrounded;
    private float maxDist;
    private Gamepad _gamepad;

    private void Start()
    {
        _bodyJump = FindObjectOfType<JumpFrog>();
        _rb = GetComponent<Rigidbody>();
        maxDist = _bodyJump.GetMaxDist();
        _gamepad = Gamepad.current;
    }

    void Update()
    {
        _isGrounded = IsGrounded();
        Vector3 input = GetInput();
        if (input.magnitude != 0)
        {
            _rb.velocity += new Vector3(0, -_rb.velocity.y, 0);
        }
        _rb.position += input * Time.deltaTime * _speed;
    }
    private void FixedUpdate()
    {

        float currentDist = Vector3.Distance(_bodyJump.transform.position, transform.position);
        Vector3 backVector = (_bodyJump.transform.position - transform.position).normalized;

        if (currentDist > maxDist)
        {
            _rb.position += -backVector * (maxDist - currentDist) * 1.1f;
        }
    }

    public bool GetGrounded() => _isGrounded;
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
        if (_gamepad == null)
        {
            res = DebugInput();
        }
        else
        {
            Vector2 stickValue = wasd ? _gamepad.leftStick.ReadValue() : _gamepad.rightStick.ReadValue();
            res += new Vector3(stickValue.x, stickValue.y, 0);
        }
        return res;
    }
    [ContextMenu("Print distance")]
    private void PrintDist()
    {
        print(Vector3.Distance(FindObjectOfType<JumpFrog>().transform.position,transform.position));
    }
}

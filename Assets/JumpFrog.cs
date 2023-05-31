using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpFrog : MonoBehaviour
{
    [SerializeField] private LegPositioning leftLeg;
    [SerializeField] private LegPositioning rightLeg;

    [SerializeField] private float strenght = 1.0f;
    [SerializeField] private float maxDist = 1;
    [SerializeField] private float jumpThreshold = .5f;

    private Rigidbody rb;

    private Func<float, float> jumpModifier;

    private Vector3 differenceLeft;
    private Vector3 differenceRight;
    private Gamepad _gamepad;

    #region jumpSwitcheVariables
    private bool jumpedLeft = false;
    private bool jumpedRight = false;

    private bool pressedLeft;
    private bool pressedRight;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _gamepad = Gamepad.current;
    }

    void Update()
    {
        differenceLeft = (this.transform.position + new Vector3(0, 0.5f, 0)) - leftLeg.transform.position;

        differenceRight = (this.transform.position + new Vector3(0, 0.5f, 0)) - rightLeg.transform.position;
        HandleInput();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<BodyAffecter>(out BodyAffecter affector))
        {
            jumpModifier = affector.GetExpression();
            affector.OnCollisionAction(this);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.TryGetComponent<BodyAffecter>(out BodyAffecter affector))
        {
            jumpModifier = null;
        }
    }
    //Private functions
    private void ApplyJumpForce(Vector3 normalDirection)
    {
        if (jumpModifier == null)
        {
            rb.AddForce(normalDirection * strenght, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(normalDirection * jumpModifier(strenght), ForceMode.Impulse);
        }
    }
    private void HandleInput()
    {
        if (_gamepad != null)
        {
            if (_gamepad.buttonEast.ReadValue() == 1 && rightLeg.GetGrounded())
            {
                if (!pressedRight)
                {
                    pressedRight = true;
                    jumpedRight = true;
                }
            }
            else
            {
                pressedRight = false;
            }

            if (_gamepad.dpad.left.ReadValue() == 1 && leftLeg.GetGrounded())
            {
                if (!pressedLeft)
                {
                    pressedLeft = true;
                    jumpedLeft = true;
                }
            }
            else
            {
                pressedLeft = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightShift) && rightLeg.GetGrounded())
            {
                jumpedRight = true;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift) && leftLeg.GetGrounded())
            {
                jumpedLeft = true;
            }

        }

    }
    private void FixedUpdate()
    {

        if (jumpedLeft)
        {
            if (Vector3.Dot(differenceLeft.normalized, Vector3.up) > jumpThreshold)
            {
                //rb.AddForce(differenceRight.normalized * strenght, ForceMode.Impulse);
                ApplyJumpForce(differenceRight.normalized);
            }
            jumpedLeft = false;
        }

        if (jumpedRight)
        {
            if (Vector3.Dot(differenceRight.normalized, Vector3.up) > jumpThreshold)
            {
                ApplyJumpForce(differenceLeft.normalized);
            }
            jumpedRight = false;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + differenceLeft.normalized * 5);

        Gizmos.DrawLine(this.transform.position, this.transform.position + differenceRight.normalized * 5);
    }

    //public functions
    public float GetMaxDist() => maxDist;
    public Vector3 GetVelocity() => rb.velocity;
    public void ApplyForce(Vector3 force)
    {
        print("added force " + force);
        rb.AddForce(force, ForceMode.Impulse);
    }

    public Vector3 GetPredictedVelocity()
    {
        // velocity = Force/Mass
        return (differenceLeft.normalized * strenght + differenceRight.normalized * strenght) / rb.mass;
    }
}

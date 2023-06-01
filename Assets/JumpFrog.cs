using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class JumpFrog : MonoBehaviour
{
    [Header("Leg instances")]
    [SerializeField] private LegPositioning leftLeg;
    [SerializeField] private LegPositioning rightLeg;
    [SerializeField] private float cooldownTime = 10;
    [Header("Move variables")]
    [SerializeField] private float strenght = 1.0f;
    [SerializeField] private float maxDist = 1;
    [SerializeField] private float jumpThreshold = .5f;



    private Vector3 previousVelocity;
    private Rigidbody rb;
    private Func<float, float> jumpModifier;

    private Vector3 differenceLeft;
    private Vector3 differenceRight;
    private Gamepad[] gamepads;

    #region jumpSwitchVariables
    private bool jumpedLeft = false;
    private bool jumpedRight = false;

    [SerializeField] private bool pressedLeft;
    [SerializeField] private bool pressedRight;
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //gamepad = Gamepad.current;
        gamepads = Gamepad.all.ToArray();

        if (gamepads.Length == 2)
        {
            leftLeg.SetGamepad(gamepads[1], true);
            rightLeg.SetGamepad(gamepads[0], true);
        }
        if (gamepads.Length == 1)
        {
            leftLeg.SetGamepad(gamepads[0], false);
            rightLeg.SetGamepad(gamepads[0], false);
        }
        //print(Gamepad.all.Count);
    }

    void Update()
    {
        differenceLeft = (this.transform.position + new Vector3(0, 0.5f, 0)) - leftLeg.transform.position;

        differenceRight = (this.transform.position + new Vector3(0, 0.5f, 0)) - rightLeg.transform.position;
        HandleInput();
    }
    private void LateUpdate()
    {
        previousVelocity = rb.velocity;
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
        if (gamepads.Length > 2)
        {
            Debug.LogError("More then two gamepads found!");
            return;
        }
        if (gamepads.Length == 2)
        {
            if (gamepads[0].buttonSouth.ReadValue() == 1 && rightLeg.GetGrounded())
            {
                if (!pressedRight)
                {
                    pressedRight = true;
                    jumpedRight = true;
                    StartCoroutine(StartCooldown(false));
                }
            }
            else
            {
                //pressedRight = false;
            }

            if (gamepads[1].buttonSouth.ReadValue() == 1 && leftLeg.GetGrounded())
            {
                if (!pressedLeft)
                {
                    pressedLeft = true;
                    jumpedLeft = true;
                    StartCoroutine(StartCooldown(true));
                }
            }
            else
            {
                //pressedLeft = false;
            }
        }
        if (gamepads.Length == 1)
        {
            if (gamepads[0].buttonEast.ReadValue() == 1 && rightLeg.GetGrounded())
            {
                if (!pressedRight)
                {
                    pressedRight = true;
                    jumpedRight = true;
                StartCoroutine(StartCooldown(false));
                }
            }
            else
            {
                //pressedRight = false;
            }

            if (gamepads[0].dpad.left.ReadValue() == 1 && leftLeg.GetGrounded())
            {
                if (!pressedLeft)
                {
                    pressedLeft = true;
                    jumpedLeft = true;
                    StartCoroutine(StartCooldown(true));

                }
            }
            else
            {
                //pressedLeft = false;
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

    private IEnumerator StartCooldown(bool left)
    {
        yield return new WaitForSeconds(cooldownTime);
        if (left) pressedLeft = false;
        else pressedRight = false;
    }

    private void FixedUpdate()
    {

        if (jumpedLeft)
        {
            if (Vector3.Dot(differenceLeft.normalized, Vector3.up) > jumpThreshold)
            {
                //rb.AddForce(differenceRight.normalized * strenght, ForceMode.Impulse);
                ApplyJumpForce(differenceLeft.normalized);
            }
            //StartCoroutine(StartCooldown(true));
            jumpedLeft = false;
        }

        if (jumpedRight)
        {
            if (Vector3.Dot(differenceRight.normalized, Vector3.up) > jumpThreshold)
            {
                ApplyJumpForce(differenceRight.normalized);
            }
            //StartCoroutine(StartCooldown(false));
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
    public Vector3 GetVelocity() => previousVelocity;
    public void ApplyForce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public Vector3 GetPredictedVelocity()
    {
        // velocity = Force/Mass
        return (differenceLeft.normalized * strenght + differenceRight.normalized * strenght) / rb.mass;
    }
}

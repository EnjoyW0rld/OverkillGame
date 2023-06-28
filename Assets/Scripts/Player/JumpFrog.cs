using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private float maxVelocityMagnitude = 7;
    [Header("Events")]
    public UnityEvent OnJumped;
    [SerializeField] private UnityEvent OnLanded;

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

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<BodyAffecter>(out BodyAffecter affector))
        {
            jumpModifier = affector.GetExpression();
            affector.OnCollisionAction(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<BodyAffecter>(out BodyAffecter affector))
        {
            jumpModifier = null;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        OnLanded?.Invoke();
    }

    //Private functions
    private void ApplyJumpForce(Vector3 normalDirection)
    {
        if (GetVelocity().magnitude > maxVelocityMagnitude) return;
        if (jumpModifier == null)
        {
            rb.AddForce(normalDirection * strenght, ForceMode.Impulse);
            OnJumped?.Invoke();

        }
        else
        {
            rb.AddForce(normalDirection * jumpModifier(strenght), ForceMode.Impulse);
            OnJumped?.Invoke();

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
            if (gamepads[0].buttonSouth.wasPressedThisFrame && rightLeg.GetGrounded())
            {
                if (!pressedRight)
                {
                    pressedRight = true;
                    jumpedRight = true;
                    StartCoroutine(StartCooldown(false));
                }
            }

            if (gamepads[1].buttonSouth.wasPressedThisFrame && leftLeg.GetGrounded())
            {
                if (!pressedLeft)
                {
                    pressedLeft = true;
                    jumpedLeft = true;
                    StartCoroutine(StartCooldown(true));
                }
            }
        }
        if (gamepads.Length == 1)
        {
            if (gamepads[0].buttonEast.wasPressedThisFrame && rightLeg.GetGrounded())
            {
                if (!pressedRight)
                {
                    pressedRight = true;
                    jumpedRight = true;
                    StartCoroutine(StartCooldown(false));
                }
            }

            if (gamepads[0].dpad.left.wasPressedThisFrame && leftLeg.GetGrounded())
            {
                if (!pressedLeft)
                {
                    pressedLeft = true;
                    jumpedLeft = true;
                    StartCoroutine(StartCooldown(true));

                }
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
            ApplyJumpForce(differenceLeft.normalized);
            jumpedLeft = false;
        }

        if (jumpedRight)
        {
            ApplyJumpForce(differenceRight.normalized);
            jumpedRight = false;
        }

    }

    //public functions
    public float GetMaxDist() => maxDist;
    public Vector3 GetVelocity() => previousVelocity;
    public void ApplyForce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void ResetVelocity() => rb.velocity = Vector3.zero;
    public Vector3 GetPredictedVelocity()
    {
        if (jumpModifier == null)
        {
            return (differenceLeft.normalized * strenght + differenceRight.normalized * strenght) / rb.mass;

        }
        else
        {
            return (differenceLeft.normalized * jumpModifier(strenght) + differenceRight.normalized * jumpModifier(strenght)) / rb.mass;
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + differenceLeft.normalized * 5);

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(this.transform.position, this.transform.position + differenceRight.normalized * 5);

        Gizmos.color = Color.white;
    }

}

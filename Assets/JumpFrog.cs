using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpFrog : MonoBehaviour
{
    [SerializeField] LegPositioning leftLeg;
    [SerializeField] LegPositioning rightLeg;

    [SerializeField] float strenght = 1.0f;
    [SerializeField] private float maxDist = 1;

    private Rigidbody rb;

    bool jumpedLeft = false;
    bool jumpedRight = false;

    Vector3 differenceLeft;
    Vector3 differenceRight;
    private Gamepad _gamepad;
    private bool pressedLeft;
    private bool pressedRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {
        differenceLeft = (this.transform.position + new Vector3(0, 0.5f, 0)) - leftLeg.transform.position;

        differenceRight = (this.transform.position + new Vector3(0, 0.5f, 0)) - rightLeg.transform.position;

        _gamepad.dpad.left.ReadValue();

        if (_gamepad != null)
        {
            if (_gamepad.buttonEast.ReadValue() == 1 && leftLeg.GetGrounded())
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
            if (_gamepad.dpad.left.ReadDefaultValue() == 1 && leftLeg.GetGrounded())
            {
                jumpedLeft = true;
            }

        }

    }

    public float GetMaxDist() => maxDist;
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + differenceLeft.normalized * 5);

        Gizmos.DrawLine(this.transform.position, this.transform.position + differenceRight.normalized * 5);
    }


    public void FixedUpdate()
    {

        if (jumpedLeft)
        {
            rb.AddForce(differenceLeft.normalized * strenght, ForceMode.Impulse);
            jumpedLeft = false;
        }

        if (jumpedRight)
        {
            rb.AddForce(differenceRight.normalized * strenght, ForceMode.Impulse);
            jumpedRight = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFrog : MonoBehaviour
{



    [SerializeField] Transform leftLeg;
    [SerializeField] Transform rightLeg;

    [SerializeField] float strenght = 1.0f;

    Rigidbody rb;

    bool jupmedLeft = false;
    bool jumpedRight = false;

    Vector3 differenceLeft;
    Vector3 differenceRight;
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        differenceLeft = (this.transform.position + new Vector3(0, 0.5f, 0)) - leftLeg.position;

        differenceRight = (this.transform.position + new Vector3(0, 0.5f, 0)) - rightLeg.position;

        if (Input.GetKeyDown(KeyCode.RightShift))
        {



            jumpedRight = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            jupmedLeft = true;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position, this.transform.position + differenceLeft.normalized * 5);

        Gizmos.DrawLine(this.transform.position, this.transform.position + differenceRight.normalized * 5);
    }


    public void FixedUpdate()
    {

        if (jupmedLeft) {
            rb.AddForce(differenceLeft.normalized * strenght, ForceMode.Impulse);
            jupmedLeft = false;
        }

        if (jumpedRight)
        {
            rb.AddForce(differenceRight.normalized * strenght, ForceMode.Impulse);
            jumpedRight = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Damagable))]
public class Drone : MonoBehaviour
{

    private Transform player;

    [Range(0, 90)]
    [SerializeField] int angle = 45;

    [Range(0, 50)]
    [SerializeField] float range = 1.0f;


    [SerializeField] float reduceSanitySpeed = 2.0f;

    //private Sanity sanity;
    private Damagable damagable;
    bool playerInRange = false;

    bool appliedModifier;

    private void Start()
    {
        damagable = GetComponent<Damagable>();
        player = GameObject.FindGameObjectWithTag("Body").transform;
        //sanity = GameObject.FindObjectOfType<Sanity>();
    }


    public void OnDrawGizmos()
    {

        Vector3 left = transform.position + new Vector3(Mathf.Cos((angle - 90) * Mathf.Deg2Rad), Mathf.Sin((angle - 90) * Mathf.Deg2Rad)) * range;

        Vector3 right = transform.position + new Vector3(Mathf.Cos((-angle - 90) * Mathf.Deg2Rad), Mathf.Sin((-angle - 90) * Mathf.Deg2Rad)) * range;


        Gizmos.DrawLine(transform.position, left);
        Gizmos.DrawLine(transform.position, right);
      //  Gizmos.DrawWireSphere(transform.position, range);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerInside();
        if (playerInRange)
        {
            damagable.OnEnterDamageArea();
            appliedModifier = true;
        }
        else if(appliedModifier && !playerInRange)
        {
            damagable.OnExitDamageArea();
            appliedModifier = false;
        }
    }

    //Check IF ANGLE IS VIALBE
    public void CheckIfPlayerInside()
    {
        Vector3 vector = player.position - transform.position;

        float angleDiference = Mathf.Atan(vector.y / vector.x);

        float angleDeg = angleDiference * Mathf.Rad2Deg;

   //     Debug.Log("______");
   //     Debug.Log(angleDeg);
   //     Debug.Log(vector.magnitude);

        bool leftCor = (angleDeg >= -90 && angleDeg <= -(90  - angle));
        bool rightCor = (angleDeg >= (90 - angle) && angleDeg <= 90);


        if ((leftCor || rightCor) && vector.magnitude < range )
        {
            if (!playerInRange)
            {
                //sanity.ChangeSanitySpeed(reduceSanitySpeed);
                playerInRange = true;
            }
        //    Debug.Log("InArea");
        } else if (playerInRange)
        {
            //sanity.ResetSanitySpeed();
            playerInRange = false;
        }


    }
}

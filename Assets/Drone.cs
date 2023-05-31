using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Drone : MonoBehaviour
{

    [SerializeField] Transform player;
   


    [Range(0, 90)]
    [SerializeField] int angle = 45;

    [Range(0, 50)]
    [SerializeField] float range = 1.0f;



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
        
    }



    //Check IF ANGLE IS VIALBE
    public void CheckIfPlayerInside()
    {
        Vector3 vector = player.position - transform.position;



        float angleDiference = Mathf.Atan(vector.y / vector.x);

        float angleDeg = angleDiference * Mathf.Rad2Deg;

        bool leftCor = (angleDeg >= -90 && angleDeg <= -angle);
        bool rightCor = (angleDeg >= angle && angleDeg <= 90);


        if ((leftCor || rightCor) && vector.magnitude < range)
        {
        //    sanity.ReduceSanity(5);
            Debug.Log("InArea");
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Drone : MonoBehaviour
{

    [SerializeField] Transform player;
   
    [SerializeField] Vector3 pointPatrolRight;
    [SerializeField] Vector3 pointPatrolLeft;

    private Vector3 pointToPatrolTo;


    [Range(0, 90)]
    [SerializeField] int angle = 45;

    [Range(0, 50)]
    [SerializeField] float range = 1.0f;

    [Range(0, 10)]
    [SerializeField] float speed = 1.0f;

    [SerializeField] Sanity sanity;
    // Start is called before the first frame update
    void Start()
    {
        pointPatrolLeft += transform.position;
        pointPatrolRight += transform.position;


        transform.position = pointPatrolLeft;
        pointToPatrolTo = pointPatrolRight;
    }

    public void OnDrawGizmos()
    {

        Vector3 left = transform.position + new Vector3(Mathf.Cos((angle - 90) * Mathf.Deg2Rad), Mathf.Sin((angle - 90) * Mathf.Deg2Rad)) * range;

        Vector3 right = transform.position + new Vector3(Mathf.Cos((-angle - 90) * Mathf.Deg2Rad), Mathf.Sin((-angle - 90) * Mathf.Deg2Rad)) * range;


    
        Gizmos.DrawLine(pointPatrolLeft + transform.position, pointPatrolRight + transform.position);

        Gizmos.DrawLine(transform.position, left);
        Gizmos.DrawLine(transform.position, right);
        Gizmos.DrawWireSphere(transform.position, range);
        //Handles.DrawBezier(left, right, new Vector3(left.y, -left.x), new Vector3(-right.y, right.x), Color.red, Texture2D.whiteTexture, 1f);
    }

    // Update is called once per frame
    void Update()
    {


        if (Vector3.Distance(transform.position, pointToPatrolTo) <= 0.1f)
        {
            if (Vector3.Distance(transform.position, pointPatrolLeft) <= 0.1f) pointToPatrolTo = pointPatrolRight;
            else pointToPatrolTo = pointPatrolLeft;
        }
        else
        {
            Vector3 distance = (pointToPatrolTo - transform.position).normalized;
            transform.position += distance * Time.deltaTime * speed;
        }


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

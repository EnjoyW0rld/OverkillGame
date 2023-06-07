using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryLine : MonoBehaviour
{

    float zValueLine;

    [SerializeField] float seconds;
    [SerializeField] int amountPositions;

    [SerializeField] JumpFrog body;

    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = amountPositions;
        zValueLine = body.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
        float dt = seconds / amountPositions;

        for (int i = 0; i < amountPositions; i++)
        {


            Vector3 originalPosition = body.gameObject.transform.position;
            Vector3 velocity = body.GetPredictedVelocity();

            Vector3 positionILine;
            //Master Formula for line trajectory with gravity enabled
            positionILine = new Vector3(originalPosition.x + velocity.x * (i * dt), originalPosition.y + (velocity.y * (i*dt)) + ((Physics.gravity.y * Mathf.Pow(i * dt, 2)) / 2), zValueLine);

            lineRenderer.SetPosition(i, positionILine);
        }



        
    }
}

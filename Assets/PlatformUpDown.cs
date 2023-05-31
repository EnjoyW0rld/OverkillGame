using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Obsolete("Replaced with ObjectMoveOnLine, as it's more generic")]
public class PlatformUpDown : MonoBehaviour
{
    [SerializeField] float YMax = 5;
    [SerializeField] float YMin = 5;

    [SerializeField, Range(0,1)] float currentPositionPercentage;
    [SerializeField] float speed = .1f;

    [SerializeField] bool goUp = false;

    Vector3 topPoint;
    Vector3 bottomPoint;


    // Start is called before the first frame update
    void Start()
    {
        topPoint = this.transform.position + new Vector3(0, YMax);
        bottomPoint = this.transform.position - new Vector3(0, YMin);

        startPosition = this.transform.position;

        if (goUp) speed *= -1f;

    }

    // Update is called once per frame
    void Update()
    {
        currentPositionPercentage += Time.deltaTime * speed;

        currentPositionPercentage = Mathf.Clamp(currentPositionPercentage, 0, 1);

        if (currentPositionPercentage == 0 || currentPositionPercentage == 1) speed *= -1;
        this.transform.position = Vector3.Lerp(topPoint, bottomPoint, currentPositionPercentage);

    }


    Vector3 startPosition;

    public void OnDrawGizmos()
    {
        Vector3 topPoint;
        Vector3 bottomPoint;
        if (startPosition == Vector3.zero){
             topPoint = this.transform.position + new Vector3(0, YMax);
             bottomPoint = this.transform.position - new Vector3(0, YMin);
        Debug.Log(topPoint);
        }
        else
        {
             topPoint = startPosition + new Vector3(0, YMax);
             bottomPoint = startPosition - new Vector3(0, YMin);
        }

        Gizmos.DrawCube(Vector3.Lerp(topPoint, bottomPoint, currentPositionPercentage), Vector3.one);

        Debug.Log(topPoint);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(topPoint, bottomPoint);

        // Gizmos.DrawLine(this.transform.position, this.transform.position + Vector3.up);

        Gizmos.color = Color.yellow;

    }
}

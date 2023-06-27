using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveOnLine : MonoBehaviour
{
    private LineForObjects lineToFollow;

    //Add editor to put the object at the right position
    [SerializeField, Range(0, 1)] protected float currentPositionPercentage;

    private Vector3 topPoint;
    private Vector3 bottomPoint;

    [SerializeField] private float speed = .1f;

    [SerializeField] private bool goLeft;
    // Start is called before the first frame update


    public void Start()
    {
        lineToFollow = GetComponentInParent<LineForObjects>();

        if (lineToFollow == null) Debug.LogError("No Line to Follow for " + this.name + "\b Add a LineForObjects on this parent");

        topPoint = lineToFollow.GetLeftGlobalPosition();
        bottomPoint = lineToFollow.GetRightGlobalPosition();

        if (goLeft) speed *= -1f;
    }


    // Update is called once per frame
    void Update()
    {
        MoveOnLine();
    }
    public void MoveOnLine()
    {
        currentPositionPercentage += Time.deltaTime * speed;

        //Clamps as lerp is being used to set the position
        currentPositionPercentage = Mathf.Clamp(currentPositionPercentage, 0, 1);

        //Exact values can be used because of the clamp above 
        if (currentPositionPercentage == 0 || currentPositionPercentage == 1) speed *= -1;


        this.transform.position = Vector3.Lerp(topPoint, bottomPoint, currentPositionPercentage);
    }


    public void SetObjectAtPercentageLine()
    {
        this.transform.position = Vector3.Lerp(topPoint, bottomPoint, currentPositionPercentage);
    }

#if UNITY_EDITOR

    /// <summary>
    /// EditorScript to set the object on the line, easier to see where it would start when the game starts.
    /// </summary>
    public void EditorSetObjectAtPercentageLine()
    {
        if (lineToFollow == null) lineToFollow = GetComponentInParent<LineForObjects>();


        topPoint = lineToFollow.GetLeftGlobalPosition();
        bottomPoint = lineToFollow.GetRightGlobalPosition();
        this.transform.position = Vector3.Lerp(topPoint, bottomPoint, currentPositionPercentage);
    }
#endif


    public float getCurrentPositionPercentage()
    {
        return currentPositionPercentage;
    }
    /// <summary>
    /// Return true if moving right
    /// </summary>
    /// <returns></returns>
    public bool GetDirection() => speed > 0;

    public void OnDrawGizmos()
    {

        Vector3 startPoint = Vector3.Lerp(topPoint, bottomPoint, currentPositionPercentage);


        Vector3 direction = Vector3.up * speed;
        Vector3 lineRight = (Vector3.down + Vector3.right).normalized * 0.5f;
        Vector3 lineLeft = (Vector3.down + Vector3.left).normalized * 0.5f;

        if (!goLeft)
        {
            direction *= -1f;

            lineRight.y *= -1f;
            lineLeft.y *= -1f;

        }

        Vector3 point = startPoint + direction;

        Gizmos.DrawLine(startPoint, point);
        Gizmos.DrawLine(point, lineRight + point);
        Gizmos.DrawLine(point, lineLeft + point);
    }


}

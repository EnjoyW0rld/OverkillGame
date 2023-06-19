using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

[RequireComponent(typeof(Damagable))]
public class Drone : MonoBehaviour
{

    private Transform player;

    [Range(0, 90)]
    [SerializeField] int angle = 45;

    [Range(0, 50)]
    [SerializeField] float range = 1.0f;

    [SerializeField] float reduceSanitySpeed = 2.0f;

    [Header("Events")]
    [SerializeField] private UnityEvent OnPlayerSpotted;
    [SerializeField] private UnityEvent OnPlayerLeftRange;

    private Vector3[] endRayPoints;
    private float rayDist;
    //private Sanity sanity;
    private Damagable damagable;
    bool playerInRange = false;

    bool appliedModifier;

    private void Start()
    {
        damagable = GetComponent<Damagable>();
        player = GameObject.FindGameObjectWithTag("Body").transform;
        endRayPoints = GetRayPoints();
        rayDist = Mathf.Abs(endRayPoints[0].x - transform.position.x);
        //sanity = GameObject.FindObjectOfType<Sanity>();
    }

    public void OnDrawGizmos()
    {

        Vector3 left = transform.position + new Vector3(Mathf.Cos((angle - 90) * Mathf.Deg2Rad), Mathf.Sin((angle - 90) * Mathf.Deg2Rad)) * range;

        Vector3 right = transform.position + new Vector3(Mathf.Cos((-angle - 90) * Mathf.Deg2Rad), Mathf.Sin((-angle - 90) * Mathf.Deg2Rad)) * range;

        Gizmos.color = playerInRange ? Color.blue : Color.white;

        //Gizmos.DrawWireSphere(transform.position, range);

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -range, 0));

        Vector3 rightOne = transform.position + new Vector3(Mathf.Cos((angle - 90) * Mathf.Deg2Rad), -range, 0);
        Vector3 leftOne = transform.position + new Vector3(Mathf.Cos((-angle - 90) * Mathf.Deg2Rad), -range, 0);
        //Gizmos.DrawLine(transform.position, rightOne);
        //Gizmos.DrawLine(transform.position, leftOne);
        //Gizmos.DrawLine(left, right);
        //Gizmos.DrawLine(transform.position, left);
        //Gizmos.DrawLine(transform.position, right);
        //  Gizmos.DrawWireSphere(transform.position, range);
        Vector3[] points = null;
        if (endRayPoints != null)
        {
            points = endRayPoints;
        }
        else
        {
        }
            points = GetRayPoints();
        Gizmos.DrawLine(transform.position, points[0]);
        Gizmos.DrawLine(transform.position, points[1]);


    }

    // Update is called once per frame
    void Update()
    {
        endRayPoints[0].x = transform.position.x + rayDist;
        endRayPoints[1].x = transform.position.x - rayDist;
        playerInRange = IsDroneSeeingPlayer();
        if (playerInRange)
        {
            OnPlayerSpotted?.Invoke();
            damagable.OnEnterDamageArea();
            appliedModifier = true;
        }
        else if (appliedModifier && !playerInRange)
        {
            OnPlayerLeftRange?.Invoke();
            damagable.OnExitDamageArea();
            appliedModifier = false;
        }
    }

    //Check IF ANGLE IS VIALBE
    private bool IsDroneSeeingPlayer()
    {
        if (!IsInsideTriangular()) return false;

        if (!IsNothingBetweenDroneAndPlayer()) return false;

        Debug.Log("Seeing!");
        return true;

    }

    private bool IsNothingBetweenDroneAndPlayer()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 dronePosition = this.transform.position;


        Vector3 direction = playerPos - dronePosition;

        if (!Physics.Raycast(this.transform.position, direction, out RaycastHit hitInfo, range)) 
        {
            Debug.LogError("Couldn't see player in range from drone");
            return false;
        }

        return hitInfo.collider.gameObject == player.gameObject;
    }

    private bool IsInsideTriangular()
    {
        Vector3 playerPos = player.transform.position;

        if (playerPos.y > transform.position.y || playerPos.y < transform.position.y - range)
        {
            return false;
        }

        Vector3 dir = endRayPoints[0] - transform.position;
        float secondZ = Vector3.Cross((endRayPoints[1] - transform.position).normalized, (playerPos - transform.position).normalized).z;
        float firstZ = Vector3.Cross(dir.normalized, (playerPos - transform.position).normalized).z;

        if (firstZ > 0 && secondZ > 0 || firstZ < 0 && secondZ < 0)
        {
            return false;
        }
        return true;
        //return IsInsideRadial();
    }
    private bool IsInsideRadial()
    {
        Vector3 vector = player.position - transform.position;

        float angleDiference = Mathf.Atan(vector.y / vector.x);

        float angleDeg = angleDiference * Mathf.Rad2Deg;

        //     Debug.Log("______");
        //     Debug.Log(angleDeg);
        //     Debug.Log(vector.magnitude);

        bool leftCor = (angleDeg >= -90 && angleDeg <= -(90 - angle));
        bool rightCor = (angleDeg >= (90 - angle) && angleDeg <= 90);


        if ((leftCor || rightCor) && vector.magnitude < range)
        {
            if (!playerInRange)
            {
                //sanity.ChangeSanitySpeed(reduceSanitySpeed);
            }
            return true;
            //    Debug.Log("InArea");
        }
        else
        {
            //sanity.ResetSanitySpeed();
            return false;
        }
    }

    public float GetRange() => range;
    public Vector3[] GetRayPoints()
    {
        Plane plane = new Plane(Vector3.up, transform.position - new Vector3(0, range, 0));
        Ray ray = new Ray(transform.position, (new Vector3(Mathf.Cos((angle - 90) * Mathf.Deg2Rad), Mathf.Sin((angle - 90) * Mathf.Deg2Rad), 0)));
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 point = ray.GetPoint(enter);
            Vector3[] points =
            {
                point,
                new Vector3(point.x +(transform.position.x - point.x) * 2,point.y,point.z)
            };
            return points;
        }
        return null;
    }
}

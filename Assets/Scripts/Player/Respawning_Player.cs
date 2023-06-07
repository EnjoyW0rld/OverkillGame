using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning_Player : MonoBehaviour
{

    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private Vector3 lastGrassPoint;
    [SerializeField] LayerMask mask;


    private void Start()
    {
        FindObjectOfType<Sanity>().OnZeroSanity.AddListener(RespawnAtGrassPoint);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            RespawnAtLastPosition();
        }
    }

    public void FixedUpdate()
    {
        if (Physics.Raycast(this.transform.position, (Vector3.down + Vector3.right).normalized, out RaycastHit hit, 3f, mask))
        {

            Transform firstHit = hit.transform;
            if (Physics.Raycast(this.transform.position, (Vector3.down + Vector3.left).normalized, out RaycastHit hit2, 3f, mask))
            {

                if (firstHit != hit2.transform) return;
                lastPosition = transform.position + Vector3.up;
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, (Vector3.down + Vector3.right).normalized * 3f);
        Gizmos.DrawRay(this.transform.position, (Vector3.down + Vector3.left).normalized * 3f);
    }

    public void SetGrassPoint(Vector3 point) => lastGrassPoint = point;
    public void RespawnAtGrassPoint()
    {
        transform.position = lastGrassPoint;
    }
    public void RespawnAtLastPosition()
    {
        transform.position = lastPosition;
    }
}

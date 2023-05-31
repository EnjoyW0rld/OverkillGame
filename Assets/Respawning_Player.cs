using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning_Player : MonoBehaviour
{

    [SerializeField] Vector3 lastPosition;

    [SerializeField] LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            Respawn();
        }
    }

    public void FixedUpdate()
    {
        if (Physics.Raycast(this.transform.position, (Vector3.down + Vector3.right).normalized, out RaycastHit hit ,3f, mask))
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


    public void Respawn()
    {
        transform.position = lastPosition;
    }
}

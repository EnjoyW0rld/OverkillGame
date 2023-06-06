using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawning_Player : MonoBehaviour
{

    [SerializeField] Vector3 lastPosition;

    //Mask where the player is allowed to repspawn on (Mostly platform)
    [SerializeField] LayerMask mask;

    // Update is called once per frame
    void Update()
    {
        //Manual Respawn key TODO: add one for controller
        if (Input.GetKeyUp(KeyCode.R))
        {
            Respawn();
        }
    }

    
    public void FixedUpdate()
    {
        TrySaveRespawn();
    }

    public bool CheckIfCanRespawn()
    {
        if (Physics.Raycast(this.transform.position, (Vector3.down + Vector3.right).normalized, out RaycastHit hit, 3f, mask))
        {

            Transform firstHit = hit.transform;
            if (Physics.Raycast(this.transform.position, (Vector3.down + Vector3.left).normalized, out RaycastHit hit2, 3f, mask))
            {
                //Makes sure the player can't respawn between two platforms
                if (firstHit != hit2.transform) return false;

                return true;
            }
        }

        return false;
    }

    public void SaveRespawn()
    {
        lastPosition = transform.position + Vector3.up;
    }

    public bool TrySaveRespawn()
    {
        if (CheckIfCanRespawn())
        {
            SaveRespawn();
            return true;
        }
        return false;
    }

    //Draws the ray it uses to check
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

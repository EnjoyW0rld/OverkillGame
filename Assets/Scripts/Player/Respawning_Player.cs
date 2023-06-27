using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Respawning_Player : MonoBehaviour
{


    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private Vector3 lastGrassPoint;
    [SerializeField] LayerMask mask;


    [SerializeField] UnityEvent onRespawnTest;
    [SerializeField] UnityEvent onRespawn;


    private void Start()
    {
        FindObjectOfType<Sanity>().OnZeroSanity.AddListener(FaderRespawnGrassPoint);
        lastGrassPoint = transform.position;
    }

    void Update()
    {
        //Manual Respawn key TODO: add one for controller
        if (Input.GetKeyUp(KeyCode.R))
        {
            RespawnAtLastPosition();
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

    public void SetGrassPoint(Vector3 point) => lastGrassPoint = point;
    public void RespawnAtGrassPoint()
    {
        transform.position = lastGrassPoint;
        onRespawn?.Invoke();
    }
    public void RespawnAtLastPosition()
    {
        transform.position = lastPosition;
        onRespawn?.Invoke();
    }

    public void FaderRespawn()
    {
        onRespawnTest?.Invoke();
        FadeInOut.Instance.Fade(RespawnAtLastPosition);
    }

    public void FaderRespawnGrassPoint()
    {
        onRespawnTest?.Invoke();
        FadeInOut.Instance.Fade(RespawnAtGrassPoint);
    }
}

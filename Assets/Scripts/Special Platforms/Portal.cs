using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Portal : MonoBehaviour
{
    [SerializeField] private string bodyTag = "Body";
    [SerializeField] private Portal connectedPortal;
    [SerializeField] private Vector2 endPoint;

    private bool canTeleport = false;
    private GameObject player;

    private void Awake()
    {
        
        Collider collider = GetComponent<Collider>();
        collider.isTrigger = true;

        if (connectedPortal == null) Debug.LogError("Portal: " + this.name + " doesn't have a connection. Add a connection!!!");
    }


    public void Update()
    {
        //No Portal Connected nothing to do
        if (connectedPortal == null) return;

        if (Input.GetKeyDown(KeyCode.T) && canTeleport)
        {
            player.transform.position = (new Vector3(connectedPortal.endPoint.x, connectedPortal.endPoint.y, 0) + connectedPortal.transform.position);
        }
    }

    //OnTriggers set the bool, as you can't detect buttonpresses in ontriggers (doesn't run every frame)

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == bodyTag)
        {
            canTeleport = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == bodyTag)
        {
            canTeleport = false;

        }
    }


    public Vector3 getEndPoint()
    {
        return endPoint;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Portal : MonoBehaviour
{
    [SerializeField] private string bodyTag = "Body";
    [SerializeField] private Portal connectedPortal;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == bodyTag)
        {
            print("beeep");
        }
    }
}
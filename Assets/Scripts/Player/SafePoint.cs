using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    private Sanity sanity;
    private void Start()
    {
        sanity = FindObjectOfType<Sanity>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Body")
        {
            //sanity.ResetSanityAmount();
            //print(other.GetComponent<Respawning_Player>());
            //other.GetComponent<Respawning_Player>().SetGrassPoint(transform.position);
        }
        
    }
}

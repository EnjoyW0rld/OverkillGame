using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{


    public void OnTriggerStay(Collider other)
    {
        Debug.LogError(other.gameObject.name);

        if (other.gameObject.TryGetComponent<Sanity>(out Sanity sanity))
        {
            sanity.AddSanity(10);
            Debug.Log("WORKJS");
        }
    }
}

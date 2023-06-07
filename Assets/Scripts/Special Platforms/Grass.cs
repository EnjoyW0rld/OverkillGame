using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{


    public void OnTriggerStay(Collider other)
    {


        if (other.gameObject.TryGetComponent<Sanity>(out Sanity sanity))
        {
            //Debug.Log("test");
            sanity.AddSanity(10);

        }
    }
}

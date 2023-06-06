using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPlatformJitter : MonoBehaviour
{

    //Doesn't work as player character consists of a lot of different elements

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Body") || collision.gameObject.CompareTag("Leg"))
        {
          //  collision.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
     //   collision.gameObject.transform.SetParent(null);
    }

}


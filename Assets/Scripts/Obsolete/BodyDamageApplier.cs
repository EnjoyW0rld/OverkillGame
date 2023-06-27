using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Obsolete][RequireComponent(typeof(Sanity),typeof(Collider))]
public class BodyDamageApplier : MonoBehaviour
{
    private Sanity sanity;
    
    private void Start()
    {
        sanity = GetComponent<Sanity>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Damagable>(out Damagable dam))
        {
            //Damagable.DecreaseType type = dam.GetDecreaseType();
            
        }
    }

}

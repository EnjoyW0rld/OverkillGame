using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damagable : MonoBehaviour
{
    [SerializeField, Tooltip("If true will remove some sanity instantly, false will make draining speed faster")]
    public bool _instant;
    [SerializeField] private string _playerTag = "Body";

    //Gradual decrease
    [SerializeField] private float _decreaseTime;

    //Instant decrease

    private Sanity _sanity;

    //Private functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == _playerTag)
        {
        }
    }
    private void DecreaseSanity()
    {

    }

    //Public functions
    public void Initialize(Sanity sanity) => sanity = _sanity;
}

[Serializable]
public class GradualDecrease
{
    public float _decreaseTime;
    [Min(0)] public float _reductionSpeed;
}

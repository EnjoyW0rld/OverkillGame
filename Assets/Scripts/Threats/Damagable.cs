using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damagable : MonoBehaviour
{
    //Enums to choose damage type
    private enum DecreaseType { Instant = 0, Gradual = 1};
    [SerializeField] private DecreaseType _decreaseType;
    
    //[SerializeField, Tooltip("If true will remove some sanity instantly, false will make draining speed faster")]
    //private bool _instant;
    [SerializeField] private string _playerTag = "Body";

    //[SerializeField,ReadOnly] private string aa = "sss";
    
    
    //Gradual decrease
    [SerializeField] private GradualDecrease _gradualDecrease;
    //Instant decrease
    [SerializeField] private InstantDecrease _instantDecrease;

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
    [SerializeField] public float _decreaseTime;
    [SerializeField, Min(0)] public float _reductionSpeed;
}

[Serializable]
public class InstantDecrease
{
    [SerializeField] public float _valueToDecrease;
}

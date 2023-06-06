using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damagable : MonoBehaviour
{

    //Enums to choose damage type
    public enum DecreaseType { Instant = 0, Gradual = 1 };
    [SerializeField] private DecreaseType decreaseType;

    [SerializeField] private string playerTag = "Body";
    [ReadMe("If it is true, object will aplly damage when touches the body, otherwise you need to call functions manually")]
    [SerializeField] private bool executeAutomatically;

    //Gradual decrease
    [SerializeField, HideIf("decreaseType", DecreaseType.Gradual, HideIfAttribute.Comparison.NotEquals)]
    private GradualDecrease gradualDecrease;
    //Instant decrease
    [SerializeField, HideIf("decreaseType", DecreaseType.Instant, HideIfAttribute.Comparison.NotEquals)]
    private InstantDecrease instantDecrease;


    private Sanity sanity;

    //Private functions
    private void OnCollisionEnter(Collision collision)
    {
        if (executeAutomatically && collision.transform.tag == playerTag)
        {
            DecreaseSanity();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (executeAutomatically && collision.transform.tag == playerTag)
        {
            if (decreaseType == DecreaseType.Gradual)
            {
                sanity.ResetSanitySpeed();
            }
        }
    }


    private void DecreaseSanity()
    {
        print("Decrease sanity invoked");
        switch (decreaseType)
        {
            case DecreaseType.Instant:
                instantDecrease.ApplyDamage(sanity);
                break;
            case DecreaseType.Gradual:
                gradualDecrease.ApplyDamage(sanity);
                break;
        }
    }

    //Public functions
    public void Initialize(Sanity sanity) => this.sanity = sanity;
    public DecreaseType GetDecreaseType() => decreaseType;

    public void OnEnterDamageArea()
    {
        switch (decreaseType)
        {
            case DecreaseType.Instant:
                instantDecrease.ApplyDamage(sanity);
                break;
            case DecreaseType.Gradual:
                gradualDecrease.ApplyDamage(sanity);
                break;
        }
    }
    public void OnExitDamageArea()
    {
        if (decreaseType == DecreaseType.Gradual)
        {
            sanity.ResetSanitySpeed();
        }
    }
}

[Serializable]
public class GradualDecrease
{
    //[SerializeField] public float _decreaseTime;
    [SerializeField, Min(0)] public float _reductionSpeed;
    public void ApplyDamage(Sanity sanity)
    {
        sanity.ChangeSanitySpeed(_reductionSpeed);
    }
}

[Serializable]
public class InstantDecrease
{
    [SerializeField] public float _valueToDecrease;
    public void ApplyDamage(Sanity sanity)
    {
        sanity.DecreaseSanity(_valueToDecrease);
    }
}

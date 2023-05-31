using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Damagable : MonoBehaviour
{

    //Enums to choose damage type
    private enum DecreaseType { Instant = 0, Gradual = 1 };
    [SerializeField] private DecreaseType _decreaseType;

    //[SerializeField, ReadMe] private string _readMe = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum";
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
            DecreaseSanity();
        }
    }

    private void DecreaseSanity()
    {
        print("Decrease sanity invoked");
        switch (_decreaseType)
        {
            case DecreaseType.Instant:
                _instantDecrease.ApplyDamage(_sanity);
                break;
            case DecreaseType.Gradual:
                _gradualDecrease.ApplyDamage(_sanity);
                break;
        }
    }

    //Public functions
    public void Initialize(Sanity sanity) => _sanity = sanity;
}

[Serializable]
public class GradualDecrease
{
    [SerializeField] public float _decreaseTime;
    [SerializeField, Min(0)] public float _reductionSpeed;
    public void ApplyDamage(Sanity sanity)
    {
        sanity.ChangeSanitySpeed(_reductionSpeed, _decreaseTime);
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

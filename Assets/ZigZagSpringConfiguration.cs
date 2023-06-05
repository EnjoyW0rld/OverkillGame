using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagSpringConfiguration : MonoBehaviour
{
    [SerializeField, Min(0)] public float springStrenght = 25;
    [SerializeField, Min(0)] public float damper = 0.2f;
    [SerializeField, Min(0)] public float tolerance = 0.025f;

    [SerializeField] public bool HideSpringsInInspector = false;

   


    public void SetStrenghtToStrings()
    {
        SpringJoint[] springs = this.GetComponentsInChildren<SpringJoint>();

        foreach(SpringJoint spring in springs)
        {
            spring.spring = springStrenght;
        }
    }

    public void SetDamperToStrings()
    {
        SpringJoint[] springs = this.GetComponentsInChildren<SpringJoint>();

        foreach (SpringJoint spring in springs)
        {
            spring.damper = damper;
        }
    }

    public void SetToleranceToStrings()
    {
        SpringJoint[] springs = this.GetComponentsInChildren<SpringJoint>();

        foreach (SpringJoint spring in springs)
        {
            spring.tolerance = tolerance;
        }
    }
}

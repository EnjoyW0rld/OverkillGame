using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour : BodyAffecter
{
    [ReadMe("Apply this is script to the platform you want to be bouncy")]
    [SerializeField] private float jumpMultiplier = 1.5f;

    protected override void SetExpression()
    {
        expression = x => x * jumpMultiplier;
    }
}
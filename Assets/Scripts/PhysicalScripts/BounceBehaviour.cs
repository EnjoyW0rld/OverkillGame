using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour : BodyAffecter
{
    [ReadMe("Some important info")]
    [SerializeField] private float jumpMultiplier = 1.5f;
    protected override void SetExpression()
    {
        expression = x => x * jumpMultiplier;
    }
}

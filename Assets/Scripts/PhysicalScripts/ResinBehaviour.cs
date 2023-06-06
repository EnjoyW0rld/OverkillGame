using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResinBehaviour : BodyAffecter
{
    [SerializeField] private float stengthDecreaseValue;

    protected override void SetExpression()
    {
        expression = x => x - stengthDecreaseValue;
    }
}

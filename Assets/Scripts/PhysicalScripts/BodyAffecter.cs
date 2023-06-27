using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyAffecter : MonoBehaviour
{
    protected Func<float, float> expression;

    private void Awake()
    {
        SetExpression();
    }

    /// <summary>
    /// You need to set value to "expression" lambda function
    /// </summary>
    abstract protected void SetExpression();
    public Func<float, float> GetExpression() => expression;
    public virtual void OnCollisionAction(JumpFrog frog)
    {

    }
}

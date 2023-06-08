using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour : BodyAffecter
{
    [ReadMe("Apply this is script to the platform you want to be bouncy")]
    [SerializeField] private float jumpMultiplier = 1.5f;
    [SerializeField] private float reloadTime = 2;
    [SerializeField] private float bounceModifier = 4;
    [SerializeField, Range(0, 1)] private float velocityPreserving;
    private bool reloaded = true;

    protected override void SetExpression()
    {
        expression = x => x * jumpMultiplier;
    }
    private IEnumerator SetCooldown()
    {
        yield return new WaitForSeconds(reloadTime);
        reloaded = true;
    }
    public override void OnCollisionAction(JumpFrog frog)
    {
        if (reloaded)
        {
            reloaded = false;
            Vector3 velocity = frog.GetVelocity();
            //Vector3 mirroredVel = Vector3.Reflect(velocity, Vector3.up) * bounceModifier;
            frog.ApplyForce(Vector3.up * velocity.magnitude);
            //float speed = mirroredVel.magnitude;
            //mirroredVel = mirroredVel.normalized * velocityPreserving + Vector3.up * (1 - velocityPreserving);
            //mirroredVel *= speed;

            //frog.ApplyForce(mirroredVel);
            //print("applying more velocity " + mirroredVel);
            StartCoroutine(SetCooldown());
        }
        //base.OnCollisionAction(frog);
    }
}
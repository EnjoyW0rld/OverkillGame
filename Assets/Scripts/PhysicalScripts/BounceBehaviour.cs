using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBehaviour : BodyAffecter
{
    [ReadMe("Some important info")]
    [SerializeField] private float jumpMultiplier = 1.5f;
    [SerializeField] private float reloadTime = 2;
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
            print(frog);
            Vector3 velocity = frog.GetVelocity();
            print(velocity);
            Vector3 mirroredVel = Vector3.Reflect(velocity, Vector3.up);
            frog.ApplyForce(mirroredVel);
            StartCoroutine(SetCooldown());
        }
        //base.OnCollisionAction(frog);
    }
}

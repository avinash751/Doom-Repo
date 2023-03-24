using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatingSteeringBehaviour : SteeringBehaviour
{
    public override void ApplyVelocityToTarget()
    {
        Vector3 targetVelocity = GetDirectionFromtarget() * -speed;
        rb.velocity += targetVelocity * Time.fixedDeltaTime;
    }
}

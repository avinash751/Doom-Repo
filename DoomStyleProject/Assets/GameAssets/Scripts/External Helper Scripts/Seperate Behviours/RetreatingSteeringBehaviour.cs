
using UnityEngine;

public class RetreatingSteeringBehaviour : SteeringBehaviour
{
 
    public override Vector3 GetDirectionFromtarget()
    {
        Vector3 direction = (transform.position - target.position);
        Vector3 normalisedDirection = direction.normalized;
        return normalisedDirection;
    }

   
}

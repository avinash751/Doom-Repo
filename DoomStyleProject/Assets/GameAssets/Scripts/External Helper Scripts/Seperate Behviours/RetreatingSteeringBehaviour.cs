
using UnityEngine;

public class RetreatingSteeringBehaviour : WayPoints
{
 
    public override Vector3 GetDirectionFromtarget()
    {
        Vector3 direction = (transform.position - target.position);
        Vector3 normalisedDirection = direction.normalized;
        return normalisedDirection;
    }
    protected override Vector3 GetDirectionTowardsFinalPathFromAstar()
    {
        if (followPath)
        {
            Vector3 goalDirection = transform.position -finalPath[currentPathIndex].worldPosition ;
            Vector3 normalisedGoalDirection = goalDirection.normalized;
            return normalisedGoalDirection;
        }
        return Vector3.zero;
    }
}

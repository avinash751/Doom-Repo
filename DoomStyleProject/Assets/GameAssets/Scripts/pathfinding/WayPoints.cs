using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : SteeringBehaviour
{
    protected Astar aStar;
    protected bool followPath;
    protected List<Node> finalPath;


    [SerializeField] protected Vector3 startposition;
    [SerializeField] protected Vector3 Goalposition;
    [SerializeField] protected int currentPathIndex;

    public override void Start()
    {
        base.Start();
        aStar = FindObjectOfType<Astar>();
        RunAstarAndSetStartAndGoalValues();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        FindNewpathBasedOnDistanceAndWhenTargetPositionChanges();
    }


    public override void ApplyVelocityToTarget()
    {
        if (followPath && currentPathIndex <= finalPath.Count)
        {

            Vector3 targetVelocity = GetDirectionTowardsFinalPathFromAstar() * speed;
            rb.velocity += targetVelocity * Time.fixedDeltaTime;
            IncrementPathIndexBasedOnDistance();

            return;
        }
        WhenFinalIndexIsMaxedMoveTowardsTarget();
    }
    void WhenFinalIndexIsMaxedMoveTowardsTarget()
    {
        base.ApplyVelocityToTarget();
    }
    protected virtual Vector3 GetDirectionTowardsFinalPathFromAstar()
    {
        if (followPath)
        {
            Vector3 goalDirection = finalPath[currentPathIndex].worldPosition - transform.position;
            Vector3 normalisedGoalDirection = goalDirection.normalized;
            return normalisedGoalDirection;
        }
        return Vector3.zero;
    }
    void IncrementPathIndexBasedOnDistance()
    {
        Vector3 currentNodePosition = finalPath[currentPathIndex].worldPosition;
        currentNodePosition.y = transform.position.y;
        float distance = Vector3.Distance(transform.position, currentNodePosition);

        if (distance <= 2f) currentPathIndex++;

        if (currentPathIndex >= finalPath.Count)
        {
            followPath = false;
            return;
        }
    }

    void FindNewpathBasedOnDistanceAndWhenTargetPositionChanges()
    {
        if (Goalposition == target.position) return;
        float distanceFromNewGoalPosition = Vector3.Distance(Goalposition,target.position);
        float distanceFromTarget = Vector3.Distance(transform.position,target.position);

        if (distanceFromNewGoalPosition > 2f && distanceFromTarget>2f)
        {
            RunAstarAndSetStartAndGoalValues();
            return;
        }
    }

    void RunAstarAndSetStartAndGoalValues()
    {
        startposition = transform.position;
        Goalposition = target.position;
        currentPathIndex = 0;
        followPath = aStar.FindBestPath(startposition, Goalposition, out finalPath);
    }
}

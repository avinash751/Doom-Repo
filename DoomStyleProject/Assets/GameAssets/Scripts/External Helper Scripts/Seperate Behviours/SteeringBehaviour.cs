using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(Rigidbody))]
public class SteeringBehaviour : MonoBehaviour
{

    public Transform target;
    public float speed;
    public float maxSpeed;
    public bool slowDownAtTarget = false;
    [Range(0, 2)] public float slowDownMultiplier;
    public float distanceThreshold;
    private protected Rigidbody rb;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void FixedUpdate()
    {
        MoveTowardsTarget();
    }

    public void MoveTowardsTarget()
    {
        ApplyVelocityToTarget();
        TruncateVelocity(maxSpeed);
        SlowDownIfTargetReached(distanceThreshold, slowDownMultiplier);
    }

    public virtual Vector3 GetDirectionFromtarget()
    {
        Vector3 direction = (target.position - transform.position);
        Vector3 normalisedDirection = direction.normalized;
        return normalisedDirection;
    }

    public virtual void ApplyVelocityToTarget()
    {
        Vector3 targetVelocity = GetDirectionFromtarget() * speed;
        transform.LookAt(target);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        rb.velocity += targetVelocity * Time.fixedDeltaTime;
    }

    protected void TruncateVelocity(float maxSpeed)
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    void SlowDownIfTargetReached(float distanceThreshold, float multiplier)
    {
        if (slowDownAtTarget && Vector3.Distance(transform.position, target.position) < distanceThreshold)
        {
            rb.velocity *= multiplier;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SteeringBehaviour))]
public class SeekBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SteeringBehaviour script = (SteeringBehaviour)target;


        script.target = (Transform)EditorGUILayout.ObjectField("Target", script.target, typeof(Transform), true);

        script.speed = EditorGUILayout.FloatField("Speed", script.speed);
        script.maxSpeed = EditorGUILayout.FloatField("Max Speed", script.maxSpeed);

        script.slowDownAtTarget = EditorGUILayout.Toggle("Slow Down At Target", script.slowDownAtTarget);

        if (script.slowDownAtTarget)
        {
            script.slowDownMultiplier = EditorGUILayout.FloatField("Slow Down Multiplier", script.slowDownMultiplier);
            script.distanceThreshold = EditorGUILayout.FloatField("Distance Threshold", script.distanceThreshold);
        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
#endif

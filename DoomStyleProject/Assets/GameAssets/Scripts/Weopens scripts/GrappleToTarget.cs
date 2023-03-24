using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Codice.Client.ChangeTrackerService.Win32Api;

public class GrappleToTarget : MonoBehaviour
{
    [SerializeField] new Transform camera;
    [SerializeField] Transform player;

    [SerializeField] LayerMask grabbable;

    [SerializeField] Vector3 targetPoint;

    [SerializeField] float maxDistance;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] float distanceFromPoint;
    [SerializeField] float distanceToStopGrappling;

    Rigidbody rb;

    [SerializeField] LineRenderer grappleVisual;

    [SerializeField] Transform grappleSpawnPoint;

    bool moveTowardsTarget;
    bool targetFound;
    [SerializeField] bool reachedTarget;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        camera = Camera.main.transform;
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            GetTarget();
            StartGrapple();
            Debug.Log("Grappling");
        }
        if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
            reachedTarget = false;
            grappleVisual.enabled = false;
            Debug.Log("Not grappling");
        }

    }

    private void LateUpdate()
    {
    }

    void StartGrapple()
    {
        if (targetFound && !reachedTarget)
        {
            DrawGrappleLineTowardsTarget();
            MoveTowardsTarget();
            CheckIfPlayerReachedTarget();
            rb.useGravity = false;
            Debug.Log("moving towards target");
        }
    }
    void MoveTowardsTarget()
    {
        Vector3 direction = targetPoint - player.position;
        Vector3 normalizeDirection = direction.normalized;
        //rb.velocity += normalizeDirection * speed;
        rb.AddForce(normalizeDirection * speed, ForceMode.Impulse);
        /*TruncateGrappleVelocity();*/
    }

    void CheckIfPlayerReachedTarget()
    {
        distanceFromPoint = Vector3.Distance(player.position, targetPoint);
        if (distanceFromPoint <= distanceToStopGrappling)
        {
            reachedTarget = true;
            StopGrapple();
        }

    }
    void TruncateGrappleVelocity()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }
    void StopGrapple()
    {
        targetFound = false;
        rb.velocity = Vector3.zero;
        grappleVisual.enabled = false;
    }

    void GetTarget()
    {
        RaycastHit hit;
        if (!targetFound && !reachedTarget)
        {
            if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, grabbable) && gameObject.activeInHierarchy)
            {
                targetFound = true;
                targetPoint = hit.point;
                Debug.Log("Finding Target");
            }
        }
    }

    void DrawGrappleLineTowardsTarget()
    {
        if (targetPoint != Vector3.zero)
        {
            grappleVisual.enabled = true;
            grappleVisual.SetPosition(0, grappleSpawnPoint.position);
            grappleVisual.SetPosition(1, targetPoint);
        }
        
    }
    private void OnDisable()
    {
        grappleVisual.enabled = false;
    }
}

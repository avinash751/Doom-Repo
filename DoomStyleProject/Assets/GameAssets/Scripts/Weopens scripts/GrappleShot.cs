using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleShot : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] SpringJoint joint;

    [SerializeField] LayerMask grabbable;

    private Vector3 grapplePoint;

    [SerializeField] Transform gunPoint;
    [SerializeField] new Transform camera;
    [SerializeField] Transform player;

    [SerializeField] float maxDistance;
    [SerializeField] float maxDistanceMultiplier;
    [SerializeField] float minDistanceMultiplier;
    [SerializeField] float spring;
    [SerializeField] float damper;
    [SerializeField] float massScale;


    private void Awake()
    {
        /* lineRenderer = GetComponent<LineRenderer>();*/
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrappling();
        }
    }

    private void LateUpdate()
    {
        /* DrawGrapple();*/
    }
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, grabbable) && gameObject.activeInHierarchy)
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * maxDistanceMultiplier;
            joint.minDistance = distanceFromPoint * minDistanceMultiplier;

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            Debug.DrawRay(camera.position, camera.forward * hit.distance, Color.green);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(camera.position + camera.forward * maxDistance, 1f);
    }

  /*  void DrawGrapple()
    {
        if (!joint)
        {
            return;
        }
        lineRenderer.SetPosition(0, gunPoint.position);
        lineRenderer.SetPosition(1, grapplePoint);
    }*/

    void StopGrappling()
    {
        /*  lineRenderer.positionCount = 0;*/
        Destroy(joint);
    }


}

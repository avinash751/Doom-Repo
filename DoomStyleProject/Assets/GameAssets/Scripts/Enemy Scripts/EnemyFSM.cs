using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    Transform Player;
    SteeringBehaviour SeekBehaviour;
    RetreatingSteeringBehaviour RetreatBehaviour;
    Wanderer wanderBehaviour;
    AiShooting shootingBehaviour;

    [SerializeField] float distanceToStartChasing;
    [SerializeField] float distanceToStartRetreating;
    [SerializeField] float distanceToStartShooting;
    [SerializeField] int bulletDamage;
    [SerializeField] float DistanceFromPlayer;

    void Start()
    {
        Player = FindObjectOfType<FpsController>().GetComponent<Transform>();

        SeekBehaviour = GetComponent<SteeringBehaviour>();
        SeekBehaviour.target = Player;
        SeekBehaviour.enabled = false;

        RetreatBehaviour = GetComponent<RetreatingSteeringBehaviour>();
        RetreatBehaviour.target = Player;
        RetreatBehaviour.enabled = false;

        wanderBehaviour = GetComponent<Wanderer>();
        wanderBehaviour.enabled = false;

        shootingBehaviour = GetComponent<AiShooting>();
        shootingBehaviour.enabled = false;

    }

    // Update is called once per frame
    virtual public void Update()
    {
        CalculateDistanceFromPlayer();
        SeekTowardsPlayer();
        RetreatFromPlayer();
        StartWandering();
        StartShooting();
    }
    void CalculateDistanceFromPlayer()
    {
        DistanceFromPlayer = Vector3.Distance(transform.position, Player.position);

    }


    void SeekTowardsPlayer()
    {
        bool CanSeekPlayer = DistanceFromPlayer <= distanceToStartChasing && DistanceFromPlayer > distanceToStartRetreating && DistanceFromPlayer > distanceToStartShooting;

        if (CanSeekPlayer)
        {
            SetColorOfMesh(Color.red);
            SeekBehaviour.enabled = true;
            SeekBehaviour.MoveTowardsTarget();
            return;
        }
        SeekBehaviour.enabled = false;
    }

    void RetreatFromPlayer()
    {
        if (DistanceFromPlayer <= distanceToStartRetreating)
        {
            SetColorOfMesh(Color.yellow);
            RetreatBehaviour.enabled = true;
            RetreatBehaviour.MoveTowardsTarget();
            return;
        }
        RetreatBehaviour.enabled = false;
    }

    void StartWandering()
    {
        if (DistanceFromPlayer > distanceToStartChasing)
        {
            SetColorOfMesh(Color.white);
            wanderBehaviour.enabled = true;
            return;
        }
        wanderBehaviour.enabled = false;
    }

    void StartShooting()
    {
        bool CanShoot = DistanceFromPlayer <= distanceToStartShooting && DistanceFromPlayer > distanceToStartRetreating;

        if (CanShoot)
        {
            SetColorOfMesh(Color.black);
            Debug.Log("shooting started");
            transform.LookAt(Player);
            shootingBehaviour.ShootObject(bulletDamage);
            return;
        }
    }

    void SetColorOfMesh(Color color)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }



}

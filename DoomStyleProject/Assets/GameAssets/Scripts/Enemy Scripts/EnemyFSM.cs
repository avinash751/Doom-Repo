using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    Transform Player;
    SteeringBehaviour SeekBehaviour;
    RetreatingSteeringBehaviour Retreatbehaviour;
    Wanderer wanderBehaviour;

    [SerializeField] float distanceToStartChasing;
    [SerializeField] float distanceToStartRetreating;
    [SerializeField] float DistanceFromPlayer;
    void Start()
    {
        Player = FindObjectOfType<FpsController>().GetComponent<Transform>();

        SeekBehaviour = GetComponent<SteeringBehaviour>(); 
        SeekBehaviour.target = Player;
        SeekBehaviour.enabled = false;

        Retreatbehaviour = GetComponent<RetreatingSteeringBehaviour>();
        Retreatbehaviour.target = Player;
        Retreatbehaviour.enabled = false;

        wanderBehaviour = GetComponent<Wanderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateDistanceFromPlayer();
        SeekTowardsPlayer();
        RetreatFromPlayer();
        StartWandering();
    }
    void CalculateDistanceFromPlayer()
    {
        DistanceFromPlayer = Vector3.Distance(transform.position, Player.position);

    }
    void SeekTowardsPlayer()
    {
        bool CanSeekPlayer = DistanceFromPlayer <= distanceToStartChasing && DistanceFromPlayer > distanceToStartRetreating;

        if (CanSeekPlayer)
        {
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
            Retreatbehaviour.enabled = true;
            return;
        }
        Retreatbehaviour.enabled = false;
    }

    void StartWandering()
    {
        if(DistanceFromPlayer> distanceToStartChasing)
        {
            wanderBehaviour.enabled = true;
            return;
        }
        wanderBehaviour.enabled = false;
    }

   
}

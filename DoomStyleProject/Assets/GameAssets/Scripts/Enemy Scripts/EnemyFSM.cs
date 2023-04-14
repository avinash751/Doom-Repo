using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
     Transform Player;
     [SerializeField]WayPoints SeekBehaviour;
     [SerializeField]RetreatingSteeringBehaviour RetreatBehaviour;
     [SerializeField]Wanderer wanderBehaviour;
     [SerializeField]AiShooting shootingBehaviour;

    [SerializeField] float distanceToStartChasing;
    [SerializeField] float distanceToStartRetreating;
    [SerializeField] float distanceToStartShooting;
    [SerializeField] int bulletDamage;
    [SerializeField] float DistanceFromPlayer;
    [SerializeField] bool skinnedMesh;

    void Start()
    {
        Player = FindObjectOfType<FpsController>().GetComponent<Transform>();

      
        SeekBehaviour.target = Player;
        SeekBehaviour.enabled = false;

       
        RetreatBehaviour.target = Player;
        RetreatBehaviour.enabled = false;

   
        wanderBehaviour.enabled = false;

     
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
            SetColorOfMesh(Color.red,skinnedMesh);
            SeekBehaviour.enabled = true;
            return;
        }
        SeekBehaviour.enabled = false;
    }

    void RetreatFromPlayer()
    {
        if (DistanceFromPlayer <= distanceToStartRetreating)
        {
            SetColorOfMesh(Color.yellow, skinnedMesh);
            RetreatBehaviour.enabled = true;
            return;
        }
        RetreatBehaviour.enabled = false;
    }

    void StartWandering()
    {
        if (DistanceFromPlayer > distanceToStartChasing)
        {
            SetColorOfMesh(Color.blue, skinnedMesh);
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
            SetColorOfMesh(Color.magenta, skinnedMesh);
            Debug.Log("shooting started");
            transform.LookAt(Player);
            shootingBehaviour.ShootObject(bulletDamage);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }
    }

    void SetColorOfMesh(Color color, bool skinnedMesh)
    {
        if(skinnedMesh)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
            skinnedMeshRenderer.material.color = color;
            return;
        }
        MeshRenderer meshRenderer = transform.GetComponent<MeshRenderer>();
        meshRenderer.material.color = color;
    }
}

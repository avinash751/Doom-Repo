using UnityEngine;
using System.Collections;

public class Wanderer : MonoBehaviour
{
    [SerializeField] private float wanderDuration = 3f;
    [SerializeField] private float restDuration = 1f;
    [SerializeField] private float wanderSpeed = 2f;

    private Vector3 wanderDirection;
    private float wanderTimer;
    private Rigidbody rb;
    bool isResting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        wanderTimer = wanderDuration;
        wanderDirection = GetRandomDirectionOnXZPlane();
    }

    private void Update()
    {
        CheckWhetherToRestAndUpdateWanderTimer();
        WanderToDirection();
    }

    void CheckWhetherToRestAndUpdateWanderTimer()
    {
        wanderTimer -= Time.deltaTime;
        if (wanderTimer <= 0f && !isResting)
        {
            Rest(restDuration);
        }
    }

    void WanderToDirection()
    {
        if (!isResting)
            rb.velocity = new Vector3(wanderDirection.x, 0f, wanderDirection.z) * wanderSpeed;
    }


    private void Rest(float duration)
    {
        rb.velocity = Vector3.zero;
        isResting = true;
        StartCoroutine(WaitAndResumeMovement(duration));
    }

    private IEnumerator WaitAndResumeMovement(float duration)
    {
        yield return new WaitForSeconds(duration);
        isResting = false;
        wanderDirection = GetRandomDirectionOnXZPlane();
        wanderTimer = wanderDuration;
    }

    private Vector3 GetRandomDirectionOnXZPlane()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        return new Vector3(randomDirection.x, 0f, randomDirection.y);
    }
}


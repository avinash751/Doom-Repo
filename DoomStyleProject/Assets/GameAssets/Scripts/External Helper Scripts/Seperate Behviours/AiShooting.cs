using UnityEngine;


public class AiShooting : MonoBehaviour
{
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float range = 100f;
    [SerializeField] bool bulletFired;
    CameraShake cameraShake;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
      cameraShake = GetComponent<CameraShake>();
    }


    public void ShootObject(int bulletDamage)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range) && !bulletFired)
        {
            Debug.Log("bullet shot");
            bulletFired = true;
            InflictDamage(hit, bulletDamage);
            Invoke(nameof(EnableShoooting), fireRate);
        }
    }

    private void InflictDamage(RaycastHit hit, int damage)
    {
        if (!hit.collider.TryGetComponent(out IDamagable damageableObject)) return;
        if (hit.collider.TryGetComponent(out FpsController player ))
        {
            Debug.Log("player has taken damage");
            damageableObject.TakeDamage(damage);
            player.gameObject.GetComponent<CameraShake>().EnableCamersShake(true,false);
        }
    }

    void EnableShoooting()
    {
        bulletFired = false;
    }
}


using UnityEngine;

public class AiShooting : MonoBehaviour
{
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float range = 100f;
    [SerializeField] bool bulletFired;




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
        IDamagable damageableObject = hit.collider.GetComponent<IDamagable>();
        if (damageableObject != null)
        {
            Debug.Log("player has taken damage");
            damageableObject.TakeDamage(damage);
        }
    }

    void EnableShoooting()
    {
        bulletFired = false;
    }
}


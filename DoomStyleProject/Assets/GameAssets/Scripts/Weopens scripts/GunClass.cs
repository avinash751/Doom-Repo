using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunClass : MonoBehaviour
{
    [Header("Gun Shooting Stats")]
    [SerializeField] float timeBetweenInputShooting;
    [SerializeField] float gunBulletRange;
    [SerializeField] float bulletSpread;
    [SerializeField] int gunMagazineSize;
    [SerializeField] bool isAutomaticShooting;

    [Header("Gun Reloading Stats")]
    [SerializeField] bool allowedToReload;
    [SerializeField] float timeForReload;
    [SerializeField] bool isAutomaticReloading;

    [Header("Multiple Bullet Shooting Stats")]
    [SerializeField] bool allowToShootMultipleBulletsAtOnce;
    [SerializeField] float timeBetweenEachBulletShot;
    [SerializeField] int bulletsToShootPerClick;


    [Header("Debug Stats, DO NOT CHNAGE")]
    [SerializeField] bool allowInputToShoot = true;
    [SerializeField] bool isReloading;
    [SerializeField] int bulletsLeftInMagazine;
    [SerializeField] string nameOfObjectHit;
    [SerializeField] int bulletsShotSoFar;


    [Header("Refrences")]
    [SerializeField] Transform gunMuzzlePoint;
    [SerializeField]Animator gunAnimator;
    Ray bulletRay;
    RaycastHit objectHit;


    [Header("Animations")]
    [SerializeField] AnimationClip shootAnimation;


    [Header("weopen effects")]
    [SerializeField] ParticleSystem gunMuzzleFlash;
    [SerializeField] ParticleSystem normalBulletimpactVfx;
    [SerializeField] GameObject FurnitureBulletImpactDecal;
    [SerializeField] float effectDestroyTimer;


    private void Start()
    {
       ReloadAmmoToGunMagazine();
      
    }
    private void Update()
    {
        CheckWhetherToReloadAmmo();
        CheckGunInputToStartShooting();
    }

    void CheckGunInputToStartShooting()
    {
        bool shootingInput = isAutomaticShooting? Input.GetKey(KeyCode.Mouse0): Input.GetKeyDown(KeyCode.Mouse0);
        if(shootingInput && allowInputToShoot && bulletsLeftInMagazine>0 && !isReloading)
        {
            StartShootingProcess();
            PlayGunAnimation(shootAnimation);
            PlayGunVfx(gunMuzzleFlash);
            SpawnGunEffectThendestroy(FurnitureBulletImpactDecal, effectDestroyTimer,true);
            SpawnGunEffectThendestroy(normalBulletimpactVfx.gameObject, effectDestroyTimer,false);
        }
    }

    void StartShootingProcess()
    {
        bulletsShotSoFar = allowToShootMultipleBulletsAtOnce ? bulletsToShootPerClick : 0; // to shoot multiple bullets at once , gun like shot gun or burst rifles
        allowInputToShoot = false;
        shootBullet();
        Invoke(nameof(EnableInputToShoot),timeBetweenInputShooting);  
    }
    void shootBullet()
    {
        bulletsLeftInMagazine--;
        ShootRaycast();
        ShootMultipleBulletsAtOnceIfRequired();
    }
    void ShootMultipleBulletsAtOnceIfRequired()
    {
        if (!allowToShootMultipleBulletsAtOnce ) return;
        bulletsShotSoFar--;
        if (bulletsShotSoFar <= 0 || bulletsLeftInMagazine <= 0) return;
        Invoke(nameof(shootBullet), timeBetweenEachBulletShot);
    }

    void ShootRaycast()
    {
        Vector3 perShotBulletSpread = GetBulletSpreadDirection();
        Vector3 bulletDirection = Camera.main.transform.forward + perShotBulletSpread;
        bulletRay = new Ray(Camera.main.transform.position,bulletDirection);

        if(Physics.Raycast(bulletRay,out objectHit, gunBulletRange))
        {
            nameOfObjectHit = objectHit.collider.name;
            Debug.DrawRay(Camera.main.transform.position,bulletDirection*gunBulletRange,Color.green,10f);
        }
    }

    Vector3 GetBulletSpreadDirection()
    {
        float xSpread = Random.Range(-bulletSpread, bulletSpread);
        float ySpread = Random.Range(-bulletSpread, bulletSpread);
        return new Vector3(xSpread, ySpread);
    }

    void EnableInputToShoot()
    {
        if (bulletsShotSoFar < 5 && bulletsShotSoFar!=0)
        { Invoke(nameof(EnableInputToShoot), timeBetweenInputShooting);
          return;
        }
        allowInputToShoot = true;
    }

    private void CheckWhetherToReloadAmmo()
    {
        if (!allowedToReload) return;

        bool reloadingInput = isAutomaticReloading ? true : Input.GetKeyDown(KeyCode.R);

        if(reloadingInput && bulletsLeftInMagazine<=0 && !isReloading)
        {
            allowInputToShoot=false;
            isReloading=true;
            Invoke(nameof(ReloadAmmoToGunMagazine), timeForReload);
        }
    }

    void ReloadAmmoToGunMagazine()
    {
        allowInputToShoot = true;
        isReloading = false;
        bulletsLeftInMagazine = gunMagazineSize;
    }


    // Helper functions for spawning , playing or destroying gun effects

    void PlayGunAnimation(AnimationClip clip)
    {
        gunAnimator.Play(clip.name, -1, 0);
    }

    void PlayGunVfx(ParticleSystem gunVfx)
    {
        gunVfx.Play();
    }

    void SpawnGunEffectThendestroy(GameObject effect, float timer, bool parenting)
    {
        if(objectHit.collider==null) return;
        var decalDuplicate = Instantiate(effect, objectHit.point, Quaternion.LookRotation(objectHit.normal));
        Vector3 decalOffset = decalDuplicate.transform.forward / 1000;
        decalDuplicate.transform.position += decalOffset;

        decalDuplicate.transform.parent =  parenting ?objectHit.collider.transform : null;
        Destroy(decalDuplicate,timer);
    }
 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * gunBulletRange);
    }
}

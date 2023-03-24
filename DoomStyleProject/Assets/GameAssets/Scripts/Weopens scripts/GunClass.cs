using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GunClass : MonoBehaviour
{
    [Header("Gun Shooting Stats")]
    [SerializeField] float timeBetweenInputShooting;
    [SerializeField] float gunBulletRange;
    [SerializeField] float bulletSpread;
    [SerializeField] public int gunMagazineSize;
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
    [SerializeField] public bool allowInputToShoot;
    [SerializeField] bool isReloading;
    [SerializeField]  int bulletsLeftInMagazine;
    [SerializeField] string nameOfObjectHit;
    [SerializeField] int bulletsShotSoFar;


    [Header("Refrences")]
    [SerializeField] Animator gunAnimator;
    Ray bulletRay;
    RaycastHit objectHit;


    [Header("Animations")]
    [SerializeField] AnimationClip shootAnimation;


    [Header("Weopen effects")]
    [SerializeField] GameObject gunCrossAir;
    [SerializeField] ParticleSystem gunMuzzleFlash;
    [SerializeField] ParticleSystem normalBulletimpactVfx;
    [SerializeField] GameObject FurnitureBulletImpactDecal;
    [SerializeField] float effectDestroyTimer;

    [Header("Gun Sounds")]
    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioSource reloadSound;
    [SerializeField] AudioSource denyShootSound;

    public Action<int> currentAmmoHasBeenChanged;

    public int CurrentAmmo
    {
        get { return bulletsLeftInMagazine; }

        set
        {
            bulletsLeftInMagazine = value;
            currentAmmoHasBeenChanged?.Invoke(bulletsLeftInMagazine);
        }
    }
    public bool AllowInputToShoot
    {
        get
        {
            return allowInputToShoot;
        }

        set
        {
            allowInputToShoot = value;
            // cross air is disabled if shooting input is false else it will be enabled
            if (allowInputToShoot) { gunCrossAir?.SetActive(true); }
            else { gunCrossAir?.SetActive(false); }
        }
    }


    private void Start()
    {
        ReloadAmmoToGunMagazine();
        AllowInputToShoot = false;
    }
    private void Update()
    {
        CheckWhetherToReloadAmmo();
        DisableShootingWhenAmmoZero();
        CheckGunInputToStartShooting();
    }

    void CheckGunInputToStartShooting()
    {
        bool shootingInput = isAutomaticShooting ? Input.GetKey(KeyCode.Mouse0) : Input.GetKeyDown(KeyCode.Mouse0);

        if (shootingInput && allowInputToShoot && bulletsLeftInMagazine > 0 && !isReloading)
        {
            StartShootingProcess();
            PlayGunAnimation(shootAnimation);
            PlayGunVfx(gunMuzzleFlash);
            PlayAGunSoundAndChangePitch(shootSound, 0.75f, 1.3f);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !allowInputToShoot && bulletsLeftInMagazine <= 0)
        {
            PlayAGunSoundAndChangePitch(denyShootSound, 0.9f, 1.2f);
        }
    }

    void StartShootingProcess()
    {
        bulletsShotSoFar = allowToShootMultipleBulletsAtOnce ? bulletsToShootPerClick : 0; // to shoot multiple bullets at once ,  like shot gun or burst rifles
        allowInputToShoot = false;
        shootBullet();
        Invoke(nameof(EnableInputToShoot), timeBetweenInputShooting);
    }
    void shootBullet()
    {
        CurrentAmmo--;
        ShootRaycast();
        ShootMultipleBulletsAtOnceIfRequired();
        // bullets effects that spawn only after bullets are shot 
        SpawnGunEffectAtRayNormalThendestroy(FurnitureBulletImpactDecal, effectDestroyTimer, true);
        SpawnGunEffectAtRayNormalThendestroy(normalBulletimpactVfx.gameObject, effectDestroyTimer, false);
    }
    void ShootMultipleBulletsAtOnceIfRequired()
    {
        if (!allowToShootMultipleBulletsAtOnce) return;
        bulletsShotSoFar-=1;
        // check to make sure it does keep shooting mulriple bullets at once
        if (bulletsShotSoFar <= 0 || bulletsLeftInMagazine <= 0) return;
        Invoke(nameof(shootBullet), timeBetweenEachBulletShot);
    }

    void ShootRaycast()
    {
        Vector3 perShotBulletSpread = GetBulletSpreadDirection();
        Vector3 bulletDirection = Camera.main.transform.forward + perShotBulletSpread;
        bulletRay = new Ray(Camera.main.transform.position, bulletDirection);

        if (Physics.Raycast(bulletRay, out objectHit, gunBulletRange))
        {
            nameOfObjectHit = objectHit.collider.name;
            Debug.DrawRay(Camera.main.transform.position, bulletDirection * gunBulletRange, Color.green, 10f);
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
        // this makes sure if all bullets not shot then dont allow player to shoot, this prevents losing more bullets than required
        if (bulletsShotSoFar < 5 && bulletsShotSoFar != 0)
        {
            // this makes sure that you can shoot again if all bullsts are shot
            Invoke(nameof(EnableInputToShoot), timeBetweenInputShooting);
            return;
        }
        allowInputToShoot = true;
    }

    private void CheckWhetherToReloadAmmo()
    {
        if (!allowedToReload) return;

        // whether to reload manualy or automaticly
        bool reloadingInput = isAutomaticReloading ? true : Input.GetKeyDown(KeyCode.R);

        if (reloadingInput && bulletsLeftInMagazine <= 0 && !isReloading)
        {
            allowInputToShoot = false;
            isReloading = true;
            PlayAGunSoundAndChangePitch(reloadSound, 0.8f, 1.4f);
            Invoke(nameof(ReloadAmmoToGunMagazine), timeForReload);
        }
    }

    void ReloadAmmoToGunMagazine()
    {
        allowInputToShoot = true;
        isReloading = false;
        CurrentAmmo = gunMagazineSize;
    }

    void DisableShootingWhenAmmoZero()
    {
        if (bulletsLeftInMagazine <= 0) allowInputToShoot = false;
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

    void SpawnGunEffectAtRayNormalThendestroy(GameObject effect, float timer, bool parenting)
    {
        if (objectHit.collider == null) return;
        var decalDuplicate = Instantiate(effect, objectHit.point, Quaternion.LookRotation(objectHit.normal));
        Vector3 decalOffset = decalDuplicate.transform.forward / 1000;
        decalDuplicate.transform.position += decalOffset;

        decalDuplicate.transform.parent = parenting ? objectHit.collider.transform : null;
        Destroy(decalDuplicate, timer);
    }

    void PlayAGunSoundAndChangePitch(AudioSource source, float minPitch, float maxPitch)
    {
        if (source == null) return;
        source.pitch = Random.Range(minPitch, maxPitch);
        source.Play();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * gunBulletRange);
    }
}

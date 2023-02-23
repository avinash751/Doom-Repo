using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms;

public class GunClass : MonoBehaviour
{
    [Header("Gun Stats")]
    [SerializeField] float timeBetweenInputShooting;
    [SerializeField] float gunBulletRange;
    [SerializeField] float bulletSpread;
    [SerializeField] float timeForReload;
    [SerializeField] int gunMagazineSize;
    [SerializeField] bool isAutomaticShooting;
    [SerializeField] bool isAutomaticReloading;
    [SerializeField] bool shootMultipleBulletsAtOnce;
    [SerializeField] float timeBetweenEachBulletShot;
    [SerializeField] int bulletsPerClick;


    [Header("Debug Stats, DO NOT CHNAGE")]
    [SerializeField] bool shootingInput;
    [SerializeField] bool readyToShoot = true;
    [SerializeField] bool isReloading;
    [SerializeField] int bulletsLeftInMagazine;


    [Header("Refrences")]
    [SerializeField] Transform gunMuzzlePoint;
    Ray bulletRay;
    Vector3 bulletSpawnOrgin;
    RaycastHit objectHit;
    [SerializeField] string nameOfObjectHit;


    private void Update()
    {
        CheckGunInputToStartShooting();
    }


    void CheckGunInputToStartShooting()
    {
        shootingInput = isAutomaticShooting? Input.GetKey(KeyCode.Mouse0): Input.GetKeyDown(KeyCode.Mouse0);

        if(shootingInput && readyToShoot && bulletsLeftInMagazine>0 && !isReloading)
        {
            StartshootingBullet();
        }
    }

    void StartshootingBullet()
    {
        readyToShoot = false;
        bulletsLeftInMagazine--;
        ShootRaycast();
        Invoke(nameof(EnableInputToShoot), timeBetweenEachBulletShot);
    }

    void ShootRaycast()
    {
        bulletSpawnOrgin = Camera.main.transform.position + new Vector3(bulletSpread, bulletSpread, 0);
        bulletRay = new Ray(bulletSpawnOrgin, Camera.main.transform.forward);

        if(Physics.Raycast(bulletRay,out objectHit,gunBulletRange))
        {
            nameOfObjectHit = objectHit.collider.name;
            Debug.DrawRay(bulletSpawnOrgin, bulletSpawnOrgin, Color.red, 5f);
        }
    }

    void EnableInputToShoot()
    {
        readyToShoot = true;
    }

    private void OnDrawGizmos()
    {
        
    }
}

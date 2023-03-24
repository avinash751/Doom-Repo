using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoConsumable : Consumable
{
    [SerializeField] int ammoToAdd;
    PickupObject player;

    private void Start()
    {
        player = FindObjectOfType<PickupObject>();
    }
    public override void Consume(float destroyTimer, AudioSource source)
    {
        base.Consume(destroyTimer, source);
        AddAmmoToGun();
    }
    void AddAmmoToGun()
    {
        GunClass currenEquippedGun = player.equipList[player.currentWeapon].GetComponent<GunClass>();
        currenEquippedGun.CurrentAmmo += ammoToAdd;
        currenEquippedGun.allowInputToShoot = true;
        currenEquippedGun.CurrentAmmo = Mathf.Clamp(currenEquippedGun.CurrentAmmo, 0, currenEquippedGun.gunMagazineSize);
        Debug.Log("Picked up some ammo");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickupObject : MonoBehaviour
{
    [SerializeField] Transform equipTransform;
    [SerializeField] Transform parent;

    [SerializeField] public List<GameObject> equipList = new List<GameObject>();

    bool weaponActive = false;

    [Header("Weapon Offsets")]
    [SerializeField] Vector3 pistolOffset;
    [SerializeField] Vector3 shotgunOffset;

    [HideInInspector] public int currentWeapon;

    [Header("Audio sources")]
    [SerializeField] AudioSource weopenPickUpAudioSource;
    [SerializeField] AudioSource consumablePickUpAudioSource;

    public Action<float, float> WeopenHasBeenSwitched;
    public Action newWeopenHasbeenEquipped;

    private void Start()
    {
        if (equipList.Count > 0)
        {
            equipList[currentWeapon].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (equipList.Count == 0)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapons(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapons(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapons(2);
        }
        WeaponOffset();
    }

    void SwitchWeapons(int number)
    {
        if (equipList.Count == 0 || equipList.Count <= number)
        {
            return;
        }
        currentWeapon = number;
        for (int i = 0; i < equipList.Count; i++)
        {
            if (i == number)
            {
                GunClass newCurrentWeopen = equipList[i].GetComponent<GunClass>();
                equipList[i].SetActive(true);

                // this provides the ammo bar its new current and max values so it updates to the correct weopen
                WeopenHasBeenSwitched?.Invoke(newCurrentWeopen.CurrentAmmo, newCurrentWeopen.gunMagazineSize);
                // this provides the refrence of the gun to ammo bar so it can update whenever the weopen is shooting
                newWeopenHasbeenEquipped?.Invoke();
            }
            else
            {
                equipList[i].SetActive(false);
            }
        }
    }
    void WeaponOffset()
    {
        if (currentWeapon == 0 && weaponActive)
        {
            equipList[currentWeapon].transform.localPosition = pistolOffset;
        }
        if (currentWeapon == 1 && weaponActive)
        {
            equipList[currentWeapon].transform.localPosition = shotgunOffset;
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        IEquipable weapon = other.GetComponent<IEquipable>();
        Weapon gun = weapon as Weapon;
        if (weapon != null && !gun.hasPickedUp)
        {
            weapon.PickedUpEquipment(equipTransform.position, gun.hasPickedUp = true, parent, weopenPickUpAudioSource);
            equipList.Add(gun.gameObject);
            SwitchWeapons(equipList.Count - 1);
            weaponActive = true;
            equipList[currentWeapon].transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        IConsumable consumeItem = other.GetComponent<IConsumable>();
        if (consumeItem != null)
        {
            consumeItem.Consume(0f, consumablePickUpAudioSource);
        }
    }
}

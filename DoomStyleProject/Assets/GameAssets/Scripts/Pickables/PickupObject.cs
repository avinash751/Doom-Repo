using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] Transform equipTransform;
    [SerializeField] Transform parent;

    [SerializeField] List<GameObject> equipList = new List<GameObject>();
    [SerializeField] Vector3 gunOffset;
    int currentWeapon;

    [SerializeField] AudioSource weopenPickUpAudioSource;
    [SerializeField] AudioSource consumablePickUpAudioSource;
    private void Start()
    {
        if (equipList.Count > 0)
        {
            equipList[currentWeapon].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
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
    }

    void SwitchWeapons(int number)
    {
        currentWeapon = number;

        if (number < 0 || number >= equipList.Count)
        {
            return;
        }
        currentWeapon = number;
        for (int i = 0; i < equipList.Count; i++)
        {
            if (i == number)
            {
                equipList[i].SetActive(true);
            }
            else
            {
                equipList[i].SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IEquipable weapon = other.GetComponent<IEquipable>();
        Weapon gun = weapon as Weapon;
        if (weapon != null && !gun.hasPickedUp)
        {
            weapon.PickedUpEquipment(equipTransform.position, gun.hasPickedUp = true, parent,weopenPickUpAudioSource);
            equipList.Add(gun.gameObject);
            SwitchWeapons(equipList.Count - 1);
            equipList[currentWeapon].transform.localRotation = Quaternion.Euler(Vector3.zero);
            equipList[currentWeapon].transform.localPosition = gunOffset;
           
        }

        IConsumable consumeItem = other.GetComponent<IConsumable>();
        if (consumeItem != null)
        {
            consumeItem.Consume(0.2f,consumablePickUpAudioSource);
          
        }
    }


  
}

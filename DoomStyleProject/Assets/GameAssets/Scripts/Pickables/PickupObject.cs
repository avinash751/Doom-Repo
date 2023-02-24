using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    [SerializeField] Transform equipTransform;
    [SerializeField] Transform parent;

    List<int> equipList = new List<int>();
    bool currentlyEquipped;


    private void OnTriggerEnter(Collider other)
    {
        IEquipable weapon = other.GetComponent<IEquipable>();
        Weapon gun = weapon as Weapon;
        if (weapon != null && !gun.hasPickedUp)
        {
            weapon.PickedUpEquipment(equipTransform.position, gun.hasPickedUp = true, parent);
        }

        IConsumable consumeItem = other.GetComponent<IConsumable>();
        if (consumeItem != null)
        {
            consumeItem.Consume(0.2f);
        }
    }


}

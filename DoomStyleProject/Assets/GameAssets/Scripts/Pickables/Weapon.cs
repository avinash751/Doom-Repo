using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IEquipable
{
    public bool hasPickedUp;
    public void PickedUpEquipment(Vector3 weaponPosition, bool hasPickedUp, Transform parent)
    {
        this.transform.position = weaponPosition;
        this.hasPickedUp = hasPickedUp;
        transform.parent = parent;
    }
}

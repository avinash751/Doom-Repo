using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    void PickedUpEquipment(Vector3 weaponPosition, bool hasPickedUp, Transform parent);
}

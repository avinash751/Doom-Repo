using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour, IConsumable
{
    public void Consume(float destroyTimer)
    {
        Destroy(gameObject, destroyTimer);
    }
}

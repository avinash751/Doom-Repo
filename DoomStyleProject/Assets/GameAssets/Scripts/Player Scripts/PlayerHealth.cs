using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

public class PlayerHealth : ObjectHealth
{
    public static event Action PlayerHasDied;



    public override void DestroyObject()
    {
        base.DestroyObject();
        PlayerHasDied?.Invoke();
    }
}


[CustomEditor(typeof(PlayerHealth))]
public class PlayerHealthEditor : ObjectHealthEditor
{ 

}


using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class ObjectHealth : MonoBehaviour,IDamagable,IDestroyable
{
    public Value  MaxHealth;
    public int currentHealth;
    [SerializeField] AudioSource damageTakenSound;
    public  bool addMoreFunctionalityWhenDamageTaken;
    public UnityEvent OnDamageTaken;


    void Start()
    {
       InitialisingValues();
    }

    public void TakeDamage(int Amount)
    {
        currentHealth -= Amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth.value);

        damageTakenSound?.Play();
        OnDamageTaken?.Invoke();
        
        if(currentHealth <=0)
        {
            DestroyObject();
        }
    }

    public virtual void DestroyObject()
    {
 
        Debug.Log("object is dead");
    }


    void InitialisingValues()
    {
        currentHealth = 0;
        currentHealth = MaxHealth.value;
    }

    
}
[CustomEditor(typeof(ObjectHealth))]
public class ObjectHealthEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ObjectHealth health = (ObjectHealth)target;
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("MaxHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("currentHealth"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("damageTakenSound"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("addMoreFunctionalityWhenDamageTaken"));

        if (health.addMoreFunctionalityWhenDamageTaken)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("OnDamageTaken"));
        }

        if (EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
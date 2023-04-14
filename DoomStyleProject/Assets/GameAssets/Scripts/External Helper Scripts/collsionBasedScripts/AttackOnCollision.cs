using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

public class AttackOnCollision : MonoBehaviour
{
    [SerializeField] int amountOfDamage;
    public bool checkWithTag;
    public string collisionTag;
    public bool doMoreFunctionalityOncollsion;
    public UnityEvent DoExtraOnCollsion;
    bool collided;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null)
        {
            if (collision.gameObject.CompareTag(collisionTag) && checkWithTag)
            {
                Attack(collision.gameObject);
            }
            else if (!checkWithTag)
            {
                Attack(collision.gameObject);
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamagable>() != null)
        {
            if (collision.gameObject.CompareTag(collisionTag) && checkWithTag)
            {
                collided = false;
            }
            else if (!checkWithTag)
            {
                collided = false;
            }

        }
    }

    public void Attack(GameObject collsionObject)
    {
        DoExtraOnCollsion.Invoke();
        IDamagable DamagableObject = collsionObject.GetComponent<IDamagable>();
        DamagableObject.TakeDamage(amountOfDamage);
        collided = true;
    }
}


#if UNITY_EDITOR

[CustomEditor(typeof(AttackOnCollision))]
public class AttackOnCollsionEditor : Editor
{
    private string[] tagNames;

    private void OnEnable()
    {
        tagNames = UnityEditorInternal.InternalEditorUtility.tags; // gets all the tags set in this unity project
    }
    public override void OnInspectorGUI()
    {
        AttackOnCollision collsionScript = (AttackOnCollision)target;
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("amountOfDamage"));
        collsionScript.checkWithTag = EditorGUILayout.Toggle("Check with Tag", collsionScript.checkWithTag);

        if (collsionScript.checkWithTag)
        {
            //this gets index of collsion tag from the tag names array and set it to the variable index
            int index = Array.IndexOf(tagNames, collsionScript.collisionTag);
            //overrides the index variables based on whats chosen in the inspector
            index = EditorGUILayout.Popup("Collision Tag", index, tagNames);
            //sets the tag based on the index variables which is then passed to the tag names array
            collsionScript.collisionTag = tagNames[index];
        }
        else
        {
            collsionScript.collisionTag = "Untagged";
        }

        EditorGUILayout.PropertyField(serializedObject.FindProperty("doMoreFunctionalityOncollsion"));

        if (collsionScript.doMoreFunctionalityOncollsion)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("DoExtraOnCollsion"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}
#endif



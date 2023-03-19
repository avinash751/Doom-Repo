using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ColliderGizmo : MonoBehaviour
{
    private void OnValidate()
    {
        
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().center + transform.position, GetComponent<BoxCollider>().size + transform.localScale);

        Gizmos.color = new Color(1, 0, 0, 0.15f);
        Gizmos.DrawCube(GetComponent<BoxCollider>().center +  transform.position, GetComponent<BoxCollider>().size + transform.localScale);
    }
}



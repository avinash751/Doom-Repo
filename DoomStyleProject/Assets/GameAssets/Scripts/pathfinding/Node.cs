using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Node : IComparable<Node>
{
    public GameObject mesh;

    public Vector3 localPosition;
    public Vector3 worldPosition;

    public Node[] Neibhours = new Node[3];
    public Node parentNode;

    public bool isVisited;
    bool isWalkable;

    Text hCostText;
    Text gCostText;
    Text fCostText;
    Text localPositionText;

    public int nodeVersion;
    public bool debugNode;


    float hCost = 0;
    public float Hcost
    {
        get
        {
            return hCost;
        }
        set
        {
            hCost = value;
            fCost = gCost + hCost;

            ReferenceTextAndSetTextForIt(ref hCostText, value.ToString(),0);
            ReferenceTextAndSetTextForIt(ref fCostText, fCost.ToString(), 2);
        }
    }

    float gCost = 0;
    public float Gcost
    {
        get
        {
            return gCost;
        }
        set
        {
            gCost = value;
            fCost = gCost + hCost;


            ReferenceTextAndSetTextForIt(ref gCostText, value.ToString(), 1);
            ReferenceTextAndSetTextForIt(ref fCostText, fCost.ToString(), 2);
        }
    }
    float fCost = 0;
    public float Fcost
    {
        get
        {
            return fCost;
        }
        set
        {

        }
    }

    public GameObject Mesh
    {
        get
        {
            return mesh;
        }
        set
        {
            mesh = value;
            localPositionText = mesh.transform.GetChild(0).GetChild(3).GetComponent<Text>();
            localPositionText.text = localPosition.ToString();
        }

    }


    public Node(Vector3 gridPosition, Vector3 worldPosition)
    {
        this.localPosition = gridPosition;
        this.worldPosition = worldPosition;
    }
    public int CompareTo(Node node)
    {
        if (node.Fcost > this.Fcost)
        {
            return -1;
        }
        else if (node.Fcost < this.Fcost)
        {
            return 1;
        }
        return 0;
    }

    void ReferenceTextAndSetTextForIt(ref Text textToReference, string contentOfText,int indexReference)
    {
        if (!debugNode) return;
        textToReference = mesh.transform.GetChild(0).GetChild(indexReference).GetComponent<Text>();
        textToReference.text = contentOfText;
    }
}

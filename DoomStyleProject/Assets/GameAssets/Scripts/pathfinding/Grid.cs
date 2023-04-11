using System.Runtime.ExceptionServices;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public  Vector3 gridSize;
    [SerializeField] Vector3 cellSize;
    public GameObject nodePrefab;
    [SerializeField] Node[] nodesArray;
    [SerializeField] int totalNodesInGrid;
    [SerializeField] bool debugMode;
    Color defaultGridColor = new Color(0, 0.02276306f, 0.3207547f, 1);
    private void Awake()
    {
        totalNodesInGrid = (int)(gridSize.x * gridSize.z);
        nodesArray = new Node[totalNodesInGrid];
        InitilizeGridWithNodes();
       
    }


    void InitilizeGridWithNodes()
    {
        for (int zNodes = 0; zNodes < gridSize.z; zNodes++)
        {
            for (int xNodes = 0; xNodes < gridSize.x; xNodes++)
            {
                int index = (int)(xNodes + zNodes * gridSize.x);
                Vector3 gridSpawnPosition = new Vector3(xNodes, 0, zNodes);
                Vector3 worldSpawnPosition = new Vector3(gridSpawnPosition.x * cellSize.x,0, gridSpawnPosition.z *cellSize.z) + transform.position + (cellSize/2);
                nodesArray[index] = new Node(gridSpawnPosition, worldSpawnPosition);
                nodesArray[index].debugNode = debugMode;
                SpawnCubeAtNodeWorldPosition(worldSpawnPosition, index, nodesArray[index]);
            }
        }   
    }

    public Node getNodeBasedOnLocalPosition(Vector3 position)
    {
        int index =(int)(position.x + position.z * gridSize.x);
        return nodesArray[index];
    }
    void SpawnCubeAtNodeWorldPosition(Vector3 spawnPosition,int index,Node node)
    {
        if (!debugMode) return;
        node.Mesh = Instantiate(nodePrefab, spawnPosition, Quaternion.identity);
        node.Mesh.transform.localScale = cellSize- new Vector3(0.01f,0,0.01f);
        SetColorOfMesh(node,defaultGridColor);
    } 
    public void ClearWholeGridColor()
    {
        if (!debugMode) return;
        for (int i = 0; i < nodesArray.Length; i++)
        {
            SetColorOfMesh(nodesArray[i], defaultGridColor);
        }
    }

    public void SetColorOfMesh(Node node,Color color)
    {
        if (!debugMode) return;
        node.mesh.GetComponent<MeshRenderer>().material.color = color;
    }

    public Node GetNodeBasedOnWorldPosition(Vector3 Worldposition)
    {
        Vector3 localPosition = new Vector3(Worldposition.x / cellSize.x, Worldposition.y, Worldposition.z / cellSize.z);
        Vector3 intLocalPosition = new Vector3((int)localPosition.x, (int)localPosition.y, (int)localPosition.z);
        intLocalPosition.y = 0;
        return getNodeBasedOnLocalPosition(intLocalPosition);
    }
}
    
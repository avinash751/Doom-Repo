using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunEventWhenListIsEmpty : MonoBehaviour
{
    [SerializeField] protected List<GameObject> listOfGameObjects = new List<GameObject>();
    [SerializeField] bool  DestroyAfterEvent;
    [SerializeField] protected  UnityEvent onListEmpty;


    private void Update()
    {
        listOfGameObjects.RemoveAll(go => go == null);
        RunEventWhenListEmpty();
    }

    void RunEventWhenListEmpty()
    {
        if (listOfGameObjects.Count == 0)
        {
            onListEmpty?.Invoke();
            DoSomthingWhenListEmpty();
            DestroyGameObject();
        }
    }

    void DestroyGameObject()
    {
        if (DestroyAfterEvent)
        {
            Destroy(gameObject);
        }
    }
    protected virtual void  DoSomthingWhenListEmpty()
    {

    }
}

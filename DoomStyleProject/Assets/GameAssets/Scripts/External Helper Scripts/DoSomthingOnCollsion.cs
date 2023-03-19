using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DoSomthingOnCollsion : MonoBehaviour
{
    [SerializeField] bool isTrigger;
    [SerializeField] bool onlyPlayOnce;
    [SerializeField] string tag;
    [SerializeField] float delayToPlayEvent;
    [SerializeField] UnityEvent DoSomethingOnCollisionOrTrigger;
    bool played;
   

  

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = this.isTrigger;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(tag))
        PlayAnEventBasedOnCollisionType(!isTrigger);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
         PlayAnEventBasedOnCollisionType(isTrigger);
    }

    public void PlayAnEventBasedOnCollisionType(bool collisionCondition)
    {
        if (onlyPlayOnce && played) return;

        if (collisionCondition)
        {
            played = true;
            Invoke(nameof(RunUnityEventAfterDelay),delayToPlayEvent);
           
        }

        
    }

    void RunUnityEventAfterDelay()
    {
        DoSomethingOnCollisionOrTrigger?.Invoke();
    }
}

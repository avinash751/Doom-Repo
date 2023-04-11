using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AfterDelayPlayEvent : MonoBehaviour
{
   
    [SerializeField]
    float delayTime;
    [SerializeField] bool playOnstart;

    [SerializeField] UnityEvent doSomethingAfterDelay;

    void Start()
    {
        if (!playOnstart) return;
        InvokeEventAfterDelay(delayTime);
    }

    public void InvokeEventAfterDelay(float delayTime)
    {
        Invoke(nameof(PlayUnityEvent), delayTime);
    }

    void PlayUnityEvent()
    {
        doSomethingAfterDelay?.Invoke();
    }



}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class EnableArrayOfSpotlightsThroughAnEvent : MonoBehaviour
{
    public OnTriggerEnableSpotlight[] spotlightArray;
    [SerializeField] float delayRate;
    [SerializeField] bool enableSotlightsOnStart;
    [SerializeField] UnityEvent afterAllSpotlightsAreEnabled;



    private void Start()
    {
        EnableSpotlightsOnStart();
    }

    void EnableSpotlightsOnStart()
    {
        if (!enableSotlightsOnStart) return;
        EnableSpotlihgtsInArray(delayRate);
    }

    void EnableSpotlightsWithDefaultDelayRate()
    {
        EnableSpotlihgtsInArray(delayRate);
    }

    public void EnableSpotlihgtsInArray(float delayTime)
    {
        StartCoroutine(EnableAllSpotlightsInArrayWithDelay(delayTime));
    }

    public void StartEnablingSpotlightsInArrayAfterDelay(float delayTime)
    {
        Invoke(nameof(EnableSpotlightsWithDefaultDelayRate), delayTime);
    }



    IEnumerator EnableAllSpotlightsInArrayWithDelay(float delayTimePerSpotlight)
    {
        for (int i = 0; i < spotlightArray.Length; i++)
        {
            yield return new WaitForSeconds(delayTimePerSpotlight);
            spotlightArray[i].EnableSpotlightAndPlaySound();
            
        }
        afterAllSpotlightsAreEnabled?.Invoke();
    }

}

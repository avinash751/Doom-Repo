using System.Collections;
using UnityEngine;

public class EnableArrayOfSpotlightsThroughAnEvent : MonoBehaviour
{
    public OnTriggerEnableSpotlight[] spotlightArray;
    [SerializeField] float delayTime;
    [SerializeField] bool enableSotlightsOnStart;



    private void Start()
    {
        EnableSpotlightsOnStart();
    }

    void EnableSpotlightsOnStart()
    {
        if (!enableSotlightsOnStart) return;
        EnableSpotlihgtsInArray();
    }

    public void EnableSpotlihgtsInArray()
    {
        StartCoroutine(EnableAllSpotlightsInArrayWithDelay());
    }



    IEnumerator EnableAllSpotlightsInArrayWithDelay()
    {
        for (int i = 0; i < spotlightArray.Length; i++)
        {
            spotlightArray[i].EnableSpotlightAndPlaySound();
            yield return new WaitForSeconds(delayTime);
        }
    }

}

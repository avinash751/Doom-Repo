using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisulaBar: MonoBehaviour
{
    public Image visualBarImage;
    public float maxValue;
    public float currentValue;
    public Gradient barGradient;

    public virtual  void Start()
    {
        visualBarImage = visualBarImage ?? GetComponent<Image>();
        visualBarImage.fillAmount = currentValue/maxValue;

    }
    virtual public void SetNewCurrentValueAndMaxValueAndUpdateBar(float newCurrentValue, float newMaxValue)
    {
        maxValue = newMaxValue;
        currentValue = newCurrentValue;
       
        UpdateBarAndColor(currentValue / maxValue);
    }
    virtual public void DecreaseCircularBarValue(int value )
    {
        currentValue = value;
        UpdateBarAndColor(currentValue/maxValue);
    }

    void UpdateBarAndColor(float fillValue )
    {
        visualBarImage.fillAmount = fillValue;
        visualBarImage.color = barGradient.Evaluate(visualBarImage.fillAmount);
    }
}

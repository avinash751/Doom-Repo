using UnityEngine;
using TMPro;

public class FadeTextOnTrigger : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    [SerializeField] float DelayDurationTofadeIn;
    [SerializeField]float fadeDuration;

    private Color initialColor;

    private bool fadeIn;
    private bool fadeOut;

    private float elapsedTime;
    private float targetAlpha;

    
    void Start()
    {
      InitializeTextColor();
    }

    void Update()
    {
        CheckWhetherToFadeOrNot(); 
    }

    void CheckWhetherToFadeOrNot()
    {
        if (fadeIn || fadeOut)
        {
            lerpTextColorTransperancy();

            // If the fade has completed, stop fading
            if (elapsedTime >= fadeDuration)
            {
                fadeIn = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // If the trigger has been entered and the text is not already fading in, start fading in
        if (other.TryGetComponent(out FpsController player) )
        {
            StartFadeIn();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the trigger has been exited and the text is not already fading out, start fading out
        if (other.TryGetComponent(out FpsController player)  )
        {
            CancelInvoke(nameof(DelayToEnableFading));
            StartFadeOut();
        }
    }

    void StartFadeIn()
    {
        fadeOut = false;
        Invoke(nameof(DelayToEnableFading), DelayDurationTofadeIn);
        initialColor = text.color;
       
        elapsedTime = 0f;
        targetAlpha = 1f;
    }

    void StartFadeOut()
    {
        initialColor = text.color;
        fadeIn = false;
        fadeOut= true;
        elapsedTime = 0f;
        targetAlpha = 0f;
    }

    void lerpTextColorTransperancy()
    {
        elapsedTime += Time.deltaTime;
        float t = elapsedTime / fadeDuration;

        text.color = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Lerp(initialColor.a, targetAlpha, t));
    }

    void DelayToEnableFading()
    {
        fadeIn = true;
    }

    void InitializeTextColor()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        initialColor = text.color;
    }
}





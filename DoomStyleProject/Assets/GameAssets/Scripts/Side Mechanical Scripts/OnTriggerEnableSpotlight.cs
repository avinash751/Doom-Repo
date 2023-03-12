using UnityEngine;

public class OnTriggerEnableSpotlight : MonoBehaviour
{
    public Light spotlight;
    private AudioSource spotlightSound;
    bool playOnce;

    private void Awake()
    {
        spotlight =  GetComponent<Light>();
        spotlightSound = GetComponent<AudioSource>();
      
    }

    private void Start()
    {
        spotlight.enabled = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerTag player))
        {
            EnableSpotlightAndPlaySound();
        }
    }

    public void EnableSpotlightAndPlaySound()
    {
        if (playOnce) return;
        spotlight.enabled = true;
        spotlightSound.pitch = Random.Range(0.9f, 1.2f);
        playOnce = true;
        spotlightSound?.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour, IConsumable
{
    [SerializeField] AudioClip consumablePickUpSoundClip;
    public virtual void Consume(float destroyTimer, AudioSource source)
    {
        Destroy(gameObject, destroyTimer);
        PlayAudioPickUpsound(source, consumablePickUpSoundClip);
    }

    void PlayAudioPickUpsound(AudioSource source, AudioClip clip)
    {
        if (source == null) return;
        source.pitch = Random.Range(0.9f, 1.2f);
        source?.PlayOneShot(clip);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IEquipable
{
    public bool hasPickedUp;
    BoxCollider gunCollider;
    GunClass gun;
   [SerializeField] AudioClip gunPickUpSoundClip;

    private void Start()
    {

        EnableOrDisableGunCollider(enabled);
    }

    public void PickedUpEquipment(Vector3 weaponPosition, bool hasPickedUp, Transform parent, AudioSource source)
    {
        this.transform.position = weaponPosition;
        this.hasPickedUp = hasPickedUp;
        transform.parent = parent;

        EnableOrDisableGunCollider(false); // this is to maks sure that the gun raycast does not hit its own collider
        EnableOrDisableShootingForGun(true);
        PlayAudioPickUpsound(source, gunPickUpSoundClip);
        
    }

    void EnableOrDisableGunCollider(bool enabled)
    {
        gunCollider = gunCollider ?? GetComponent<BoxCollider>();
        gunCollider.enabled = enabled;
    }


    void EnableOrDisableShootingForGun(bool enabled)
    {
        gun = gun ?? GetComponent<GunClass>();
        gun.AllowInputToShoot = enabled;
    }

    void PlayAudioPickUpsound(AudioSource source, AudioClip clip)
    {
        if (source == null) return;
        source.pitch = Random.Range(0.9f, 1.2f);
        source?.PlayOneShot(clip);
    }
}

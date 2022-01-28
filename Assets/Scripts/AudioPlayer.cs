using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting Audio Setting")]
    [SerializeField]
    AudioClip shootingAudioClip;

    [SerializeField]
    [Range(0, 1)]
    float shootingVolume = .3f;

    [Header("Damage Audio Setting")]
    [SerializeField]
    AudioClip damageAudioClip;

    [SerializeField]
    [Range(0, 1)]
    float damageVolume = .4f;

    Vector3 mainCameraPosition;

    void Start()
    {
        mainCameraPosition = Camera.main.transform.position;
    }

    public void PlayShootingAudio()
    {
        if (shootingAudioClip != null)
        {
            AudioSource.PlayClipAtPoint (
                shootingAudioClip,
                mainCameraPosition,
                shootingVolume
            );
        }
    }

    public void PlayDamageAudio()
    {
        if (damageAudioClip != null)
        {
            AudioSource.PlayClipAtPoint (
                damageAudioClip,
                mainCameraPosition,
                damageVolume
            );
        }
    }
}

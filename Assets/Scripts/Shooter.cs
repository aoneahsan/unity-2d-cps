using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [HideInInspector]
    public bool isFiring = false;

    [Header("Genernal")]
    [SerializeField]
    GameObject projectilePrefab;

    [SerializeField]
    float projectileSpeed = 10f;

    [SerializeField]
    float projectileLifeTime = 5f;

    [SerializeField]
    float delayInProjectiles = .2f;

    Coroutine firingCoroutine;

    [Header("AI Settings")]
    [SerializeField]
    bool useAI;

    [SerializeField]
    float minDelayInProjectile = .5f;

    [SerializeField]
    float maxDelayInProjectile = 5f;

    AudioPlayer audioPlayer;

    void Start()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine (firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while (projectilePrefab != null)
        {
            PlayShootingAudio();

            GameObject projectile =
                Instantiate(projectilePrefab,
                transform.position,
                Quaternion.identity);

            Rigidbody2D rg = projectile.GetComponent<Rigidbody2D>();

            if (rg != null)
            {
                rg.velocity = transform.up * projectileSpeed;
            }

            Destroy (projectile, projectileLifeTime);

            float delay = delayInProjectiles;
            if (useAI)
            {
                delay = GetRandomDelayForProjectile();
            }

            yield return new WaitForSecondsRealtime(delay);
        }
    }

    float GetRandomDelayForProjectile()
    {
        return Random.Range(minDelayInProjectile, maxDelayInProjectile);
    }

    void PlayShootingAudio()
    {
        if (audioPlayer != null && gameObject.tag == "Player")
        {
            audioPlayer.PlayShootingAudio();
        }
    }
}

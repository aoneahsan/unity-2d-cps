using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("General Health Setting")]
    [SerializeField]
    int health = 50;

    [SerializeField]
    ParticleSystem hitParticleEffect;

    CameraShake cameraShake;

    [Header("Player Health Setting")]
    [SerializeField]
    bool applyCameraShakeOnDamage = false;

    [SerializeField]
    Slider playerHealthSlider;

    AudioPlayer audioPlayer;

    ScoreKeeper playerScoreKeeper;

    Enemy enemy;

    bool isPlayer;

    bool isEnemy;

    LevelManager levelManager;

    void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        playerScoreKeeper = FindObjectOfType<ScoreKeeper>();
        enemy = GetComponent<Enemy>();
        isPlayer = gameObject.tag == "Player";
        isEnemy = gameObject.tag == "Enemies";
        levelManager = FindObjectOfType<LevelManager>();
        ResetPlayerHealthUI();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            int damage = damageDealer.GetDamage();
            damageDealer.Hit();
            PlayHitEffect();
            ShakeCameraIfPlayer();
            TakeDamage (damage);
        }
    }

    private void TakeDamage(int damage)
    {
        // decrease healt
        health -= damage;

        bool isDead = health <= 0;

        // play damage sound based on different conditions
        if (isEnemy && isDead)
        {
            PlayDamageAudio();

            // add score points to playerScore
            AddEnemyScorePointsToPlayerScore();
        }
        else if (isPlayer)
        {
            UpdatePlayerHealthUI();
            PlayDamageAudio();
            if (isDead && levelManager != null)
            {
                levelManager.SetPlayerScore(playerScoreKeeper.GetPlayerScore());
                levelManager.LoadGameOverScene();
            }
        }

        // remove current gameObject
        if (isDead)
        {
            Destroy (gameObject);
        }
    }

    private void ShakeCameraIfPlayer()
    {
        if (applyCameraShakeOnDamage && cameraShake != null)
        {
            cameraShake.PlayShakeAnimation();
        }
    }

    void PlayHitEffect()
    {
        if (hitParticleEffect != null)
        {
            ParticleSystem hitEffect =
                Instantiate(hitParticleEffect,
                transform.position,
                Quaternion.identity);
            Destroy(hitEffect,
            hitEffect.main.duration + hitEffect.main.startLifetime.constantMax);
        }
    }

    private void PlayDamageAudio()
    {
        if (audioPlayer != null)
        {
            audioPlayer.PlayDamageAudio();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    private void AddEnemyScorePointsToPlayerScore()
    {
        if (enemy != null && playerScoreKeeper != null)
        {
            int enemyScorePoints = enemy.GetEnemyScorePoints();
            playerScoreKeeper.IncreasePlayerScore (enemyScorePoints);
        }
    }

    void ResetPlayerHealthUI()
    {
        if (isPlayer && playerHealthSlider != null)
        {
            playerHealthSlider.minValue = 0;
            playerHealthSlider.maxValue = health;
            playerHealthSlider.value = health;
        }
    }

    void UpdatePlayerHealthUI()
    {
        if (isPlayer && playerHealthSlider != null)
        {
            playerHealthSlider.value = health;
        }
    }
}

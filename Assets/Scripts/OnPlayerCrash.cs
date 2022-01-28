using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnPlayerCrash : MonoBehaviour
{
    [SerializeField]
    float loadDelay = 0.5f;

    [SerializeField]
    ParticleSystem crashEffect;

    [SerializeField]
    AudioClip crashClip;

    bool gameEnded;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !gameEnded)
        {
            gameEnded = true;
            FindObjectOfType<PlayerController>().GameEnded();
            crashEffect.Play();
            GetComponent<AudioSource>().PlayOneShot(crashClip);
            Invoke("ReloadScene", loadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}

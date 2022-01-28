using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField]
    int coinPickupScore = 5;

    bool wasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            IncreasePlayerPoints();
        }
    }

    void IncreasePlayerPoints()
    {
        // increase points
        FindObjectOfType<GameSession>().IncreasePlayerScore(coinPickupScore);

        // hide game object (extra set)
        gameObject.SetActive(false);

        // destroy current coin gameobject
        Destroy (gameObject);
    }
}

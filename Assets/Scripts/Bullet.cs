using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 16f;

    PlayerMovement player;

    float xSpeed;

    Rigidbody2D myRigidbody2D;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidbody2D.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemies")
        {
            Destroy(other.gameObject);
        }
        Destroy (gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy (gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;

    Rigidbody2D myRigidBody2D;

    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody2D.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            moveSpeed = -moveSpeed;

            transform.localScale =
                new Vector2(transform.localScale.x * -1,
                    transform.localScale.y);
        }
    }
}

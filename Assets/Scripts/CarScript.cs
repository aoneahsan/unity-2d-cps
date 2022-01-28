using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    [SerializeField]
    float streeSpeed = 300f;

    [SerializeField]
    float moveSpeed = 20f;

    [SerializeField]
    float slowSpeed = 15f;

    [SerializeField]
    float boostSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        float streeAmount =
            Input.GetAxis("Horizontal") * streeSpeed * Time.deltaTime;
        float moveAmount =
            Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -streeAmount);
        transform.Translate(0, moveAmount, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (moveSpeed != slowSpeed)
        {
            moveSpeed = slowSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boost" && moveSpeed != boostSpeed)
        {
            moveSpeed = boostSpeed;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float torqueAmount = 10;

    [SerializeField]
    float boostSpeed = 50f;

    [SerializeField]
    float slowSpeed = 20f;

    [SerializeField]
    float normalSpeed = 35f;

    Rigidbody2D rg2d;

    SurfaceEffector2D levelSurfaceEffector2d;

    bool gameEnded;

    // Start is called before the first frame update
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        levelSurfaceEffector2d = FindObjectOfType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameEnded)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    public void GameEnded() {
        gameEnded = true;
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rg2d.AddTorque (torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rg2d.AddTorque(-torqueAmount);
        }
    }

    void RespondToBoost()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            levelSurfaceEffector2d.speed = boostSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            levelSurfaceEffector2d.speed = slowSpeed;
        }
        else
        {
            levelSurfaceEffector2d.speed = normalSpeed;
        }
    }
}

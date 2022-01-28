using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;

    [SerializeField]
    float moveSpeed = 10f;

    Camera mainCamera;

    [SerializeField]
    float paddingLeft = .5f;

    [SerializeField]
    float paddingRight = .5f;

    [SerializeField]
    float paddingTop = 4f;

    [SerializeField]
    float paddingBottom = 2f;

    Vector3 minBounds;

    Vector3 maxBounds;

    Shooter shooter;

    void Start()
    {
        mainCamera = Camera.main;
        shooter = GetComponent<Shooter>();
        InitBounds();
    }

    void InitBounds()
    {
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector3 playerPosition = new Vector3();
        playerPosition.x =
            Mathf
                .Clamp(transform.position.x + delta.x,
                minBounds.x + paddingLeft,
                maxBounds.x - paddingRight);
        playerPosition.y =
            Mathf
                .Clamp(transform.position.y + delta.y,
                minBounds.y + paddingBottom,
                maxBounds.y - paddingTop);
        transform.position = playerPosition;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.isFiring = value.isPressed;
        }
    }
}

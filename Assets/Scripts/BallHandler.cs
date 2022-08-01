using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
  [SerializeField] GameObject ballPrefab;
  [SerializeField] float spwanDelay = 1;
  [SerializeField] float despwanDelay = .3f;
  [SerializeField] Rigidbody2D ballPivotRG;

  Camera mainCamera;
  Transform ballTransform;
  Rigidbody2D ballRG;
  SpringJoint2D ballSpringJoint;

  void Start()
  {
    mainCamera = Camera.main;
    SpwanBall();
  }

  void Update()
  {
    if (Touchscreen.current.primaryTouch.press.isPressed && mainCamera != null)
    {
      if (ballRG != null && !ballRG.isKinematic)
      {
        ballRG.bodyType = RigidbodyType2D.Kinematic;
      }

      Vector2 touchinput = Touchscreen.current.primaryTouch.position.ReadValue();

      Vector3 pos = mainCamera.ScreenToWorldPoint(touchinput);

      if (ballTransform != null)
      {
        ballTransform.position = new Vector3(pos.x, pos.y, 0f);
      }
    }
    else
    {
      if (ballRG != null && ballRG.isKinematic)
      {
        ballRG.bodyType = RigidbodyType2D.Dynamic;
      }
    }

    if (Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
    {
      Invoke(nameof(DespwanBall), despwanDelay);
    }
  }

  void DespwanBall()
  {
    if (ballSpringJoint != null)
    {
      ballSpringJoint.enabled = false;
    }

    Invoke(nameof(SpwanBall), spwanDelay);
  }

  void SpwanBall()
  {
    if (ballPrefab != null && ballPivotRG != null)
    {
      ballSpringJoint = null;
      ballRG = null;
      ballTransform = null;

      GameObject ball = Instantiate(ballPrefab, ballPivotRG.transform.position, Quaternion.identity);

      ballTransform = ball.GetComponent<Transform>();
      ballRG = ballTransform.GetComponent<Rigidbody2D>();
      ballSpringJoint = ballTransform.GetComponent<SpringJoint2D>();

      ballSpringJoint.connectedBody = ballPivotRG;
    }

  }
}

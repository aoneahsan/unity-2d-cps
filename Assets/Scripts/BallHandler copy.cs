using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class BallHandlerOLD : MonoBehaviour
{
  [SerializeField] GameObject ballPrefab;
  [SerializeField] float spwanDelay = 1;
  [SerializeField] float despwanDelay = .3f;
  [SerializeField] Rigidbody2D ballPivotRG;

  Camera mainCamera;
  Rigidbody2D ballRG;
  SpringJoint2D ballSpringJoint;

  void Start()
  {
    mainCamera = Camera.main;
    SpwanBall();
  }

  private void OnEnable()
  {
    EnhancedTouchSupport.Enable();
  }

  private void OnDisable()
  {
    EnhancedTouchSupport.Disable();
  }

  void Update()
  {
    if (Touch.activeTouches.Count > 0 && mainCamera != null)
    {
      if (ballRG != null && !ballRG.isKinematic)
      {
        ballRG.bodyType = RigidbodyType2D.Kinematic;
      }

      Vector2 touchinput = new Vector2();

      foreach (Touch touch in Touch.activeTouches)
      {
        touchinput += touch.screenPosition;
      }

      touchinput /= Touch.activeTouches.Count;

      // Vector2 touchinput = Touchscreen.current.primaryTouch.position.ReadValue();

      Vector3 pos = mainCamera.ScreenToWorldPoint(touchinput);

      if (ballRG != null)
      {
        ballRG.position = pos;
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

      GameObject ball = Instantiate(ballPrefab, ballPivotRG.transform.position, Quaternion.identity);

      ballRG = ball.GetComponent<Rigidbody2D>();
      ballSpringJoint = ball.GetComponent<SpringJoint2D>();

      ballSpringJoint.connectedBody = ballPivotRG;
    }

  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;

    Rigidbody2D myRigidbody;

    Animator myAnimator;

    Collider2D myCollider2d;

    SpriteRenderer mySpriteRenderer;

    [SerializeField]
    float runSpeed = 10f;

    [SerializeField]
    float jumpSpeed = 25f;

    [SerializeField]
    float climbSpeed = 10f;

    [SerializeField]
    Sprite playerIdelSprite;

    [SerializeField]
    Sprite playerClimbingSprite;

    string currentVisibleSprite = "idel";

    float gravityAtStart;

    BoxCollider2D myBoxCollider2D;

    bool isAlive = true;

    CapsuleCollider2D myCapsuleCollider2D;

    [SerializeField]
    Vector2 deathSpeed = new Vector2(20f, 20f);

    [SerializeField]
    GameObject bulletGameObject;

    [SerializeField]
    GameObject gunGameObject;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2d = GetComponent<Collider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        gravityAtStart = myRigidbody.gravityScale;
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myCapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (isAlive)
        {
            Run();
            FlipSprite();
            Climbing();
            Die();
        }
    }

    void OnMove(InputValue value)
    {
        if (isAlive)
        {
            moveInput = value.Get<Vector2>();
        }
    }

    void Run()
    {
        Vector2 playerVelocity =
            new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
    }

    void FlipSprite()
    {
        float playerXSpeed = myRigidbody.velocity.x;
        bool isRuning = Mathf.Abs(playerXSpeed) > Mathf.Epsilon;

        if (isRuning)
        {
            float playerDirection = Mathf.Sign(playerXSpeed);
            transform.localScale =
                new Vector2(Mathf.Abs(transform.localScale.x) * playerDirection,
                    transform.localScale.y);

            // set running animation
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            // set running animation false (back to idel)
            myAnimator.SetBool("isRunning", false);
        }
    }

    void OnJump(InputValue value)
    {
        if (isAlive)
        {
            LayerMask groundLayerMask = LayerMask.GetMask("Ground", "Climbing");

            // before (with issue of wall jumping (player able to jump while hanging with wall)
            // bool isTouchingGround = myCollider2d.IsTouchingLayers(groundLayerMask);
            bool isTouchingGround =
                myBoxCollider2D.IsTouchingLayers(groundLayerMask);

            // after (to resolve wall jumping issue) (cause issue as it's set to trigger)
            // bool isTouchingGround =
            //     myBoxCollider2D.IsTouchingLayers(groundLayerMask);
            if (value.isPressed && isTouchingGround)
            {
                myRigidbody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
    }

    void Climbing()
    {
        // float playerYSpeed = moveInput.y;
        // bool isClimbing = Mathf.Abs(playerYSpeed) > Mathf.Epsilon;
        LayerMask ClimbingLayerMask = LayerMask.GetMask("Climbing");

        // before (changed just because changed in course)
        // bool isTouchingClimbingLayer =
        //     myCollider2d.IsTouchingLayers(ClimbingLayerMask);
        // after (box collider is feet collider and other is body collider)
        bool isTouchingClimbingLayer =
            myBoxCollider2D.IsTouchingLayers(ClimbingLayerMask);

        if (isTouchingClimbingLayer)
        {
            if (myRigidbody.gravityScale != 0f)
            {
                myRigidbody.gravityScale = 0f;
            }
            if (currentVisibleSprite != "climbing")
            {
                mySpriteRenderer.sprite = playerClimbingSprite;
                currentVisibleSprite = "climbing";
            }

            Vector2 climbingVelocity =
                new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbingVelocity;

            if (Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon)
            {
                // set climbing animation true
                myAnimator.SetBool("isClimbing", true);
            }
            else
            {
                // set climbing animation false (back to idel)
                myAnimator.SetBool("isClimbing", false);
            }
        }
        else
        {
            if (myRigidbody.gravityScale != gravityAtStart)
            {
                myRigidbody.gravityScale = gravityAtStart;
            }
            if (currentVisibleSprite != "idel")
            {
                mySpriteRenderer.sprite = playerIdelSprite;
                currentVisibleSprite = "idel";
            }

            if (myAnimator.GetBool("isClimbing"))
            {
                // set climbing animation false (back to idel)
                myAnimator.SetBool("isClimbing", false);
            }
        }
    }

    void Die()
    {
        if (
            myCapsuleCollider2D
                .IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards"))
        )
        {
            isAlive = false;
            myAnimator.SetTrigger("Dead");
            myRigidbody.velocity = deathSpeed;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void OnFire(InputValue value)
    {
        if (isAlive)
        {
            Instantiate(bulletGameObject,
            gunGameObject.transform.position,
            transform.rotation);
        }
    }
}

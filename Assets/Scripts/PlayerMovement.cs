using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltRunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 5f;
    [SerializeField] float fltClimbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float fltGravityScaleAtStart;


    bool blnIsAlive = true;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        fltGravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!blnIsAlive) {return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if(!blnIsAlive) {return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void OnMove(InputValue value)
    {
        if (!blnIsAlive) {return; }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!blnIsAlive) {return; }
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2 (0f, fltJumpSpeed);
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * fltRunSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool blnPlayerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("blnIsRunning", blnPlayerHasHorizontalSpeed);
        
    }

    void FlipSprite()
    {
        bool blnPlayerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (blnPlayerHasHorizontalSpeed)
        {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
    void ClimbLadder()
        {
            if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                myRigidBody.gravityScale = fltGravityScaleAtStart;
                myAnimator.SetBool("blnIsClimbing", false);
                return;
            }
            Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * fltClimbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0f;
            bool blnPlayerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("blnIsClimbing", blnPlayerHasVerticalSpeed);
        }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            blnIsAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;
        }
    }
}

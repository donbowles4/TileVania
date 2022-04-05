using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float fltRunSpeed = 10f;
    [SerializeField] float fltJumpSpeed = 5f;
    [SerializeField] float fltClimbSpeed = 5f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    float fltGravityScaleAtStart;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        fltGravityScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){return;}
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
            if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
            {
                myRigidBody.gravityScale = fltGravityScaleAtStart;
                return;
            }
            Vector2 climbVelocity = new Vector2 (myRigidBody.velocity.x, moveInput.y * fltClimbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0f;
        }
}

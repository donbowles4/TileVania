using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float fltMoveSpeed = 1f;



    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2 (fltMoveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        fltMoveSpeed = -fltMoveSpeed;
        FlipEnemyFacing();
    }
    
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float fltBulletSpeed = 20f;
    Rigidbody2D myRigidBody;
    PlayerMovement player;
    float fltXSpeed;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        fltXSpeed = player.transform.localScale.x * fltBulletSpeed;
    }


    void Update()
    {
        myRigidBody.velocity = new Vector2 (fltXSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        Destroy(gameObject);
    }
}

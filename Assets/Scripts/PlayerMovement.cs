using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private BoxCollider2D playerHitBox;
    [SerializeField] private float movementSpeed =4;
    private Rigidbody2D body;
    private SpriteRenderer spi;
    private Animator anim;
    [SerializeField] private float jumpForce =20;

    [SerializeField] private AudioSource jumpSound;

    [SerializeField] private LayerMask jumpableGround;

    private int jumpNumber;

    private bool facingLeft;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spi = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //playerHitBox = GetComponent<BoxCollider2D>();
    }

    

    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * movementSpeed, body.velocity.y);//Movement

       
        if (Input.GetButtonDown("Jump") && jumpNumber < 2)
        {
            jumpSound.Play();
            body.velocity = new Vector2(body.velocity.x, jumpForce);//Jump
            jumpNumber++;                
        }

        
       

        if (body.velocity.x < 0f && !facingLeft)
        {
            spi.flipX = true;
            facingLeft = true;
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        if (body.velocity.x > 0f && facingLeft)
        {
            spi.flipX = false;
            facingLeft = false;
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        //brute force if statement for right running w/out flipping
        if (body.velocity.x > 0f && !facingLeft)
        {
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        //brute force if statement for left running w/out flipping
        if (body.velocity.x < 0f && facingLeft)
        {
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        if (body.velocity.x == 0f)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Running", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        }

        if (body.velocity.y > .5f)
        {
            anim.SetBool("Jumping", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Falling", false);
            anim.SetBool("Running", false);
        }

        if (body.velocity.y < -1f)
        {
            anim.SetBool("Falling", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Jumping", false);
            anim.SetBool("Running", false);
        }

    }

    //private bool IsGrounded()
    //{
    //    return Physics2D.BoxCast(playerHitBox.bounds.center, playerHitBox.bounds.size, 0f, Vector2.down, -1f, jumpableGround);

    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Test"))
        {
            jumpNumber = 0;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

      private Rigidbody2D rd2d;
      public GameObject bullet;

      public bool flipX;
      public bool alive;
      private bool climbing;
      private SpriteRenderer flip;

      public float speed;

      public float rayDistance;

      public AudioClip Jump;
      public AudioClip Climb;

     public AudioSource musicSource;

     Animator animator;
     Vector2 lookDirection = new Vector2(1,0);
     

    void Start()
    {
         rd2d = GetComponent<Rigidbody2D>();
         flip = GetComponent<SpriteRenderer>();
         animator = GetComponent<Animator>();
         alive = true;
    }


    void FixedUpdate()
    {

        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        animator.SetFloat("Look X", lookDirection.x);

        RaycastHit2D hitBox = Physics2D.Raycast(transform.position, Vector2.up, rayDistance, LayerMask.GetMask("Ladder"));

    //Direction for the sprite


      if (Input.GetKeyDown(KeyCode.D))
            {
                flip.flipX = false;
            }

      if (Input.GetKeyDown(KeyCode.A))
        {
            if (flip != null)
            {
                 flip.flipX = true;
            }
        }

        //If statement for ladder
      if (hitBox.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                climbing = true;
            }
        }
     else
        {
            climbing = false;
        }

     if (climbing == true)
      {
        vertMovement = Input.GetAxis("Vertical");
        rd2d.velocity = new Vector2(rd2d.velocity.x, vertMovement * speed);
        rd2d.gravityScale = 0;
      }

      else 
      {
          rd2d.gravityScale = 5;
      }

     }

    //for player jumping
    private void OnCollisionStay2D(Collision2D collision)
    {


        if (collision.collider.tag == "Platform")
        {


            if (Input.GetKey(KeyCode.W))
            {
                 rd2d.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                 musicSource.clip = Jump;
                 musicSource.Play();
            }
            
        }

         if (collision.collider.tag == "Ground")
        {


            if (Input.GetKey(KeyCode.W))
            {
                 rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                 musicSource.clip = Jump;
                 musicSource.Play();
            } 
        }
      }
}

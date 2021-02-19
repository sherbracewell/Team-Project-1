using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBoyController : MonoBehaviour
{
    private Rigidbody2D rd2d;

     public float speed;
     float hozMovement;
     float vertMovement;
     private bool climbing;
     public AudioClip jumpSound;

     public AudioSource musicSource;

     public bool flipX;
     private SpriteRenderer flippy;

     Animator animator;
     Vector2 lookDirection = new Vector2(1,0);
     public float rayDistance;

    // Start is called before the first frame update
    void Start()
    {
         rd2d = GetComponent<Rigidbody2D>();
         flippy = GetComponent<SpriteRenderer>();
         animator = GetComponent<Animator>();
    }

  void Update()
    {
      if (Input.GetKey("escape"))
         {
            Application.Quit();
         }

         RaycastHit2D hitBox = Physics2D.Raycast(transform.position, Vector2.up, rayDistance, LayerMask.GetMask("Ladder"));

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
        animator.SetTrigger("Climbing");
        rd2d.velocity = new Vector2(rd2d.velocity.x, vertMovement * speed);
        rd2d.gravityScale = 0;
      }

      else 
      {
          rd2d.gravityScale = 1;
      }

    }

  void FixedUpdate()
    {
         hozMovement = Input.GetAxis("Horizontal");
         vertMovement = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(hozMovement, vertMovement);
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

      if (Input.GetKeyDown(KeyCode.D))
            {
                flippy.flipX = false;
            }

      if (Input.GetKeyDown(KeyCode.A))
        {
            if (flippy != null)
            {
                 flippy.flipX = true;
            }
        }

    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {


            if (Input.GetKey(KeyCode.W))
            {
                 rd2d.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
                 animator.SetTrigger("Jumping");
                 musicSource.clip = jumpSound;
                 musicSource.Play();
            }

        }
        
         if (collision.collider.tag == "Platforms")
        {


            if (Input.GetKeyDown(KeyCode.W))
            {
                 rd2d.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                 animator.SetTrigger("Jumping");
                 musicSource.clip = jumpSound;
                 musicSource.Play();
            }
        }
    }
}

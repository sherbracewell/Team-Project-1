using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketBoyController : MonoBehaviour
{
    private Rigidbody2D rd2d;

     public float speed;
     float hozMovement;
     float vertMovement;
     private bool climbing;

     public int Lives { get { return currentLives; }}
     int currentLives;
     public int maxLives = 3;
     public Text livesText;  

     public AudioClip jumpSound;
     public AudioClip hitSound;
     public AudioClip shootSound;
     public AudioClip pickUp;
     public AudioSource musicSource;

     public bool flipX;
     private SpriteRenderer flippy;

     Animator animator;
     Vector2 lookDirection = new Vector2(1,0);
     public float rayDistance;

     public GameObject bulletPrefab;

     public GameObject cameraOne;
     public GameObject cameraTwo;
     public GameObject winGame;
     public GameObject loseGame;
     public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
         rd2d = GetComponent<Rigidbody2D>();
         flippy = GetComponent<SpriteRenderer>();
         animator = GetComponent<Animator>();
         currentLives = maxLives;
         livesText.text = "Lives: " + currentLives.ToString();
    }

  void Update()
    {
      if (Input.GetKeyDown("escape"))
         {
            pauseMenu.SetActive(true);
            gameObject.SetActive(false);
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

      if (Input.GetKey(KeyCode.D))
            {
                flippy.flipX = false;
                lookDirection = new Vector2(1,0);
            }

      if (Input.GetKey(KeyCode.A))
        {
            if (flippy != null)
            {
                 flippy.flipX = true;
                 lookDirection = new Vector2(-1,0);
            }
        }

    if(Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }

    }
    
    public void Health(int amount)
    {
        currentLives = Mathf.Clamp(currentLives + amount, 0, maxLives);
        livesText.text = "Lives: " + currentLives.ToString(); 

        if (amount < 0)
        {
         animator.SetTrigger("Hit");
         PlaySound(hitSound);
        }

        if (amount > 0)
        {
            PlaySound(pickUp);
        }
        if (currentLives == 0)
        {
            gameObject.SetActive(false);
            cameraOne.SetActive(false);
            cameraTwo.SetActive(false);
            loseGame.SetActive(true);
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
                 PlaySound(jumpSound);
            }

        }
        
         if (collision.collider.tag == "Platforms")
        {


            if (Input.GetKeyDown(KeyCode.W))
            {
                 rd2d.AddForce(new Vector2(0, 7), ForceMode2D.Impulse);
                 animator.SetTrigger("Jumping");
                 PlaySound(jumpSound);
            }
        }

         if (collision.collider.tag == "Transition")
        { 
            transform.position = new Vector2(54.64f, -3.9f);
            cameraOne.SetActive(false);
            cameraTwo.SetActive(true);
        }

    }

    public void PlaySound(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }

     void Shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, rd2d.position + Vector2.up * 0.01f, Quaternion.identity);

        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.Shoot(lookDirection, 300);

        animator.SetTrigger("Shooting");
        
        PlaySound(shootSound);
    }

    public void Winner()
    {
        gameObject.SetActive(false);
        cameraTwo.SetActive(false);
        winGame.SetActive(true);
    }
    
}

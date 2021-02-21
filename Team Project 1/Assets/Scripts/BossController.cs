using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    
    public float speed;
    float timer;
    public float changeTime = 3.0f;
    Rigidbody2D rd2d;
    int direction = 1;
    public ParticleSystem boomboom;
     public int Health { get { return enemyHealth; }}
     int enemyHealth;
     public int maxHealth = 3;

     public AudioSource musicSource;
     public AudioClip injured;
     public RocketBoyController player;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        enemyHealth = maxHealth;
    }
    
    void Update()
    {
         timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    
    void FixedUpdate()
    {
        Vector2 position = rd2d.position;
        position.x = position.x + Time.deltaTime * speed * direction;
        rd2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RocketBoyController thePlayer = other.gameObject.GetComponent<RocketBoyController >();

        if (thePlayer != null)
        {
            thePlayer.Health(-2);
        }
    }
    

    public void Hit(int amount)
    {
        
            enemyHealth = Mathf.Clamp(enemyHealth + amount, 0, maxHealth);
        
        if (amount < 0)
        {
            PlaySound(injured);

            if (enemyHealth == 0)
            {
                Destroy(gameObject);
                ParticleSystem enemy = Instantiate(boomboom, rd2d.position + Vector2.up * 0.5f, Quaternion.identity);
                boomboom.Play();
                player.GetComponent<RocketBoyController>().Winner();
            }
        }   
    }
    public void PlaySound(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
}


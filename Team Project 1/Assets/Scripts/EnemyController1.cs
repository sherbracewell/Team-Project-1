using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    public float speed;
    float timer;
    public float changeTime = 3.0f;
    Rigidbody2D rd2d;
    int direction = 1;
    public ParticleSystem boom;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
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
            thePlayer.Health(-1);
        }
    }
    

    public void Destroy()
    {
        Destroy(gameObject);
        ParticleSystem enemy = Instantiate(boom, rd2d.position + Vector2.up * 0.5f, Quaternion.identity);
        boom.Play();
    }
}

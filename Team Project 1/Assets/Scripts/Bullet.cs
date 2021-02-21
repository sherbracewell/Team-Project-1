using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rd2d;
    
    void Awake()
    {
        rd2d = GetComponent<Rigidbody2D>();
    }
    
    public void Shoot(Vector2 direction, float force)
    {
        rd2d.AddForce(direction * force);
    }
    
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController1 enemy = other.collider.GetComponent<EnemyController1>();

        if (enemy != null)
        {
            enemy.Destroy();
        }

        BossController boss = other.collider.GetComponent<BossController>();

        if (boss != null)
        {
            boss.Hit(-1);
        }

        Destroy(gameObject);


    }
}
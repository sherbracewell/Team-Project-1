using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public AudioClip pickUp;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        RocketBoyController controller = other.GetComponent<RocketBoyController>();

        if (controller != null)
        {
            if (controller.Lives < controller.maxLives)
            {
                controller.Health(1);
                Destroy(gameObject);
            
                controller.PlaySound(pickUp);
            }
        }
    }
}
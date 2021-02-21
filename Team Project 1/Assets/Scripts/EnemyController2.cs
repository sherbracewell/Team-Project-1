using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        RocketBoyController thePlayer = other.gameObject.GetComponent<RocketBoyController >();

        if (thePlayer != null)
        {
            thePlayer.Health(-1);
        }
    }
}

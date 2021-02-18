using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public Text gameOverText;

    void Start()
    {
        gameOverText.text = "";
    
    }

    public void gameEnded()
        {
          if (gameOver == true)
          
          {
             gameOverText.gameObject.SetActive(true);
             gameOverText.text = "Game Over!";
          }

        }
}

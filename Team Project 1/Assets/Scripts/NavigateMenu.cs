﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateMenu : MonoBehaviour
{
   public GameObject pauseMenu;
   public GameObject player;
  public void SwitchScene(string sceneName)
     {
        SceneManager.LoadScene(sceneName);
     }

   public void ExitGame()
   {
      Application.Quit();
   }

   public void Resume()
   {
      pauseMenu.SetActive(false);
      player.SetActive(true);
   }
}


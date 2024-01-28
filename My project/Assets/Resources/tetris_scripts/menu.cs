using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    public void PlaY()
   {
        SceneManager.LoadScene("tetris_scene");
   }
   public void QuiteGame()
   {
        Application.Quit();
   }
     public void Results()
   {
        SceneManager.LoadScene("LeaderboardScene");
   }
}

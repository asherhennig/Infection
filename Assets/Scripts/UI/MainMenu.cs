using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    public GameObject game;

    void Start()
    {
        Time.timeScale = 1;
        game = GameObject.FindGameObjectWithTag("gameManager");
    }

    public void PlayButton()
    {
        if (game.GetComponent<GameManager>().level == 1)
        {
            SceneManager.LoadScene("Lab.2");
        }
        else if(game.GetComponent<GameManager>().level == 2)
        {
            SceneManager.LoadScene("Level2_Forest");
        }
        else if (game.GetComponent<GameManager>().level == 3)
        {
            SceneManager.LoadScene("Level3_LaunchPad");
        }
                
    }
    
    public void quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

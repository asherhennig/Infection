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
    private GameObject game;
    public GameObject tutorialPanel;
    public GameObject gameManager;

    void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayButton()
    {
        if (gameManager.GetComponent<GameManager>().level == 1)
        {
            tutorialPanel.SetActive(true);
        }
        else if(gameManager.GetComponent<GameManager>().level == 2)
        {
            SceneManager.LoadScene("Forest");
        }
        else if (gameManager.GetComponent<GameManager>().level == 3)
        {
            SceneManager.LoadScene("LaunchPad");
        }
                
    }

    public void playTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void playGame()
    {
        SceneManager.LoadScene("Lab");
    }
    
    public void quit()
    {
        Application.Quit();
    }
}

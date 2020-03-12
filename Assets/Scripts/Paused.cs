using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Paused : MonoBehaviour
{
    private bool isPaused = false;
    GameObject[] pauseMenu;
    GameObject[] pauseButton;
    GameObject[] Options;


    void Start()
    {
        Time.timeScale = 1;
        pauseMenu = GameObject.FindGameObjectsWithTag("ShowOnPause");
        pauseButton = GameObject.FindGameObjectsWithTag("HideOnPause");
        Options = GameObject.FindGameObjectsWithTag("Option");
        hidePaused();
        hideOptions();
    }

    public void Option()
    {
        Time.timeScale = 0;
        showOptions();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        showPaused();
    }
     public void Unpause()
     {
        Time.timeScale = 1;
        isPaused = false;
        hidePaused();
        hideOptions();
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    void Update()
    {
        
    }
    public void showPaused()
    {
        foreach (GameObject g in pauseMenu)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in pauseButton)
        {
            g.SetActive(false);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject g in pauseMenu)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in pauseButton)
        {
            g.SetActive(true);
        }
    }

    public void showOptions()
    {
        Debug.Log("Showoptions");
        foreach (GameObject g in Options)
        {
            g.SetActive(true);
        }
    }

    public void hideOptions()
    {
        Debug.Log("hideoptions");
        foreach (GameObject g in Options)
        {
            g.SetActive(false);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hideOptions();
    }

    public void Exit()
    {
        SceneManager.LoadScene(sceneName:"Main menu");
        hideOptions();
    }
}


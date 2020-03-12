using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class statsScreen : MonoBehaviour
{
    private bool isPaused = false;
    GameObject[] showStatsScreenPostWave;
    GameObject[] showStatsScreenPostLevel;
    GameObject[] continueButtonWave;
    GameObject[] continueButtonLevel;

    // Start is called before the first frame update
    void Start()
    {
        showStatsScreenPostWave = GameObject.FindGameObjectsWithTag("postWave");
        showStatsScreenPostLevel = GameObject.FindGameObjectsWithTag("postLevel");
        continueButtonWave = GameObject.FindGameObjectsWithTag("postWave");
        continueButtonLevel = GameObject.FindGameObjectsWithTag("postLevel");
        hideStatsWave();
        hideStatsLevel();
    }

    public void showStatScreenWave()
    {
        Time.timeScale = 0;
        displayStatsWave();
    }

    public void showStatsScreenLevel()
    {
        Time.timeScale = 0;

    }

    public void showStats()
    {
        displayStatsWave();
    }

    public void displayStatsWave()
    {
        //Debug.Log("displayStats2 " + showStatsScreenPostWave.Length);
        foreach (GameObject g in showStatsScreenPostWave)
        {
            g.SetActive(true);
        }
    }

    public void displayStatsLevel()
    {
        foreach (GameObject g in showStatsScreenPostLevel)
        {
            g.SetActive(true);
        }
    }

    //this is called simply to hide the menu when the game starts
    public void hideStatsWave()
    {
        foreach (GameObject g in showStatsScreenPostWave)
        {
            Debug.Log("displayStats5675");
            g.SetActive(false);
        }
    }

    //this is called simply to hide the menu when the game starts
    public void hideStatsLevel()
    {
        foreach (GameObject g in showStatsScreenPostLevel)
        {
            g.SetActive(false);
        }
    }

    public void continueWave()
    {
        foreach (GameObject g in continueButtonWave)
        {
            g.SetActive(false);
        }
    }

    public void continueLevel()
    {
        foreach (GameObject g in continueButtonLevel)
        {
            g.SetActive(false);
        }
    }
}

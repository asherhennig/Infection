﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;

    void Start()
    {
        Time.timeScale = 1;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }

        audioManager.PlaySound("MenuBGM");
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

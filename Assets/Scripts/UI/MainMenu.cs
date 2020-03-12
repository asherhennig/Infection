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
    private AudioManager audioManager;

    void Start()
    {
        Time.timeScale = 1;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }

        StartCoroutine(Delay(0.1f)); //Play the music after a delay
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

    //For adding a delay to when the music starts
    IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);

        audioManager.Loop();
        audioManager.PlaySound("MenuBGM");
    }
}

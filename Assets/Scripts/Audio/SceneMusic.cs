using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusic : MonoBehaviour
{
    AudioManager audioManager;
    Scene currentScene;
    string sceneName;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }

        StartCoroutine(PlayMusic(0.1f));
    }

    IEnumerator PlayMusic(float time)
    {
        yield return new WaitForSeconds(time);

        if (sceneName == "Main menu")
        {
            audioManager.PlaySound("MenuBGM");
        }
        else if (sceneName == "Lab.2")
        {
            audioManager.PlaySound("LabBGM");
        }
        else if (sceneName == "Level2_Forest")
        {
            audioManager.PlaySound("ForestBGM");
        }
        //else if (sceneName == "") // Add when there is a launchpad scene
        //{
        //    audioManager.PlaySound("LaunchpadBGM");
        //}
    }
}

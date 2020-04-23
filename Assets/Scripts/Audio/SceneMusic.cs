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

        StartCoroutine(PlayMusic(0.1f));
    }

    IEnumerator PlayMusic(float time)
    {
        yield return new WaitForSeconds(time);

        if (sceneName == "Main menu")
        {
            audioManager.PlaySound("MenuBGM");
        }
        else if (sceneName == "Lab")
        {
            audioManager.PlaySound("LabBGM");
        }
        else if (sceneName == "Forest")
        {
            audioManager.PlaySound("ForestBGM");
        }
        else if (sceneName == "LaunchPad")
        {
            audioManager.PlaySound("LaunchpadBGM");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    
    private float volume = 0.7f;

    public AudioSource source;

    public void SetSource(AudioSource audioSource)
    {
        source = audioSource;
        source.clip = clip;
    }

    public void SetVolume(float vol)
    {
        volume = vol;
        source.volume = vol;
    }

    public void Play()
    {
        source.volume = volume;

        if (!source.isPlaying)
        {
            source.Play();
        }
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Toggle toggle;
    public Slider slider;
    
    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            go.transform.SetParent(this.transform);
            sounds[i].SetSource(go.AddComponent<AudioSource>());
        }

        toggle.GetComponent<Toggle>();
        slider.GetComponent<Slider>();

        toggle.isOn = false;
        slider.value = 1;
    }

    public void PlaySound(string soundName)
    {
        for (int i = 0;  i < sounds.Length;  i++)
        {
            if (sounds[i].name == soundName)
            {
                sounds[i].Play();

                if (sounds[i].name == "MenuBGM" || sounds[i].name == "LabBGM" || sounds[i].name == "ForestBGM" || sounds[i].name == "LaunchpadBGM")
                {
                    sounds[i].source.loop = true;
                }

                return;
            }
        }

        // no sound with soundName
        Debug.Log("AudioManager: Sound not found in list, " + soundName);
    }

    public void SetVolume(Slider slider)
    {
        if (toggle.isOn)
        {
            slider.value = 0;
        }
        else
        {
            toggle.isOn = false;

            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i].SetVolume(slider.value);
            }
        }
    }

    public void MuteButton(Toggle toggle)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].SetVolume(0);
            slider.value = 0;
        }
    }
}

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
        if (source.isPlaying == false)
        {
            source.volume = volume;
            source.Play();
        }
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Toggle SFXtoggle;
    public Slider SFXslider;
    public Toggle BGMtoggle;
    public Slider BGMslider;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            go.transform.SetParent(this.transform);
            sounds[i].SetSource(go.AddComponent<AudioSource>());
        }

        SFXtoggle.GetComponent<Toggle>();
        SFXslider.GetComponent<Slider>();

        SFXtoggle.isOn = GetBool("SFXToggle");
        SFXslider.value = PlayerPrefs.GetFloat("SFXSlider");

        BGMtoggle.GetComponent<Toggle>();
        BGMslider.GetComponent<Slider>();

        BGMtoggle.isOn = GetBool("BGMToggle");
        BGMslider.value = PlayerPrefs.GetFloat("BGMSlider");
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
    }

    public void SetSFXVolume()
    {
        if (SFXtoggle.isOn)
        {
            SFXslider.value = 0;
            PlayerPrefs.SetFloat("SFXSlider", SFXslider.value);
            SetBool("SFXToggle", true);
        }
        else
        {
            SFXtoggle.isOn = false;
            SetBool("SFXToggle", false);

            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name != "MenuBGM" && sounds[i].name != "LabBGM" && sounds[i].name != "ForestBGM" && sounds[i].name != "LaunchpadBGM")
                {
                    sounds[i].SetVolume(SFXslider.value);
                    PlayerPrefs.SetFloat("SFXSlider", SFXslider.value);
                }
            }
        }
    }

    public void MuteSFXButton()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name != "MenuBGM" && sounds[i].name != "LabBGM" && sounds[i].name != "ForestBGM" && sounds[i].name != "LaunchpadBGM")
            {
                sounds[i].SetVolume(0);
                SFXslider.value = 0;
                PlayerPrefs.SetFloat("SFXSlider", SFXslider.value);
                SetBool("SFXToggle", true);
            }
        }
    }

    public void SetBGMVolume()
    {
        if (BGMtoggle.isOn)
        {
            BGMslider.value = 0;
            PlayerPrefs.SetFloat("BGMSlider", BGMslider.value);
            SetBool("BGMToggle", true);
        }
        else
        {
            BGMtoggle.isOn = false;
            SetBool("BGMToggle", false);

            for (int i = 0; i < sounds.Length; i++)
            {
                if (sounds[i].name == "MenuBGM" || sounds[i].name == "LabBGM" || sounds[i].name == "ForestBGM" || sounds[i].name == "LaunchpadBGM")
                {
                    sounds[i].SetVolume(BGMslider.value);
                    PlayerPrefs.SetFloat("BGMSlider", BGMslider.value);
                }
            }
        }
    }

    public void MuteBGMButton()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == "MenuBGM" || sounds[i].name == "LabBGM" || sounds[i].name == "ForestBGM" || sounds[i].name == "LaunchpadBGM")
            {
                sounds[i].SetVolume(0);
                BGMslider.value = 0;
                PlayerPrefs.SetFloat("BGMSlider", BGMslider.value);
                SetBool("BGMToggle", true);
            }
        }
    }

    public static void SetBool(string key, bool state)
    {
        PlayerPrefs.SetInt(key, state ? 1 : 0);
    }

    public static bool GetBool(string key)
    {
        int value = PlayerPrefs.GetInt(key);

        if (value == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

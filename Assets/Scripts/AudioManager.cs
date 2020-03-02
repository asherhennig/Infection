﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;

    private AudioSource source;

    public void SetSource(AudioSource audioSource)
    {
        source = audioSource;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume;
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

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
    }

    public void PlaySound(string soundName)
    {
        for (int i = 0;  i < sounds.Length;  i++)
        {
            if (sounds[i].name == soundName)
            {
                sounds[i].Play();
                return;
            }
        }

        // no sound with soundName
        Debug.Log("AudioManager: Sound not found in list, " + soundName);
    }
}

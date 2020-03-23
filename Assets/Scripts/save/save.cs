using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    GameObject Player;
    GameObject[] Ammo;
    //variables to save
    int health;
    int shotgunAmmo;
    int grenadeAmmo;
    int score;
    int money;
    int maxHealth;

    void Start()
    {
        //find the game objects with the tags so you can access the variables in the ammo and player script
        Player = GameObject.FindGameObjectWithTag("Player");
        //Ammo = GameObject.FindGameObjectsWithTag("");
        // sets variables in the save script to be the variables in other scripts wanting to save
        int health = GetComponent<Player>().curHealth;
        int shotgunAmmo = GetComponent<Ammo>().shotgunAmmo;
        int grenadeAmmo = GetComponent<Ammo>().grenadeAmmo;
        int score = GetComponent<GameManager>().score;
        int money = GetComponent<GameManager>().bubblegum;
        int maxHealth = GetComponent<Player>().maxHealth;
    }

    public void load()
    {
        //load previous player prefs
        PlayerPrefs.GetInt("health");
        PlayerPrefs.GetInt("shot gun ammo");
        PlayerPrefs.GetInt("score");
        PlayerPrefs.GetInt("bubblegum");
        PlayerPrefs.GetInt("max health");


    }

    public void save()
    {
        //set player prefs for current state of the game
        PlayerPrefs.SetInt("health",health);
        PlayerPrefs.SetInt("shot gun ammo", shotgunAmmo);
        PlayerPrefs.SetInt("gernade", grenadeAmmo);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("bubblegum", money);
        PlayerPrefs.SetInt("max health", maxHealth);
    }
}

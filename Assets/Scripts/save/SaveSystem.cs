using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public GameObject Player;
  
    //variables to save
    int health;
    int shotgunAmmo;
    int grenadeAmmo;
    int score;
    int money;
    int maxHealth;
    int difficulty;
    int level;
    float difficultymod;

    void Start()
    {
        //find the game objects with the tags so you can access the variables in the ammo and player script
        Player = GameObject.FindGameObjectWithTag("Player");
       
        //sets variables in the save script to be the variables in other scripts wanting to save
        health = Player.GetComponent<Player>().curHealth;
        shotgunAmmo = Player.GetComponent<Ammo>().shotgunAmmo;
        grenadeAmmo = Player.GetComponent<Ammo>().grenadeAmmo;
        score = GetComponent<GameManager>().score;
        money = GetComponent<GameManager>().bubblegum;
        maxHealth = Player.GetComponent<Player>().maxHealth;
        difficulty = GetComponent<GameManager>().curDifficulty;
        difficultymod = GetComponent<GameManager>().difficultyMod;
        level = GetComponent<GameManager>().level;
    }

    public void gameLoad()
    {
        //load previous player prefs
        health = PlayerPrefs.GetInt("health");
        shotgunAmmo = PlayerPrefs.GetInt("shot gun ammo");
        score = PlayerPrefs.GetInt("score");
        money = PlayerPrefs.GetInt("bubblegum");
        maxHealth = PlayerPrefs.GetInt("max health");
        difficulty = PlayerPrefs.GetInt("difficulty");
        level = PlayerPrefs.GetInt("level");
        difficultymod = PlayerPrefs.GetFloat("difMod");
        Debug.Log(health);

    }

    public void gameSave()
    {
        //set player prefs for current state of the game
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("shot gun ammo", shotgunAmmo);
        PlayerPrefs.SetInt("gernade", grenadeAmmo);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("bubblegum", money);
        PlayerPrefs.SetInt("max health", maxHealth);
        PlayerPrefs.SetInt("difficulty", difficulty);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetFloat("difMod", difficultymod);
        PlayerPrefs.Save();
        Debug.Log(health);
        Debug.Log(money);
    }
}



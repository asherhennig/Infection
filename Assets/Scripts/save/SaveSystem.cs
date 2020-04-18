using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {

    }

    public void gameLoad()
    {
        //find the game objects with the tags so you can access the variables in the ammo and player script
        Player = GameObject.FindGameObjectWithTag("Player");
        //load previous player prefs
        Player.GetComponent<Player>().curHealth = PlayerPrefs.GetInt("health");
        Player.GetComponent<Ammo>().shotgunAmmo = PlayerPrefs.GetInt("shot gun ammo");
        GetComponent<GameManager>().score = PlayerPrefs.GetInt("score");
        GameManager.totalBubblegum = PlayerPrefs.GetInt("bubblegum");
        Player.GetComponent<Player>().maxHealth = PlayerPrefs.GetInt("max health");
        GetComponent<GameManager>().curDifficulty = PlayerPrefs.GetInt("difficulty");
        GetComponent<GameManager>().level = PlayerPrefs.GetInt("level");
        GetComponent<GameManager>().difficultyMod = PlayerPrefs.GetFloat("difMod");
        Player.GetComponent<Ammo>().grenadeAmmo = PlayerPrefs.GetInt("gernade");
    }

    public void gameSave()
    {
        //find the game objects with the tags so you can access the variables in the ammo and player script
        Player = GameObject.FindGameObjectWithTag("Player");

        
        //set player prefs for current state of the game
        PlayerPrefs.SetInt("health", Player.GetComponent<Player>().curHealth);
        PlayerPrefs.SetInt("shot gun ammo", Player.GetComponent<Ammo>().shotgunAmmo);
        PlayerPrefs.SetInt("gernade", Player.GetComponent<Ammo>().grenadeAmmo);
        PlayerPrefs.SetInt("score", GetComponent<GameManager>().score);
        PlayerPrefs.SetInt("bubblegum", GameManager.totalBubblegum);
        PlayerPrefs.SetInt("max health", Player.GetComponent<Player>().maxHealth);
        PlayerPrefs.SetInt("difficulty", GetComponent<GameManager>().curDifficulty);
        PlayerPrefs.SetInt("level", GetComponent<GameManager>().level);
        PlayerPrefs.SetFloat("difMod", GetComponent<GameManager>().difficultyMod);
        PlayerPrefs.Save();
    }
}



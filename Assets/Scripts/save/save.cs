using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Ammo;
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
        Ammo = GameObject.FindGameObjectsWithTag("Gun");
        //sets variables in the save script to be the variables in other scripts wanting to save
        int health = Player.GetComponent<Player>().curHealth;
        int shotgunAmmo = Ammo[0].GetComponent<Ammo>().shotgunAmmo;
        int grenadeAmmo = Ammo[1].GetComponent<Ammo>().grenadeAmmo;
        int score = GetComponent<GameManager>().score;
        int money = GetComponent<GameManager>().bubblegum;
        //int maxHealth = Player.GetComponent<Player>().maxHealth;
    }

    public void gameLoad()
    {
        //load previous player prefs
        PlayerPrefs.GetInt("health");
        PlayerPrefs.GetInt("shot gun ammo");
        PlayerPrefs.GetInt("score");
        PlayerPrefs.GetInt("bubblegum");
        PlayerPrefs.GetInt("max health");


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
    }
}



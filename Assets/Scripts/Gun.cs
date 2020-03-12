﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int weaponDam;
    public GameObject bulletPrefab;
    //so the gun gets the components off of player
    public GameObject player;
    public Transform firePosition;
    public float fireSpeed = 0.75f;
    bool miniFire = false;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pistol update:" + weaponDam);
            if (!IsInvoking("fireBullet"))
            {
                InvokeRepeating("fireBullet", 0f, fireSpeed);

                // Play audio when bullet is fired
                audioManager.PlaySound("LaserSound");
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet");
        }
    }


    public void fireBullet()
    {
        Debug.Log("Firing");
        // 1   
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        // 2   
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = firePosition.rotation;
        // 3   
        bullet.GetComponent<Rigidbody>().velocity =
            transform.forward * 10;
        //4
        bullet.GetComponent<bullet>().damage = weaponDam;
        Debug.Log("fire");
    }

 

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int weaponDam;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public Ammo ammo;
    public float fireSpeed = 0.75f;
    public float bulletSpeed = 10.0f;

    [HideInInspector]
    public AudioManager audioManager;

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
    }

    public void fire()
    {
        InvokeRepeating("fireBullet", 0f, fireSpeed);
    }

    public void stopFiring()
    {
        CancelInvoke("fireBullet");
    }
    


    public void fireBullet()
    {   // 1   
        
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;

        if (Player.minigunFiring == true)
        {
            Destroy(bullet, 1f);
        }
        else
        {
            Destroy(bullet, 4f)
        }
        // 2   
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = firePosition.rotation;
        // 3   
        bullet.GetComponent<Rigidbody>().velocity =
            transform.forward * bulletSpeed;
        //4
        bullet.GetComponent<bullet>().damage = weaponDam;

        // Play audio when bullet is fired
        audioManager.PlaySound("LaserSound");
    }
}

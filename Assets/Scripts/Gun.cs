using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int weaponDam;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public float fireSpeed = 0.75f;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInvoking("fireBullet"))
            {
                InvokeRepeating("fireBullet", 0f, fireSpeed);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet");
        }
    }

    void fireBullet()
    {   // 1   
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        // 2   
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = firePosition.rotation;
        // 3   
        bullet.GetComponent<Rigidbody>().velocity =
            transform.forward * 10;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
         if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemyBase>().takeDamage(weaponDam);
            Destroy(gameObject);
        }
        
    }
}

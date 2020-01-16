using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePosition;
    public float fireSpeed = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        // 3   
        bullet.GetComponent<Rigidbody>().velocity =      
            transform.forward * 100;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int weaponDam;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public float fireSpeed = 0.75f;
    public float bulletSpeed = 10.0f;
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
        fire();
    }

    public void fire()
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

   public void miniGunFire()
    {
        //acrtivate mini gun
        gameObject.GetComponent<GunEquipper>().activeMiniGun();
        //start corutine to constantly fire
        StartCoroutine("miniGunAttack");
        //stop the firing
        CancelInvoke("fireBullet");
    }

    void fireBullet()
    {   // 1   
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        // 2   
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = firePosition.rotation;
        // 3   
        bullet.GetComponent<Rigidbody>().velocity =
            transform.forward * bulletSpeed;
        //4
        bullet.GetComponent<bullet>().damage = weaponDam;
    }

    //FIRE THE MINI GUN
    private IEnumerator miniGunAttack()
    {
        //starts firing reptated at double speed
        InvokeRepeating("fireBullet", 0f, fireSpeed*2);
        //waits for 15 seconds
        yield return new WaitForSeconds(15);
        Debug.Log("minigun");
        
    }

}

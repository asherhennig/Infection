using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : Gun
{
    public int pelletsPerShot;
    public float spread;
    List<Quaternion> pellets;

    private void Awake()
    {
        pellets = new List<Quaternion>(pelletsPerShot);
        for (int i = 0; i < pelletsPerShot; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    void fireBullet()
    {
        int i = 0;
        foreach(Quaternion quat in pellets)
        {
            pellets[i] = Random.rotation;
            GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, pellets[i], spread);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.right * bulletSpeed);
        }
    }

    //void fireBullet()
    //{
    //    ///this was referanced from https://answers.unity.com/questions/1337081/shotgun-radndom-spread-with-true-bullets-c.html
    //    float totalSpread = spread / pelletsPerShot;
    //    for (int i = 0; i < pelletsPerShot; i++)
    //    {
    //        // calculate angle of bullet
    //        float spreadA = totalSpread * (i + 1);
    //      //  Debug.Log("totalSpread: " + totalSpread);
    //       // Debug.Log("spreadA: " + spreadA);
    //        float spreadB = spread / 2.0f;
    //      //  Debug.Log("spreadB: " + spreadB);
    //        float finSpread = spreadB - spreadA + totalSpread / 2;
    //      //  Debug.Log("finSpread: " + finSpread);
    //        float angle = transform.eulerAngles.y;

    //       // Debug.Log("angle: " + angle);

    //        //create bullet rot
    //        Quaternion rotation = Quaternion.Euler(new Vector3(90, 0, finSpread + angle));
    //      //  Debug.Log("angle: " + angle);

    //        // spawn bullet   
    //        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
    //        // set it to spawn at the firePos
    //        bullet.transform.position = firePosition.position;
    //        bullet.transform.rotation = rotation;
    //        // give it speed   
    //      //  Debug.Log("transform.forward:" + transform.up);
    //      //  Debug.Log("bulletSpeed: "+ bulletSpeed);
    //        Debug.Log("transform.forward * bulletSpeed: " + transform.up * bullet.transform.rotation.z * bulletSpeed);

    //        bullet.GetComponent<Rigidbody>().AddForce(new Vector3(rotation.x, rotation.y, rotation.z) * bulletSpeed, ForceMode.VelocityChange);//velocity =
    //        //    transform.forward * bulletSpeed;
    //        // give it damage
    //        bullet.GetComponent<bullet>().damage = weaponDam;
    //    }
    //}

}

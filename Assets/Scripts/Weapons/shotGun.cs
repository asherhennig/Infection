using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : Gun
{
    public int pelletsPerShot;
    public float spread;
    GameObject bullet;
    Quaternion rotation;

    void Update()
    {

    }

    private void Awake()
    {
        pellets = new List<Quaternion>(pelletsPerShot);
        for (int i = 0; i < pelletsPerShot; i++)
        {
            // calculate angle of bullet
            float spreadA = totalSpread * (i + 1);
            float spreadB = spread / 2.0f;
            float finSpread = spreadB - spreadA + totalSpread / 2;
            float angle = transform.eulerAngles.y;

            //create bullet rot
            rotation = Quaternion.Euler(new Vector3(90, 0, finSpread + angle));

            // spawn bullet   
            bullet = Instantiate(bulletPrefab) as GameObject;
            // set it to spawn at the firePos
            bullet.transform.position = firePosition.position;
            bullet.transform.rotation = rotation;
            // give it speed   
            bullet.GetComponent<Rigidbody>().AddForce(new Vector3(rotation.x, 0, rotation.z) * bulletSpeed, ForceMode.VelocityChange);//velocity =
            //    transform.forward * bulletSpeed;
            // give it damage
            bullet.GetComponent<bullet>().damage = weaponDam;
        }
    }

    void upgradePelletNum()
    {
        pelletsPerShot++;
        spread += 30;
    }
    void upgradeDam()
    {
        weaponDam++;
    }
}

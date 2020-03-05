using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : Gun
{
    public int pelletsPerShot;
    public float spread;
    GameObject bullet;
    Quaternion rotation;

    void fireBullet()
    {
        ///this was referanced from https://answers.unity.com/questions/1337081/shotgun-radndom-spread-with-true-bullets-c.html
        float totalSpread = spread / pelletsPerShot;
        for (int i = 0; i < pelletsPerShot; i++)
        {
            // calculate angle of bullet
            float spreadA = totalSpread * (i + 1);
            //  Debug.Log("totalSpread: " + totalSpread);
            // Debug.Log("spreadA: " + spreadA);
            float spreadB = spread / 2.0f;
            //  Debug.Log("spreadB: " + spreadB);
            float finSpread = spreadB - spreadA + totalSpread / 2;
            //  Debug.Log("finSpread: " + finSpread);
            float angle = transform.eulerAngles.y;

            // Debug.Log("angle: " + angle);

            //create bullet rot
            Quaternion rotation = Quaternion.Euler(new Vector3(90, 0, finSpread + angle));
            //  Debug.Log("angle: " + angle);
            // ray cast implemtation
            RaycastHit hit;

            //fires out raycast from fire pos
            if(Physics.Raycast(transform.position, Vector3.forward, out hit))
            {
                if(hit.collider.gameObject.tag == "Enemy")
                {

                }
            }
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



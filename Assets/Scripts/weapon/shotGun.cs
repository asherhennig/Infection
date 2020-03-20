using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : Gun
{
    public int pelletsPerShot;
    public float spread;
    GameObject bullet;
    Quaternion rotation;
    public float shotTrailDuration;

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
            if(Physics.Raycast(firePosition.position, Vector3.forward, out hit))
            {
                SpawnBullet(firePosition.transform.position, hit.transform.position, shotTrailDuration);
                //if it hits an enemy it does damage
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<enemyBase>().takeDamage(weaponDam);
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
    //this spawns the bullet to be used for the shotgun and attaches the shotgunbullet script to it for us as well as set the values in it
    public void SpawnBullet(Vector3 start, Vector3 end, float duration)
    {
        GameObject sgBullets = Instantiate(bullet) as GameObject;
        sgBullets.transform.position = start;
        sgBullets.transform.rotation = transform.rotation;
        shotgunBullet bulletComponent = sgBullets.AddComponent<shotgunBullet>();
        bulletComponent.SetValues(start, end, duration);
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject grenade;
    public float throwSpeed = 5.0f;
    public float exploRadius = 1.0f;
    public float fuseTime = 3.0f;
    public int  baseDamage = 5;

    private int expDam;
    // Start is called before the first frame update
    void Start()
    {
        //calls the grenade to explode after the fuse goes off
        Invoke("explode", fuseTime);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //explodes the granade on contact with an enemy
    void exploOnContact(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            explode();
        }
    }

    //explode returns an int to be used as the damage to be applied to others
    void explode()
    {
        //sets two seperate radius for near and far damage for explosion
        Collider[] Arround = Physics.OverlapSphere(transform.position, exploRadius);
        Collider[] ArroundNear = Physics.OverlapSphere(transform.position, Mathf.Abs(exploRadius / 2)); 

        //close radius does base damage
        foreach (Collider intoExp in ArroundNear)
        {
            if (intoExp.transform.tag == "Enemy")
            {
                expDam = baseDamage;
                intoExp.gameObject.GetComponent<FollowFood>().takeDamage(expDam);

            }
               
        }

        //farther radius that does less damage
        foreach (Collider inExp in Arround)
        {
            if (inExp.transform.tag == "Enemy")
            {
               //this should return an int a thrid the size of base damage
                expDam = Mathf.Abs(baseDamage / 3);
                inExp.gameObject.GetComponent<FollowFood>().takeDamage(expDam);
            }
        }

        //this will be used for when the particle system for the grenade is ready
        //grenade.GetComponent<ParticleSystem>().Play();
        //Destroy(gameObject, grenade.GetComponent<ParticleSystem>().duration);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragGrenade : MonoBehaviour
{

    public float exploRadius = 2.5f;
    public float fuseTime = 3.0f;
    public int baseDamage = 5;
    private int expDam;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("explode", fuseTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
    //explodes the grenade on contact with an enemy
    void exploOnContact(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && IsInvoking("explode"))
        {
            explode();
        }
    }
    //explode returns an int to be used as the damage to be applied to others
    void explode()
    {
        //sets two seperate radius for near and far damage for explosion
        Collider[] Arround = Physics.OverlapSphere(transform.position, exploRadius);
        Collider[] ArroundNear = Physics.OverlapSphere(transform.position, Mathf.Abs(exploRadius / 3));

        //close radius does base damage
        foreach (Collider intoExp in ArroundNear)
        {
            if (intoExp.transform.tag == "Enemy")
            {
                expDam = baseDamage;
                intoExp.gameObject.GetComponent<enemyBase>().takeDamage(expDam);

            }

        }

        //farther radius that does less damage
        foreach (Collider inExp in Arround)
        {
            if (inExp.transform.tag == "Enemy")
            {
                //this should return an int a thrid the size of base damage
                expDam = Mathf.Abs(baseDamage / 3);
                inExp.gameObject.GetComponent<enemyBase>().takeDamage(expDam);
            }
        }

        //this will be used for when the particle system for the grenade is ready
        //grenade.GetComponent<ParticleSystem>().Play();
        //Destroy(gameObject, grenade.GetComponent<ParticleSystem>().duration);
        //Destroy(gameObject); // this was causing the prefab to be destroyed while switching weapons which was causing problems
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lureGrenade : Grenade
{
    public float fuseTime = 10.0f;
    private AudioManager audioManager;
    private int mapRadius = 1000;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("explode", fuseTime);

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }
        Collider[] Arround = Physics.OverlapSphere(transform.position, mapRadius);

        foreach (Collider inExp in Arround)
        {
            if (inExp.transform.tag == "Enemy")
            {
                //this should set the ememies to follow the brain
                inExp.gameObject.GetComponent<enemyBase>().target2 = gameObject.transform;
            }
        }
    }

    void explode()
    {
        Destroy(gameObject);
    }
}

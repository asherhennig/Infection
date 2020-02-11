using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int pistolDam = 1;
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
         if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemyBase>().takeDamage(pistolDam);
            Destroy(gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class ShoulderShooter : MonoBehaviour
{
    public int damage;
    public Transform target;    //so ZAR knows where player is
    public GameObject bulletprefab;
    public Vector3 destination;
    public Vector3 stoppingDistance;
    public float shotRange;


    NavMeshAgent shooterZar;

    void Start()
    {
        shooterZar = this.GetComponent<NavMeshAgent>();
        shooterZar.stoppingDistance = shotRange;        //setting up a minimum line of sight

    }

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Enemy is damaging our hero!!" + damage);
            collision.gameObject.GetComponent<Player>().takeDamage();
            Destroy(gameObject);        //or Destroy.Player
            //then invoke the player death, load Wave screen and stats -> constants
      
        }

    }
    //function to return to main menu once player is destroyed
}



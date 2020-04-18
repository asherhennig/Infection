using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using Panda;

    /*ai BEHAVIOR class*/

public class ShoulderShooter : MonoBehaviour
{
    public int damage;
    public Transform target;             //so ZAR knows where player is
    public GameObject bulletprefab;
    public Vector3 destination;    //proximity before stopping to aim, fire
    public float shotRange;
    public enemyBase shooterZar;            //EX to protected Agent agent;

    void Start()
    {
        //shooterZar = this.GetComponent<NavMeshAgent>();
        //shooterZar.stoppingDistance - GameObject.position<target> = shotRange;        //setting up a minimum line of sight before the Shooting AI stops to aim & shoot

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
            collision.gameObject.GetComponent<Player>().takeDamage();
            Destroy(gameObject);        //or Destroy.Player
            //then invoke the player death, load Wave screen and stats -> constants
      
        }

    }
    //function to return to main menu once player is destroyed
}



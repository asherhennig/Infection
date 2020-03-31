using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;       //for AI mechs like navmesh


/*AGENT class - makes use of behaviors from SHOULDERSHOOTER to create intelligence choices*/

public class enemyBase : MonoBehaviour

{
    public float speed = 3.0f;
    public float accuracy = 0.09f;      //enemy accuracy to player before enemy stops moving
    public float Accel = 5.0f;            //for speed, accuracy and SLERP to pick up breifly when grenade is tossed (SpeedMult)
    public float orientation;
    public float rotation = 0.0f;
    public Transform target;             //target is hero/player
    public Vector3 stoppingDistance;
    public UnityEvent onDestroy;
    public int health = 5;
    public GameObject grenade;          //EnemyBase can recognize the grenade
    NavMeshAgent enemy;                 //AI navigate
    protected Lure lure;
  

    private int newHealth;
    public GameObject currencyprefab;
   

    void Start()
    {
        newHealth = health;
        enemy = this.GetComponent<NavMeshAgent>();

 
    }


    // update is called every frame
    void Update()
    {
        //updates health to the newhealth value
        health = newHealth;

        //if the health of a enemy is equal or less than 0 it dies
        if (health <= 0)
        {
            Die();
            GameObject currency = Instantiate(currencyprefab) as GameObject;
            currencyprefab.transform.position = this.transform.position;
        }

    }

    // LateUpdate for physics
    void LateUpdate()
    {
        if (target != null)
        {
            this.transform.LookAt(target.position);                               //Enemy faces player
            Vector3 direction = target.position - this.transform.position;        //enemy direction: where its going MINUS where it is
            Debug.DrawRay(this.transform.position, direction, Color.green);     //for the intended path

            if (direction.magnitude > accuracy)                                 //If direction length is larger than enemy dis from player

                this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);       //..Then move towards the player in 
        }                                                                                              // global space


    }

    public void Die()
    {
        Destroy(gameObject);
        onDestroy.Invoke();
        onDestroy.RemoveAllListeners();
    }
    
    //this has calculates the players new health post damage
    public void takeDamage(int damTaken)
    {
        newHealth = health - damTaken;
        Debug.Log("Pistol damage after shot is:" + damTaken);
        Debug.Log("health is:" + health);
        Debug.Log("enemy took damage " + newHealth);
    }
}

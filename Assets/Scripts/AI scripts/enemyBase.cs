using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemyBase : MonoBehaviour

{
    public float speed = 3.0f;
    public float accuracy = 0.09f;      //enemy accuracy to player before enemy stops moving
    public Transform target;             //goal is hero/player
    public UnityEvent onDestroy;
    public int health = 5;
    private int newHealth;
    public GameObject currencyprefab;
    int chance;
    public GameObject currencyprefab2;

    void Start()
    {
        newHealth = health;
    }

    // update is called every frame
    void Update()
    {
        //updates health to the newhealth value
        health = newHealth;

        //if the health of a enemy is equal or less than 0 it dies
        if (health <= 0)
        {
            // increase the number of kills the player has in the player script
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().numKills++;
            Die();
            //create a random chance for drop 
            chance = Random.Range(0, 10);
            //if it is the low chance of 5 gum loot drop is that
            if (chance >= 8)
            {
                Instantiate(currencyprefab2, this.transform.position, Quaternion.identity);
            }
            //other wise it is normal drop
            else
            {
                Instantiate(currencyprefab, this.transform.position, Quaternion.identity);
            }
           
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

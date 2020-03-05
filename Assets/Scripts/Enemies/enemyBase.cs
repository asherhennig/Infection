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
    public float diffMod;
    
    void Awake()
    {
        setHealth();
    }

    // update is called every frame
    void Update()
    {
        //if the health of a enemy is equal or lesss than 0 it dies
        if (health <= 0)
        {
            Die();
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
        health -= damTaken;
        Debug.Log("damage after shot is:" + damTaken);
        Debug.Log("health is:" + health);
    }

    public void setDiff(float DiffMod)
    {
        diffMod = DiffMod;
    }

    public void setHealth()
    {
        health = (int)(health * diffMod);
    }
}

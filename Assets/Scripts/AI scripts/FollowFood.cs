using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowFood : MonoBehaviour

{
    public float speed = 3.0f;
    public float accuracy = 0.09f;      //enemy accuracy to player before enemy stops moving
    public Transform target;             //goal is hero/player
    public UnityEvent onDestroy;
    public int health = 5;
    private int newHealth = 0;

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

        health -= newHealth;
        if (health == 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        onDestroy.Invoke();
        onDestroy.RemoveAllListeners();
    }
    void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("in collision");
        //check if enemy collided with bullet
        //if (coll.gameObject.tag == "Bullet")
        //{
        //    takeDamage(1);

        //}
       
    }

    public void takeDamage(int damTaken)
    {
        newHealth = damTaken;
        Debug.Log("enemy took damage " + health);
    }
}

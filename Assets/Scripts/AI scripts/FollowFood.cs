using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowFood : MonoBehaviour

{
    public float speed = 1.0f;
    public float accuracy = 0.09f;      //enemy accuracy to player before enemy stops moving
    public Transform target;             //goal is hero/player
    public UnityEvent onDestroy;

    // LateUpdate for physics
    void LateUpdate()
    {
        this.transform.LookAt(target.position);                               //Enemy faces player
        Vector3 direction = target.position - this.transform.position;        //enemy direction: where its going MINUS where it is
        Debug.DrawRay(this.transform.position, direction, Color.green);     //for the intended path

        if (direction.magnitude > accuracy)                                 //If direction length is larger than enemy dis from player

            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);       //..Then move towards the player in 
                                                                                                        // global space
    }

    public void Die()
    {
        onDestroy.Invoke();
        onDestroy.RemoveAllListeners();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFood : MonoBehaviour

{
    public float speed = 1.0f;
    public float accuracy = 0.09f;      //enemy accuracy to player before enemy stops moving
    public Transform goal;             //goal is hero/player


    // LateUpdate for physics
    void LateUpdate()
    {
        this.transform.LookAt(goal.position);                               //Enemy faces player
        Vector3 direction = goal.position - this.transform.position;        //enemy direction: where its going MINUS where it is
        Debug.DrawRay(this.transform.position, direction, Color.green);     //for the intended path

        if (direction.magnitude > accuracy)                                 //If direction length is larger than enemy dis from player

            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);       //..Then move towards the goal in 
                                                                                                        // global space
    }
}

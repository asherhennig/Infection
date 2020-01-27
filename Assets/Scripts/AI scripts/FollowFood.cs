using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowFood : MonoBehaviour

{
    public float speed = 1.0f;
    public float accuracy = 0.09f;
    public Transform goal;      //goal is hero/player


    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.LookAt(goal.position);
        Vector3 direction = goal.position - this.transform.position;        //calculating player's location minus the AI's positions
        Debug.DrawRay(this.transform.position, direction, Color.green);

        if (direction.magnitude > accuracy)

            this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);       //as opposed to local space.
    }
}

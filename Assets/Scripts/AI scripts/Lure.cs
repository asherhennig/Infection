using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*Lure will replace the Steering CLASS*/

public class Lure : MonoBehaviour
{
    //effective accelerated movement towards new goal

    public float angular;
    public Vector3 linear;
    public GameObject grenade;
    GameObject[] enemies;
    public float detectionRadius;
    public float chase;

    public virtual void Distraction() 
    {
        /*NPC Movement and Rotation*/
        angular = 20.0f;
        linear = new Vector3();
    }

    void GrenadeLure(Vector3 position)  //pass in pos of grenade when its dropped
    {
        //brainGrenade position & enemy position is within the level (set detection radius)
        if (Vector3.Distance(position, this.transform.position) < detectionRadius)
        {
            //Enemy reacts and chases brain
            Vector3 chaseDirection = (this.transform.position).normalized;
            //position of the enemy minus the position of the GRENADE
            Vector3 newTarget = this.transform.position + chaseDirection * chase;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
        }



    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    /*AI mechanics/physics*/
    private void LateUpdate()
    {
        
    }
}

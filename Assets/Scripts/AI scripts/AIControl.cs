//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class AIControl : MonoBehaviour
//{

//    GameObject playerLocation;              //Player's location on the map
//    NavMeshAgent agent;                     //referring to navmeshes on our avatars
//    Animator anim;                          //once we need to implement animations with walking/chasing
//    float speedMult;
//    float detectionRadius = 20.0f;          // the perimeter taken for enemy to notice/follow player
//    float biteRadius = 10.0f;               // enemy detects a "hit box" to make attempts to bite

//    void ChaseFood()
//    {
//        speedMult = Random.Range(0.5f, 1.5f);
//        agent.speed = 2 * speedMult;
//        agent.angularSpeed = 120;
//        // anim.SetFloat("speedMult", speedMult);
//        // anim.SetTrigger("isWalking");
//        agent.ResetPath();

//    }



//    void Start()
//    {
//        playerLocation = GameObject.FindGameObjectWithTag("Player");  //all "goal" locations in the scene
//        agent = this.GetComponent<NavMeshAgent>();
//        agent.SetDestination(playerLocation[Random.Range(0, playerLocation.Length)].transform.position);   //array of random goals, agent will choose one.

//        anim = this.GetComponent<Animator>();
//        //before walking animation starts:
//        anim.SetFloat("wOffset", Random.Range(0, 1));
//        ResetAgent();
//        //anim.SetTrigger("IsWalking");     // "triggering" the walking animation
//        //anim.SetTrigger("IsBiting");      //Bite animation triggered

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (agent.remainingDistance < 1)   //remaing referencing distnace between avatar and goal
//        {
//            ResetAgent();
//            agent.SetDestination(playerLocation[Random.Range(0, playerLocation.Length)].transform.position);
//        }
//    }

//    //function to enable the zombie chasing the player 
//    public void DetectFood(Vector3 position)
//    {
//        if (Vector3.Distance(position, this.transform.position) < detectionRadius)      //
//        {
//            Vector3 detectionRadius = (this.transform.position - position).normalized;
//            Vector3 newgoal = this.transform.position - // detect the players 

//            NavMeshPath path = new NavMeshPath();
//            agent.CalculatePath(newgoal, path);
            
//            {
//                agent.SetDestination();
//                // anim.SetTrigger("isBiting);
//                agent.speed = //increase speed to chase within set radius
//                agent.angularSpeed = 500.0f;
//            }

//        }
//    }
//}
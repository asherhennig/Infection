using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Movement : MonoBehaviour
{

    [SerializeField]
    Transform Target;   //player location
    [SerializeField]
    float chaseRange = 5.0f;

    NavMeshAgent ShooterZar;
    float distanceToTarget = Mathf.Infinity;    // if Initialized at 0, Enemy AI will head straight for player                                            
    bool seeTarget = false;                     //Initialized at false so that it doesn't give chase immediately - "Complete C# UNITY Developer" https://www.gamedev.tv/p/complete-unity-developer-3d
    //seeTarget ==> isProvoked

    // Start is called before the first frame update
    void Start()
    {
        ShooterZar = GetComponent<NavMeshAgent>();
    }


    //private void StrikeTarget()
    //{
    //    while (ShooterZar.Search)
    //    if (ShooterZar.stoppingDistance <= distanceToTarget)
    //    {
    //        PursueTarget();  //is ChaseTarget()
    //    }
    //    if (ShooterZar.stoppingDistance <= distanceToTarget)
    //    {
    //        ZapTarget();    //is AttackTarget()
    //    }
    //}

    private void PursueTarget()
    {
        // new enemySpeed = enemySpeed * 1.5f;
         ShooterZar.SetDestination(Target.position);
        //if (distanceToTarget > 0.5 * )
        //{
        //    ShooterZar();
        //}
    }
    
    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(Target.position, transform.position); //mesasure dist between (a, b)  ==> (AI, target)
        
        if (seeTarget)
        {
            //StrikeTarget();
            //is EngageTarget();
        }
        else if (chaseRange >= distanceToTarget)
        {
            seeTarget = true;
 
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;



public class Shoulders : MonoBehaviour
{

    [SerializeField]
    Transform Target;   //player location
    [SerializeField]
    public float chaseRange = 10.0f;
    NavMeshAgent ShooterZar;

    float distanceToTarget = Mathf.Infinity;    // if Initialized at 0, Enemy AI will head straight for player                                            
    bool seeTarget = false;                     //Initialized at false so that it doesn't give chase immediately - "Complete C# UNITY Developer" https://www.gamedev.tv/p/complete-unity-developer-3d
    //seeTarget ==> isProvoked
    Transform lazerSpawn;
    GameObject lazerPrefab;

    void Start()
    {
        ShooterZar = GetComponent<NavMeshAgent>();

    }

    //Panda Lib
    [Task]
    public void Search()          //.. for and pick a Random goal
    {
        Vector3 destination = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));
        ShooterZar.SetDestination(destination);
        //wont continue executing the sequence without a succession.
        Task.current.Succeed();
    }

    [Task]
    public void Idle() // Move to Destination
    {
        if (ShooterZar.remainingDistance <= ShooterZar.stoppingDistance && !ShooterZar.pathPending)
        {
            Task.current.Succeed();
        }
    }

    [Task]
    private void ChooseDirection(float x, float z) //is frozen on Y axis
    {
        Vector3 dir = new Vector3(x, 0, z);
        ShooterZar.SetDestination(dir);
        Task.current.Succeed();

    }

    [Task]
    public bool ZapTarget() //The enemy's offensive method "Fire"
    {
        GameObject lazer = GameObject.Instantiate(lazerPrefab, lazerSpawn.transform.position, lazerSpawn.transform.rotation);
        lazer.GetComponent<Rigidbody>().AddForce(lazer.transform.forward);
        return true;
        Debug.Log("ZAP!");

    }

    [Task]






    void Update()
    {
        // every frame, it mesasures dist between (va, vb)  ==> (AI, target)
        distanceToTarget = Vector3.Distance(Target.position, transform.position);

        if (seeTarget)
        {
            //StrikeTarget();
            //is EngageTarget();
        }
        else if (chaseRange >= distanceToTarget)
        {
            seeTarget = true;
            ShooterZar.SetDestination(Target.position);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet") // 
        {
            //ehealth--;
        }
    }



    //private void StrikeTarget() // isTargetPlayer**  ==>Aim at Target
    //{
    //    //while (ShooterZar.Search)
    //    if (ShooterZar.stoppingDistance <= distanceToTarget)
    //    {
    //        IdlePursue();  //is ChaseTarget()
    //    }
    //    if (ShooterZar.stoppingDistance <= distanceToTarget)
    //    {
    //        ZapTarget();    //is AttackTarget() or Fire() ==> AI shoots
    //    }
    //}

    private void IdlePursue()   //Idle Walk
    {
        // new enemySpeed = enemySpeed * 1.5f;
        //ShooterZar.SetDestination(Target.position);
        //if (distanceToTarget > 0.5 * )
        //{
        //    ShooterZar();
        //}
    }

}

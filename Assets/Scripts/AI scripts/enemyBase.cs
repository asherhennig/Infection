using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;       //for AI mechs like navmesh


/*AGENT class - makes use of behaviors from SHOULDERSHOOTER to create intelligence choices*/

public class enemyBase : MonoBehaviour

{
    public float speed = 3.0f;
    public float accuracy = 0.09f;      //enemy accuracy to player before enemy stops moving
    public float Accel = 5.0f;            //for speed, accuracy and SLERP to pick up breifly when grenade is tossed (SpeedMult)
    public float orientation;
    public float rotation = 0.0f;
    public Transform target;             //target is hero/player
    public Vector3 stoppingDistance;
    public GameObject grenade;          //EnemyBase can recognize the grenade
    NavMeshAgent enemy;                 //AI navigate
    protected Lure lure;
    public GameObject currencyprefab;
    public UnityEvent onDestroy;
    public int ehealth = 5;
    private int newehealth;
    public Transform target2;
    int chance;
    public GameObject currencyprefab2;
    public GameObject hitPrefab;
    public GameObject enemyDeathPrefab;
    GameObject clone;
    public Animator head;


    private AudioManager audioManager;

    //this is used to modify the enemies stats later on
    public float diffMod;
    

    void Start()
    {
        head = GetComponent<Animator>();

        //call to init the enemies stats
        setEnemyStats();

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }

    }


    // update is called every frame
    void Update()
    {
        //if the ehealth of a enemy is equal or lesss than 0 it dies
        if (ehealth <= 0)
        {
            Die();

            GameObject currency = Instantiate(currencyprefab) as GameObject;
            currencyprefab.transform.position = this.transform.position;

            clone = Instantiate(enemyDeathPrefab, this.transform.position, Quaternion.identity);
            Destroy(clone, 2f);

            // Play sound
            audioManager.PlaySound("RobotDeathSound");
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
        
        if (target != null)
        {
            head.SetBool("IsMoving", true);
        }
        else
        {
            head.SetBool("IsMoving", false);
        }
    }

    // LateUpdate for physics
    void LateUpdate()
    {
            if (target2 != null)
            {
                this.transform.LookAt(target2.position);                               //Enemy faces player
                Vector3 direction = target2.position - this.transform.position;        //enemy direction: where its going MINUS where it is
                Debug.DrawRay(this.transform.position, direction, Color.green);     //for the intended path

                if (direction.magnitude > accuracy)                                 //If direction length is larger than enemy dis from player
                {
                    this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);       //..Then move towards the player in
                                                                                                                //in global space
                    if (Time.timeScale > 0)
                    {
                        audioManager.PlaySound("RobotSound");
                    }
                    head.SetBool("InRange", false);
                }
                else
                {
                    head.SetBool("InRange", true);
                }
            }
            else
            {
                this.transform.LookAt(target.position);                               //Enemy faces player
                Vector3 direction = target.position - this.transform.position;        //enemy direction: where its going MINUS where it is
                Debug.DrawRay(this.transform.position, direction, Color.green);     //for the intended path

                if (direction.magnitude > accuracy)                                 //If direction length is larger than enemy dis from player
                {
                    this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);       //..Then move towards the player in
                                                                                                                //in global space
                    if (Time.timeScale > 0)
                    {
                        audioManager.PlaySound("RobotSound");
                    }
                    head.SetBool("InRange", false);
                }
                else
                {
                    head.SetBool("InRange", true);
                }
            }
    }

    public void Die()
    {
        Destroy(gameObject);
        onDestroy.Invoke();
        onDestroy.RemoveAllListeners();
    }

    //this has calculates the players new ehealth post damage
    public void takeDamage(int damTaken)
    {
        ehealth -= damTaken;
        clone = Instantiate(hitPrefab, this.transform.position, Quaternion.identity);
        Destroy(clone, 2f);
    }

    public void setDiff(float DiffMod)
    {
        diffMod = DiffMod;
    }

      //this sets the enemies health and speed
    public void setEnemyStats()
    {
        //health has to be recast as a int because its a float and int multiplied which is a float and health is only an int
        ehealth = (int)(ehealth * diffMod);
        //speed luckily can stay as a float
        speed = speed * diffMod;
    }
}

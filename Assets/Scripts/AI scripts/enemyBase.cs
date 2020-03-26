using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class enemyBase : MonoBehaviour

{
    public float speed = 3.0f;
    public float accuracy = 0.5f;      //enemy accuracy to player before enemy stops moving
    public Transform target;             //goal is hero/player
    public UnityEvent onDestroy;
    public int health = 5;

    private int newHealth;
    public GameObject currencyprefab;
    int chance;
    public GameObject currencyprefab2;
    public GameObject hitPrefab;
    public GameObject enemyDeathPrefab;

    private Animator Head;



    private AudioManager audioManager;


    public float diffMod;
    

    void Start()
    {
        setHealth();

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }
    }

    // update is called every frame
    void Update()
    {
        //if the health of a enemy is equal or lesss than 0 it dies
        if (health <= 0)
        {
            //Die();
            Instantiate(enemyDeathPrefab, this.transform.position, Quaternion.identity);
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
    }

    // LateUpdate for physics
    void LateUpdate()
    {
        if (target != null)
        {
            this.transform.LookAt(target.position);                               //Enemy faces player
            Vector3 direction = target.position- this.transform.position;        //enemy direction: where its going MINUS where it is
            Debug.DrawRay(this.transform.position, direction, Color.green);     //for the intended path

            if (direction.magnitude > accuracy)                                 //If direction length is larger than enemy dis from player

                this.transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);       //..Then move towards the player in
                                                                                                            //in global space
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Head.SetBool("IsMoving", true);
        }
        else
        {
            Head.SetBool("IsMoving", false);
        }
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
        Instantiate(hitPrefab, this.transform.position, Quaternion.identity);

        
        Debug.Log("Pistol damage after shot is:" + damTaken);
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

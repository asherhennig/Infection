﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //initialized current health and max health and other public vars
    public int curHealth;
    public int maxHealth;
    public int currency = 0;
    public float speed = 10;
    public float rotSpeed = 1.0f;
    public float timeBetweenHits = 0;
    public LayerMask layerMask;
    //so get componet can access the minigun
   private Gun minigun;
    public bool isactive = false;

    //private init
    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private bool isDead = false;
    private bool isHit = false;
    private float timeSinceHit = 0;
    private GunEquipper gunEquipper;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gunEquipper = GetComponent<GunEquipper>();
        minigun = GetComponent<Gun>();
    }

    //added takeDamage function
    public void takeDamage()
    {
        int healthDamage = 1;
        curHealth -= healthDamage;
        Debug.Log("you've been hurt, health is: " + curHealth + " out of: " + maxHealth);
        if(curHealth <= 0)
        {
            isDead = true;
            
        }
    }

    //added max health up function
    public void maxUp()
    {
        curHealth = maxHealth;
        Debug.Log("health is: " + curHealth);
    }

    // added health pick up and it caps at max health
    public void pickUpHealth()
    {
        //pick up only gives one health
        curHealth += 1;
        if (curHealth > maxHealth)
        {
            //wont go over max health
            curHealth = maxHealth;
            Debug.Log("You're at max health!");
        }
        else
        {
            Debug.Log("Health Up! " + curHealth);
        }
    }

    public void pickUp1Curr()
    {
        currency++;
    }

    public void picUp5Curr()
    {
        currency = currency + 5;
    }

   
   
    public void pickUpMiniGun()
    {
        isactive = true;
        gunEquipper.activeMiniGun();
        while(isactive)
        {
            
            minigun.fireBullet();

        }

        //deactivate the mini gun and reactivate pistol
        gameObject.GetComponent<GunEquipper>().deactiveMiniGun();
    }
    //checks which pickup we got to know its effect
    public void PickUpItem(int pickupItem)
    {
        
        switch (pickupItem)
        {
            //uses constant class to define the variables and set it to case 1, 2 .....ect.

            //heals 1 health point
            case Constants.healthPickUp1:
                pickUpHealth();
                break;
            
            //heals full
            case Constants.HealthPickUpFull:
                maxUp();
                break;

            //add one bubble gum to inventory
            case Constants.bubbleGum1:
                pickUp1Curr();
                break;

            //add 5 bubble gum to inventory
            case Constants.bubbleGum5:
                picUp5Curr();
                break;

            //pick up the mini gun and start shooting
            case Constants.miniGunPickUp:
                pickUpMiniGun();
                break;

            default:
                //in case of bad pick up
                Debug.LogError("Bad pickup type passed" + pickupItem);
                break;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        //moves the player using the character controller
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
                                            0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * speed);
        //gives the player some I frames after being hit, we can adjust how long
        if(isHit)
        {
            timeSinceHit += Time.deltaTime;
            if(timeSinceHit>timeBetweenHits)
            {
                isHit = false;
                timeSinceHit = 0;
            }
        }
        //if youre dead you die... lol
        if(isDead)
        {
            Die();
        }
    }
    void FixedUpdate()
    {
        //this makes the palyer look at the cursor when its on screen
        //creating hit and ray variables
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //fires ray from camera and returns hit
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {

            }
        }

        //sets where we want to turn to
        Vector3 targetPositon = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //gives smooth roation
        Quaternion rotation = Quaternion.LookRotation(targetPositon - transform.position);
        //turns to cursor 
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
    }
    //detects if we get hit by an enemy
    private void OnTriggerEnter(Collider other)
    {
        //checks if its and enemy by seeing if it has the FollowFood script
        enemyBase enemy = other.gameObject.GetComponent<enemyBase>();
        if (enemy != null)
        {
            //checks if were not already hit
            if(!isHit)
            {
                takeDamage();
            }
        }
    }

    //this is where eventually well do everything that happens when the player dies here
    public void Die()
    {
        Debug.Log("GameOver");
        Destroy(gameObject);
    }
}

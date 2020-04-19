using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //initialized current health and max health and other public vars
    public int curHealth;
    public int maxHealth;
    public float speed = 10;
    public float rotSpeed = 1.0f;
    public float timeBetweenHits = 0;
    public LayerMask layerMask;
    public GameObject miniGun;
    public GameObject PlayerHitPrefab;

    enemyBase enemy;

    public Animator heroAnim;
    Animator head;


    //so get componet can access the minigun
    public bool isDead = false;
    public HealthBar healthBar;

    //private init
    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private bool isHit = false;
    private float timeSinceHit = 0;
    private GunEquipper gunEquipper;
    private AudioManager audioManager;
    public GameObject pistolButton;
    public GameObject shotgunButton;

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject grenade;
    public GameObject lureGrenade;

    public static bool minigunFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 5;
        characterController = GetComponent<CharacterController>();
        gunEquipper = GetComponent<GunEquipper>();
        
        healthBar.setMaxHealth(maxHealth);

        heroAnim = GetComponent<Animator>();

        audioManager = AudioManager.instance;
    }

    //added takeDamage function
    public void takeDamage()
    {
        int healthDamage = 1;
        curHealth -= healthDamage;
        healthBar.setHealth(curHealth);

        if(curHealth <= 0)
        {
            isDead = true;
        }
    }

    //added max health up function
    public void maxUp()
    {
        curHealth = maxHealth;
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
        }
    }

    public void pickUp1Curr()
    {
        GameManager.totalBubblegum += 10;
    }

    public void picUp5Curr()
    {
        GameManager.totalBubblegum += 50;
    }

    public void pickUpMiniGun()
    {
        StartCoroutine("fireMiniGun");
    }
    //checks which pickup we got to know its effect
    public void PickUpItem(int pickupItem)
    {
        switch (pickupItem)
        {
            //uses constant class to define the variables and set it to case 1, 2 .....ect.

            //heals 1 health point
            case Constants.healthPickUp1:
            {
                pickUpHealth();
                break;
            }
            
            //heals full
            case Constants.HealthPickUpFull:
            {
                maxUp();
                break;
            }

            //add one bubble gum to inventory
            case Constants.bubbleGum1:
            { 
                pickUp1Curr();
                break;
            }

            //add 5 bubble gum to inventory
            case Constants.bubbleGum5:
            { 
                picUp5Curr();
                break;
            }

            //pick up the mini gun and start shooting
            case Constants.miniGunPickUp:
            {
                pickUpMiniGun();
                break;
            }

            default:
                //in case of bad pick up
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

        if (moveDirection == Vector3.zero)
        {
            heroAnim.SetBool("IsMoving", false);        //Set Animator to not moving if character vector = 0
        }
        else
        {
            heroAnim.SetBool("IsMoving", true);
        }

        //gives the player some I frames after being hit, we can adjust how long
        if (isHit)
        {
            timeSinceHit += Time.deltaTime;
            if(timeSinceHit>timeBetweenHits)
            {
                isHit = false;
                timeSinceHit = 0;
            }
        }


        //if youre dead you die... lol
        if (isDead)
        {
            Die();
        }
    }
    void FixedUpdate()
    {
        ////Player direction controls
        //Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
        //                                    0, Input.GetAxis("Vertical"));




        //else
        //{
        //    head.AddForce(transform.right * 150, ForceMode.Acceleration);            //head bobble functionality

        //    bodyAnimator.SetBool("IsMoving", true);         //Set Animator to moving if character vector != 0
        //}


        //this makes the palyer look at the cursor when its on screen
        //creating hit and ray variables
        RaycastHit hit;

        Vector3 MouseAxis = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //sets where we want to turn to
        Vector3 targetPositon = new Vector3(transform.position.x + MouseAxis.x * 10, transform.position.y, transform.position.z + MouseAxis.y * 10);
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
                Instantiate(PlayerHitPrefab, this.transform.position, Quaternion.identity);
                takeDamage();
            }
        }
    }

    private IEnumerator fireMiniGun()
    {
        //200 is the num of bulets fired when powered up
        for (int i = -0; i < 150; i++)   
        {
            minigunFiring = true;
            usingMinigun();

            //gets the fire bulet function from the mini gun in gun script and calls it
            miniGun.GetComponent<Gun>().fire();

            audioManager.PlaySound("MinigunSound");

            //call againg in half a second
            yield return new WaitForSeconds(1/2);
        }

        miniGun.GetComponent<Gun>().stopFiring();

        minigunFiring = false;
        stopMinigun();

        //deactivate the mini gun and reactivate pistol
        gunEquipper.deactiveMiniGun();
    }

    //this is where eventually well do everything that happens when the player dies here
    public void Die()
    {
        Destroy(gameObject);
    }

    void usingMinigun()
    {
        miniGun.SetActive(true);
        pistol.SetActive(false);
        shotgun.SetActive(false);
        grenade.SetActive(false);
        lureGrenade.SetActive(false);

        pistolButton.SetActive(false);
        shotgunButton.SetActive(false);

        heroAnim.SetBool("SetActive_shotgun", true);
        heroAnim.SetBool("SetActive_pistol", false);
        heroAnim.SetBool("SetActive_throw", false);
    }

    void stopMinigun()
    {
        miniGun.SetActive(false);
        pistol.SetActive(true);
        pistolButton.SetActive(true);

        heroAnim.SetBool("SetActive_shotgun", false);
        heroAnim.SetBool("SetActive_pistol", true);
    }
}

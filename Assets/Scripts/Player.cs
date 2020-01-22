using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //initialized current health and max health
    public int curHealth;
    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //added takeDamage function
    public void takeDamage(int amount)
    {
        int healthDamage = amount;
        curHealth -= healthDamage;
        if(curHealth <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    //added max health up function
    public void maxUp()
    {
        maxHealth += 1;
        curHealth += 1;
        Debug.Log("Max Health Up: " + maxHealth + " Current Health: " + curHealth);
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

    public void PickUpItem(int pickupItem)
    {
        switch (pickupItem)
        {
            case Constants.healthPickUp:
                pickUpHealth();
                break;
            case Constants.granadePickUp:
                break;
            case Constants.maxUpPickUp:
                maxUp();
                break;
            default:
                Debug.LogError("Bad pickup type passed" + pickupItem);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

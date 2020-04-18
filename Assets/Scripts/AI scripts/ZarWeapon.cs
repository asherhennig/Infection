using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ZarWeapon : MonoBehaviour
{
    [SerializeField]
    GameObject ZarShottaken;
    [SerializeField]
    float ZarRayRange = 100.0f;

 
    void Update()
    {
       // if (PlayerInput.GetButtonDown) //player shoots/provokes SHOULDERS
           // ZarShoot();
    }

    void ZarShoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(ZarShottaken.transform.position, ZarShottaken.transform.forward, out hit))
        {
            //add hit effect
            //call Target Health
            
        }
    }


    /* uncomment when scripts merge*/
    //private void OnTriggerEnter(Collider collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<Player>().takeDamage();
    //        Destroy(gameObject);        //or Destroy.Player
    //                                    //then invoke the player death, load Wave screen and stats -> constants

    //    }

    //}
    //function to return to main menu once player is destroyed
}

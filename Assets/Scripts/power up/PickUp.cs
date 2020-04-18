using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private AudioManager audioManager;
    public int type;

    void Start()
    {
        audioManager = AudioManager.instance;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Player>() != null
            && collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().PickUpItem(type);
            Destroy(gameObject);
            // Play sound
            audioManager.PlaySound("PickupSound");
        }
    }
    
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Player>() != null
            && collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().PickUpItem(type);
            Destroy(gameObject);
            Debug.Log("destroyed");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

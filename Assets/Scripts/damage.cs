using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("in collision");
        //check if enemy collided with bullet
        if(coll.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGunTimer : MonoBehaviour
{
    float timeleft = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Player>().isactive==true)
        {
            timeleft -= Time.deltaTime;
            if (timeleft == 0)
            {
                gameObject.GetComponent<Player>().isactive = false;
            }
        }
        
    }
}

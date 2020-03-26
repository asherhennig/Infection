using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyDie : MonoBehaviour
{
    public GameObject enemyDeathPrefab;
    
    void Start()
    {

    }

    
    void Update()
    {
        if (GetComponent<enemyBase>().health <= 0)
        {
            
        }

    }
}

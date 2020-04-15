using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdManager : MonoBehaviour
{
    public GameObject enemyPrefab;                       //HEad, shoulders, kneesNtoes
    public int maxHerd = 20;                           
    public GameObject[] allEnemy;                        // array of total instantiated ZAR
    public Vector3 barrierLimits = new Vector3(8, 8, 8);    //radius value for the fdistance range - increase vector for a bigger range
    public Vector3 targetPos;                              //vec3 to modify x, y and z coordinates.

    //Inspector settings for manual changes
    [Header("Herd Settings")]      
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDist;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;


     void Start()
    {
        allEnemy = new GameObject[maxHerd];      //create fish and an array to store
        for (int i = 0; i < maxHerd; i++)       //loop is creating a position to place the enemy
                                                  //That position is based on the flock manager + a random vector3 value
        {
            //ZARS traveling location is based off the position of the herd manager PLUS a rnadom value within the trveling barrier limits
            Vector3 location = this.transform.position + new Vector3(Random.Range(-barrierLimits.x, barrierLimits.x),     
                                                                Random.Range(-barrierLimits.y, barrierLimits.y),
                                                                Random.Range(-barrierLimits.z, barrierLimits.z));
            allEnemy[i] = (GameObject)Instantiate(enemyPrefab, location, Quaternion.identity);
            allEnemy[i].GetComponent<Herd>().herdsManager = this;           //now linking to Herd.cs for effective movement around the middle pod
        }

        targetPos = this.transform.position;
    }

    private void Update()
    {
        if (Random.Range(0, 100) < 10)
                        
            targetPos = this.transform.position + new Vector3(Random.Range(-barrierLimits.x, barrierLimits.x),
                                                                    Random.Range(-barrierLimits.y, barrierLimits.y),
                                                                    Random.Range(-barrierLimits.z, barrierLimits.z));
    }
}
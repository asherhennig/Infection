using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //gameobjects that will be needed in the script
    public GameObject player;
    public GameObject[] spawnPoints;
    public GameObject enemy;
    public GameObject pickUpPrefab;

    //public vars so we can modify them as we need
    public int maxEnemiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float pickUpMaxSpawnTime = 10.0f;

    //private data for keeping track of enemies on screen and time
    // between spawns of enemies and pick-ups
    private int enemiesOnScreen = 0;
    //these are for the spawning of pickups
    private bool spawnedPickUp = false;
    private float actualPickUpTime = 0;
    private float currentPickUpTime = 0;
    //these are for enemy spawns
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        actualPickUpTime = Random.Range(pickUpMaxSpawnTime - 3.0f, pickUpMaxSpawnTime);
        actualPickUpTime = Mathf.Abs(actualPickUpTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        //updating pick up spawn time
        currentPickUpTime += Time.deltaTime;

        //checks if the current spawntime is more than the upgrade spawn time and that one isnt spawned
        if(currentPickUpTime > actualPickUpTime && !spawnedPickUp)
        {
            //generates a random number based on the number of spawnpoints we have and
            //assigns one to be the spawn, finally it spawns a pickup
            int randnum = Random.Range(0, spawnPoints.Length - 1);
            GameObject spawnLocation = spawnPoints[randnum];
            GameObject pickUp = Instantiate(pickUpPrefab) as GameObject;
            pickUp.transform.position = spawnLocation.transform.position;
            spawnedPickUp = true;
        }

        ////enemy spawn timer tick
        //currentSpawnTime += Time.deltaTime;
        ////checks if its time to spawn
        //if(currentSpawnTime > generatedSpawnTime)
        //{
        //    //if it is sets spawn timer to 0 and new time to spawn
        //    currentSpawnTime = 0;
        //    generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);

        //}
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    //gameobjects that will be needed in the script
    public GameObject player;
    public GameObject[] itemSpawnPoints;
    public GameObject[] enemySpawnPoints;
    public GameObject enemy;
    public GameObject pickUpPrefab;

    //public vars so we can modify them as we need
    public int maxEnemiesOnScreen;
    public int enemiesPerSpawn;
    public int restTimer;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float pickUpMaxSpawnTime = 10.0f;
    public int wave=0;

    //private data for keeping track of enemies on screen and time
    // between spawns of enemies and pick-ups
    private int enemiesOnScreen = 0;
    //these are for the spawning of pickups
    private bool spawnedPickUp = false;
    private float actualPickUpTime = 0;
    private float currentPickUpTime = 0;
    //these are for enemy spawns
    public int MaxPerWave = 5;
    private int curSpawnedWave = 0;
    GameObject pickUp;
    //this lets us know if a wave is active
    public bool activeWave = true;

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        actualPickUpTime = Random.Range(pickUpMaxSpawnTime - 3.0f, pickUpMaxSpawnTime);
        actualPickUpTime = Mathf.Abs(actualPickUpTime);
        Time.timeScale = 1;
        restTimer = 0;
        StartCoroutine("updatedRestTimer");

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("updatedRestTimer");
        //updating pick up spawn time
        currentPickUpTime += Time.deltaTime;
        //checks if the current spawntime is more than the upgrade spawn time and that one isnt spawned
        if (currentPickUpTime > actualPickUpTime && !spawnedPickUp)
        {
            //generates a random number based on the number of spawnpoints we have and
            //assigns one to be the spawn, finally it spawns a pickup
            int randnum = Random.Range(0, itemSpawnPoints.Length - 1);
            GameObject spawnLocation = itemSpawnPoints[randnum];
            pickUp = Instantiate(pickUpPrefab) as GameObject;
            pickUp.transform.position = spawnLocation.transform.position;
            spawnedPickUp = true;
            actualPickUpTime = Random.Range(pickUpMaxSpawnTime - 3.0f, pickUpMaxSpawnTime);
            actualPickUpTime = Mathf.Abs(actualPickUpTime);
            Debug.Log("Spawned");
        }
        //checks if the pick up has been picked up
        if (pickUp == null && spawnedPickUp == true)
        {
            currentPickUpTime = 0;
            spawnedPickUp = false;
            Debug.Log("deactive");
        }
        if (activeWave)
        {
            //checks if its time to spawn
            if (curSpawnedWave < MaxPerWave)
            {
               // Debug.Log("hi");
                if (enemiesPerSpawn > 0 && enemiesOnScreen < MaxPerWave)
                {
                    List<int> previousSpawnLocations = new List<int>();
                    if (enemiesPerSpawn > enemySpawnPoints.Length)
                    {
                        enemiesPerSpawn = enemySpawnPoints.Length - 1;
                    }

                    enemiesPerSpawn = (enemiesPerSpawn > MaxPerWave) ? enemiesPerSpawn - MaxPerWave : enemiesPerSpawn;
                    for (int i = 0; i < enemiesPerSpawn; i++)
                    {
                        if (curSpawnedWave < MaxPerWave + wave && enemiesOnScreen < maxEnemiesOnScreen)
                        {
                            Debug.Log("here");
                            enemiesOnScreen += 1;
                            int spawnPoint = -1;
                            while (spawnPoint == -1)
                            {
                                int randNum = Random.Range(0, enemySpawnPoints.Length - 1);
                                if (!previousSpawnLocations.Contains(randNum))
                                {
                                    previousSpawnLocations.Add(randNum);
                                    spawnPoint = randNum;
                                }
                            }
                            GameObject spawnLocation = enemySpawnPoints[spawnPoint];
                            GameObject newEnemy = Instantiate(enemy) as GameObject;
                            curSpawnedWave++;
                            Debug.Log("Enemy Spawned");
                            newEnemy.transform.position = spawnLocation.transform.position;
                            enemyBase enemyScript = newEnemy.GetComponent<enemyBase>();
                            if (player != null)
                            {
                                enemyScript.target = player.transform;
                                Vector3 targetRotation = new Vector3(player.transform.position.x,
                                       newEnemy.transform.position.y, player.transform.position.z);
                                newEnemy.transform.LookAt(targetRotation);
                            }
                            enemyScript.onDestroy.AddListener(enemyDestroyed);
                        }
                    }
                }

            }
            else if (curSpawnedWave == MaxPerWave && enemiesOnScreen == 0)
            {
                activeWave = false;
                restTimer = 10;
                curSpawnedWave = 0;
                Debug.Log("rest period");
                wave++;
                MaxPerWave++;
                maxEnemiesOnScreen++;

            }
        }
    }

    private IEnumerator updatedRestTimer()
    {
        if(!activeWave)
        {
            Debug.Log("hello?");
            yield return new WaitForSeconds(10);
                activeWave = true;
            
        }
    }

    
    public void enemyDestroyed()
    {
        enemiesOnScreen -= 1;
        Debug.Log("enemy destroyed");
    }
}

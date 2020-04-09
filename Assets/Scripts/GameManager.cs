using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public int level = 1;
    //game objects that will be needed in the script
    public GameObject player;
    private Player player1;
    public GameObject[] itemSpawnPoints;
    public GameObject[] enemySpawnPoints;
    public GameObject enemy;
    public GameObject[] pickUpPrefab;
    public GameObject[] statScreen;
    GameObject[] buyShotgun;
    GameObject[] buyShells;
    GameObject[] buyNade;
    GameObject[] buyHealth;
    GameObject[] buyMax;
    GameObject[] buyBrain;
    GameObject[] purchase;
    public ScoreCounter gameUI;
    public int score;
    public int bubblegum;
    private int price;
    private bool canPurchase = false;
    public bool isGameOver = false;
    private int itemID;

    //public vars so we can modify them as we need
    public int maxEnemiesOnScreen;
    public int enemiesPerSpawn;
    public int restTimer;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float pickUpMaxSpawnTime = 20.0f;
    public int wave = 0;
     
    //private data for keeping track of enemies on screen and time
    // between spawns of enemies and pick-ups
    private int enemiesOnScreen = 0;
    //these are for the spawning of pickups
    private bool spawnedPickUp = false;
    private float actualPickUpTime = 0;
    private float currentPickUpTime = 0;
    //number for a random pick up in array
    int pickUpNum;
    //these are for enemy spawns
    public int MaxPerWave = 5;
    private int curSpawnedWave = 0;
    GameObject pickUp;
    GameObject currency;
    //this lets us know if a wave is active
    public bool activeWave = true;
    //Difficulty
    public int curDifficulty;
    public float difficultyMod = 1.0f;
    //text mesh for dificulty selector
    public TextMeshProUGUI output;

    public GameObject tutorialCanvas;
    public Text tutorialText;
    int arrayPos;
    string[] textArray = { "Well hello there!",
                           "I didn't realize anyone was still around.",
                           "I see you were sleeping, you've missed quite a bit",
                           "I'll keep it short",
                           "I need you to get the cure off this planet to save the universe.",
                           "But before you begin, I should teach you some things...",
                           "This is an item that heals you for a bit of health",
                           "This item fully heals you",
                           "This next one is a minigun! cool, right? It can help you get out of sticky situations",
                           "Also, if you find any bubblegum, I would appreciate it if you could give them to me",
                           "Not for free, of course. I'll trade in some items of your choosing",
                           "To begin with, here is a laser gun, free of charge",
                           "Oh no! There's an enemy spotted ahead.",
                           "Use the left stick to move and the right stick to turn and shoot" };

    void Awake()
    {
        player1 = GameObject.FindObjectOfType<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0; // It's 0 so the tutorial can play without enemies spawning
        tutorialCanvas.SetActive(true);
        arrayPos = 0;

        
        singleton = this;
        actualPickUpTime = Mathf.Abs(actualPickUpTime);
        restTimer = 0;
        StartCoroutine("updatedRestTimer");
        buyShotgun = GameObject.FindGameObjectsWithTag("BuyShotgun");
        buyShells = GameObject.FindGameObjectsWithTag("BuyShells");
        buyNade = GameObject.FindGameObjectsWithTag("BuyNade");
        buyHealth = GameObject.FindGameObjectsWithTag("BuyHealth");
        buyMax = GameObject.FindGameObjectsWithTag("BuyMax");
        buyBrain = GameObject.FindGameObjectsWithTag("BuyBrain");
        purchase = GameObject.FindGameObjectsWithTag("Purchase");
        HidePurchase();
        setDifficulty(curDifficulty);
        actualPickUpTime = Random.Range((pickUpMaxSpawnTime * difficultyMod) - 3.0f, (pickUpMaxSpawnTime * difficultyMod));
        GetComponent<SaveSystem>().gameLoad();

    }

    // Update is called once per frame
    void Update()
    {
        if(level ==1)
        {
            Tutorial(); // plays tutorial
        }

        StartCoroutine("updatedRestTimer");
        //updating pick up spawn time
        currentPickUpTime += Time.deltaTime;
        if (wave <= 5)
        {
            //checks if the current spawn time is more than the upgrade spawn time and that one isnt spawned
            if (pickUpPrefab.Length > 0)
            {
                spawnItems();
            }
            if (activeWave)
            {
                //checks if its time to spawn
                if (curSpawnedWave < MaxPerWave)
                {
                    spawnWave();
                }
                else if (curSpawnedWave == MaxPerWave && enemiesOnScreen == 0)
                {
                    endWave();
                    //load shop menu here
                    GetComponent<SaveSystem>().gameSave();
                }
            }
            else
            {
                endWave();
                for (int i = 0; i < statScreen.Length; i++)
                {
                    statScreen[i].SetActive(true);
                }
                Time.timeScale = 0;
            }
        }
        
    }

    private IEnumerator updatedRestTimer()
    {
        if (!activeWave)
        {
            Debug.Log("hello?");
            yield return new WaitForSeconds(10);
            activeWave = true;

        }
    }

    public void enemyDestroyed()
    {
        //int gumChance = Random.Range(0, 10);
        enemiesOnScreen -= 1;
        //give gum and score on kill(testing score and bubblegum counters)
        bubblegum += 5;
        //gameUI.SetMoneyText(bubblegum);
        score += 100;
        //gameUI.SetScoreText(score);

        //currency = Instantiate(bubbleGum[gumChance]) as GameObject;
        Debug.Log("enemy destroyed");
    }
    public void Prices1()
    {
        foreach (GameObject g in buyShells)
        {
            price = 200;
            Debug.Log("Testing1");
        }
    }

    public void Prices2()
    {
        foreach (GameObject g in buyNade)
        {
            price = 3000;
            Debug.Log("Testing2");
        }
    }
    public void Prices3()
    {
        foreach (GameObject g in buyHealth)
        {
            price = 2500;
            Debug.Log("Testing3");
        }

    }

    public void Prices4()
    {
        foreach (GameObject g in buyMax)
        {
            price = 5000;
            Debug.Log("Testing4");
        }
    }
    public void Prices5()
    {
        foreach (GameObject g in buyBrain)
        {
            price = 3500;
            Debug.Log("Testing5");
        }
    }

    public void HidePurchase()
    {
        foreach (GameObject g in purchase)
        {
            g.SetActive(false);
        }
        Debug.Log("purchase:" + purchase.Length);
    }

    public void Buyable()
    {
        if (bubblegum >= price);
    }

    public void setDifficulty(int difficulty)
    {
        if (difficulty == 0)
        {
            difficultyMod = 0.5f;
            Debug.Log("Easy selected");
        }
        else if (difficulty == 1)
        {
            difficultyMod = 1.0f;
            Debug.Log("Medium selected");
        }
        else if (difficulty == 2)
        {
            difficultyMod = 2.0f;
            Debug.Log("Hard selected");
        }
    }

    public void roundDiffUpdate()
    {
        difficultyMod += 0.5f;
    }

    public void spawnWave()
    {
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
                    newEnemy.transform.position = spawnLocation.transform.position;
                    enemyBase enemyScript = newEnemy.GetComponent<enemyBase>();
                    newEnemy.GetComponent<enemyBase>().setDiff(difficultyMod);
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

    public void endWave()
    {
        activeWave = false;
        restTimer = 10;
        curSpawnedWave = 0;
        Debug.Log("rest period");
        //ups difficulty for next round
        roundDiffUpdate();
        wave++;
        //sets the max enemies
        MaxPerWave = (int)(MaxPerWave + (10 * difficultyMod));
        maxEnemiesOnScreen++;
        score += 500;
    }
    public void spawnItems()
    {
        if (currentPickUpTime > actualPickUpTime && !spawnedPickUp)
        {
            pickUpNum = Random.Range(0, 2);
            //generates a random number based on the number of spawn points we have and
            //assigns one to be the spawn, finally it spawns a pickup
            int randnum = Random.Range(0, itemSpawnPoints.Length - 1);
            GameObject spawnLocation = itemSpawnPoints[randnum];
            pickUp = Instantiate(pickUpPrefab[pickUpNum]) as GameObject;
            pickUp.transform.position = spawnLocation.transform.position;
            spawnedPickUp = true;
            actualPickUpTime = Random.Range((pickUpMaxSpawnTime * difficultyMod) - 3.0f, (pickUpMaxSpawnTime * difficultyMod));
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
    }

    void Tutorial()
    {
        tutorialText.text = textArray[arrayPos];

        if (Input.GetMouseButtonDown(0))
        {
            if (arrayPos >= textArray.Length - 1)
            {
                tutorialCanvas.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                arrayPos++;
            }
        }
    }
}

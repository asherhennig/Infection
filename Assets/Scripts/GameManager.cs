using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public int level = 1;
    public static int shotGunactive = 0;
    //game objects that will be needed in the script
    public GameObject player;
    private Player player1;
    public GameObject[] itemSpawnPoints;
    public GameObject[] enemySpawnPoints;
    public GameObject enemy;
    public GameObject[] pickUpPrefab;
    public GameObject statScreen;
    public int score;
    private int price;
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
    //this lets us know if a wave is active
    public bool activeWave = true;
    //Difficulty
    public int curDifficulty;
    public float difficultyMod;
    //text mesh for dificulty selector
    public TextMeshProUGUI output;
    public Ammo ammo;
    public Text timerText;
    public Text bubbleGumText;
    public Text scoreText;
    public Text shopBubbleGumText;

    private AudioManager audioManager;

    public Button shotgunHUD;
    public Button grenadeHUD;
    public Button brainadeHUD;
    public Button shotgunButton;
    
    public static int totalBubblegum;

    void Awake()
    {
        GetComponent<SaveSystem>().gameLoad();
        player1 = GameObject.FindObjectOfType<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ammo = Ammo.instance;
        Time.timeScale = 1;
        
        audioManager = AudioManager.instance;
        
        singleton = this;
        actualPickUpTime = Mathf.Abs(actualPickUpTime);
        restTimer = 0;
        StartCoroutine("updatedRestTimer");

        setDifficulty(curDifficulty);
        actualPickUpTime = Random.Range((pickUpMaxSpawnTime * difficultyMod) - 3.0f, (pickUpMaxSpawnTime * difficultyMod));
        
        setDifficulty(curDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        updateStatText();
        
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
                    statScreen.SetActive(true);
                    Time.timeScale = 0;
                    //load shop menu here
                    GetComponent<SaveSystem>().gameSave();
                }
            }
            if (wave > 5)
            {
                level++;
                nextLevel();
            }
        }
    }

    private IEnumerator updatedRestTimer()
    {
        if (!activeWave)
        {
            yield return new WaitForSeconds(10);
            activeWave = true;

        }
    }

    void nextLevel()
    {
        if (level == 1)
        {
            SceneManager.LoadScene("Lab");
        }
        else if (level == 2)
        {
            SceneManager.LoadScene("Forest");
        }
        else if (level == 3)
        {
            SceneManager.LoadScene("LaunchPad");
        }
    }

    public void enemyDestroyed()
    {
        enemiesOnScreen -= 1;
        //give gum and score on kill(testing score and bubblegum counters)
        //bubblegum += 50;
        totalBubblegum += 50;
        score += 100;
    }

    public void Prices()
    {
        price = 1000;
        itemID = 1;
    }

    public void Prices1()
    {
        price = 200;
        itemID = 2;
    }

    public void Prices2()
    {
        price = 3000;
        itemID = 3;
    }

    public void Prices3()
    {
        price = 2500;
        itemID = 4;
    }

    public void Prices4()
    {
        price = 5000;
        itemID = 5;
    }

    public void Prices5()
    {
        price = 3500;
        itemID = 6;
    }
    
    public void Buyable()
    {
        if (totalBubblegum >= price)
        {
            totalBubblegum = totalBubblegum - price;

            if (itemID == 1)
            {
                shotGunactive = 1;
                shotgunHUD.interactable = true;
                shotgunButton.interactable = false;
            }
            else if (itemID == 2)
            {
                ammo.AddAmmo("Shotgun", 3);
            }
            else if (itemID == 3)
            {
                ammo.AddAmmo("Grenade", 1);
                grenadeHUD.interactable = true;
            }
            else if (itemID == 4)
            {
                player1.curHealth++;
            }
            else if (itemID == 5)
            {
                player1.curHealth = player1.maxHealth;
            }
            else if (itemID == 6)
            {
                ammo.AddAmmo("lureGrenade", 1);
                brainadeHUD.interactable = true;
            }
        }
    }

    public void setDifficulty(int difficulty)
    {
        if (difficulty == 0)
        {
            difficultyMod = 0.5f;
            curDifficulty = 0;
        }
        else if (difficulty == 1)
        {
            difficultyMod = 1.0f;
            curDifficulty = 1;
        }
        else if (difficulty == 2)
        {
            difficultyMod = 2.0f;
            curDifficulty = 2;
        }
        gameObject.GetComponent<SaveSystem>().gameSave();
        
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
        audioManager.PlaySound("WaveClearSound");
        activeWave = false;
        restTimer = 10;
        curSpawnedWave = 0;
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
            actualPickUpTime = Random.Range((pickUpMaxSpawnTime * difficultyMod) + 60, (pickUpMaxSpawnTime * difficultyMod));
            actualPickUpTime = Mathf.Abs(actualPickUpTime);
        }
        //checks if the pick up has been picked up
        if (pickUp == null && spawnedPickUp == true)
        {
            currentPickUpTime = 0;
            spawnedPickUp = false;
        }
    }

    void updateStatText()
    {
        if (statScreen != null)
        {
            bubbleGumText.text = totalBubblegum.ToString();
            scoreText.text = score.ToString();
            shopBubbleGumText.text = "Bubblegum:\n" + totalBubblegum.ToString();
        }
    }

    public void continueTime()
    {
        Time.timeScale = 1;
    }
}

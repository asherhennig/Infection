using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject heart;
    public GameObject plusHeart;
    public GameObject minigun;
    public GameObject bubblegum;
    public GameObject enemy;
    GameObject clone;
    bool enemyDead = false;
    bool enemyAlive = false;

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
                           "Oh no! There's an enemy spotted ahead.",
                           "Use the left stick to move and the right stick to turn and shoot",
                           "Great job!",
                           "I think you're ready for the real thing now",
                           "Good luck!"};

    string[] textArray2 = { "Great job!",
                            "I think you're ready for the real thing now",
                            "Good luck!" };

    void Start()
    {
        Time.timeScale = 0;
        tutorialCanvas.SetActive(true);
        arrayPos = 0;
    }

    void Update()
    {
        playTutorial();
        Debug.Log("Array Position = " + arrayPos);

    }

    void playTutorial()
    {

        if (arrayPos >= textArray.Length - 3)
        {
            tutorialCanvas.SetActive(false);
            Time.timeScale = 1;

            if (enemyAlive == false)
            {
                enemyAlive = true;
                clone = Instantiate(enemy, new Vector3(0, 0, 15), Quaternion.identity);
            }

            if (clone == null)
            {
                enemyDead = true;

                if (enemyDead == true)
                {
                    Time.timeScale = 0;
                    tutorialCanvas.SetActive(true);
                    tutorialText.text = textArray[arrayPos];

                    if (Input.GetMouseButtonDown(0))
                    {
                        arrayPos++;
                    }

                    if (arrayPos == textArray.Length)
                    {
                        Debug.Log("Switching Scenese");
                        SceneManager.LoadScene("Lab.2");
                    }
                }
            }

        }
        else
        {
            tutorialText.text = textArray[arrayPos];

            if (Input.GetMouseButtonDown(0))
            {
                arrayPos++;
                showImages();
            }
        }
    }

    void showImages()
    {
        if (arrayPos == 6)
        {
            clone = Instantiate(heart, transform.position, Quaternion.Euler(-40, 0, 85));
        }
        if (arrayPos == 7)
        {
            Destroy(clone);
            clone = Instantiate(plusHeart, transform.position, Quaternion.Euler(-40, 0, -5));
            clone.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
        if (arrayPos == 8)
        {
            Destroy(clone);
            clone = Instantiate(minigun, transform.position, Quaternion.Euler(0, 90, 0));
            clone.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        if (arrayPos == 9)
        {
            Destroy(clone);
            clone = Instantiate(bubblegum, transform.position, Quaternion.Euler(0, 0, 0));
            clone.transform.localScale = new Vector3(2, 2, 2);
        }
        if (arrayPos == 10)
        {
            Destroy(clone);
        }
    }
}

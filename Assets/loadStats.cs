using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadStats : MonoBehaviour
{
    public GameObject[] statsBackground;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 1;
        statsBackground = GameObject.FindGameObjectsWithTag("statsBackground");
    }

    public void showStatsScreen()
    {
        foreach (GameObject g in statsBackground)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in statsBackground)
        {
            g.SetActive(false);
        }
    }
}

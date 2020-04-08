using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDown : MonoBehaviour
{
    public TextMeshProUGUI output;

    public void HandleInput(int difficulty)
    {
        if (difficulty == 0)
        {

            Debug.Log("Easy selected");
        }
        if (difficulty == 1)
        {

            Debug.Log("Medium selected");
        }
        if (difficulty == 2)
        {

            Debug.Log("Hard selected");
        }
    }

}

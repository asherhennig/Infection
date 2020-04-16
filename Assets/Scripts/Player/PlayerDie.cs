using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    Animator heroAnim;

    public GameObject playerDeathPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Player>().isDead == true)
        {
            heroAnim.SetBool("Dead", true);
            Instantiate(playerDeathPrefab, this.transform.position, Quaternion.identity);
        }

    }
}

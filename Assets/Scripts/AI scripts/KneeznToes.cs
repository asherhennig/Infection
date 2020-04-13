using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KneeznToes : enemyBase
{
    NavMeshAgent KneesandToes;

    void update()
    {
        KneesandToes.SetDestination(target.position);
    }

    //I hope I'm destroying the ZAR and not the player....
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
            Destroy(this.gameObject);
    }
}

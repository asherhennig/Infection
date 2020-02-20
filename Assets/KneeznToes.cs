using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class KneeznToes : MonoBehaviour
{
    public Transform target;    //so ZAR knows where player is
    public GameObject bulletprefab;
    public Vector3 destination;
    public Vector3 stoppingDistance;
    public float shotRange; 

    NavMeshAgent shooterZar;

    void Start()
    {
        shooterZar = this.GetComponent<NavMeshAgent>();
        shooterZar.stoppingDistance = shotRange - 2.5;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
}

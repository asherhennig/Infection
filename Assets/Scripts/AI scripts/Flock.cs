using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockManager fluckManager;       //linking the FlockManager
    float speed;
    bool turning = false;



    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(fluckManager.minSpeed, fluckManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        FishyRules();

        Bounds b = new Bounds(fluckManager.transform.position, fluckManager.limpLimits * 2);
        RaycastHit hit;
        Vector3 direction = fluckManager.transform.position - transform.position;

        if (!b.Contains(transform.position))
        {
            turning = true;
        }
        else if (Physics.Raycast(transform.position, this.transform.forward * 50, out hit))
        {
            turning = true;
            direction = Vector3.Reflect(this.transform.forward, hit.normal);

        }
        else
            turning = false;

        if (turning)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), fluckManager.rotationSpeed * Time.deltaTime);
        }
        else
        {

            if (Random.Range(0, 100) < 10)
                speed = Random.Range(fluckManager.minSpeed, fluckManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                FishyRules();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    void FishyRules()
    {
        GameObject[] fishset;       //Holder getting a hold of all the fish in the current flock
        fishset = fluckManager.allEnemy;

        Vector3 vcenter = Vector3.zero;
        Vector3 avAvoid = Vector3.zero;
        float globalSpeed = 0.01f;
        float neighDistance;
        int grpSize = 0;

        foreach (GameObject go in fishset)
        {
            if (go != this.gameObject)
            {
                neighDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (neighDistance <= fluckManager.neighbourDist)
                {
                    vcenter += go.transform.position;
                    grpSize++;

                    if (neighDistance < 1.0f)
                    {
                        avAvoid = avAvoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    globalSpeed = globalSpeed + anotherFlock.speed;

                }
            }
        }
        if (grpSize < 0)
        {
            vcenter = vcenter / grpSize + (fluckManager.goalPos - this.transform.position);     //average position (the goals pos minus fishes current pos.
            speed = globalSpeed / grpSize;

            Vector3 direction = (vcenter + avAvoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), fluckManager.rotationSpeed * Time.deltaTime);
        }
    }
}

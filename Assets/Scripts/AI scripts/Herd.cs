using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herd : MonoBehaviour
{

    public HerdManager zarsManager;       //linking the HerdManager
    public float speed;
    bool turning = false;



    // Start is called before the first frame update
    void Start()
    {
        //speed is set inbetween a random range of the min and max speed settings
        speed = Random.Range(zarsManager.minSpeed, zarsManager.maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        HerdFlow();

        Bounds b = new Bounds(zarsManager.transform.position, zarsManager.barrierLimits * 2);
        RaycastHit hit;
        Vector3 direction = zarsManager.transform.position - transform.position;

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

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), zarsManager.rotationSpeed * Time.deltaTime);
        }
        else
        {

            if (Random.Range(0, 100) < 10)
                speed = Random.Range(zarsManager.minSpeed, zarsManager.maxSpeed);
            if (Random.Range(0, 100) < 20)
                HerdFlow();
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    //Vector calculations for natual flocking/movement around each other
    void HerdFlow()
    {
        GameObject[] Zars;              //Holder getting a hold of all the ZARs in the current herd
        Zars = zarsManager.allEnemy;

        Vector3 avCenter = Vector3.zero; //AV center of the group
        Vector3 avAvoid = Vector3.zero;
        float globalSpeed = 0.01f;          //av speed of the group
        float neighDistance;                //how far each enemy is from each other, to make sure they dont bunch or bottleneck
        int grpSize = 0;

        foreach (GameObject go in Zars)     //
        {
            if (go != this.gameObject)
            {
                neighDistance = Vector3.Distance(go.transform.position, this.transform.position);
                if (neighDistance <= zarsManager.neighbourDist)
                {
                    avCenter += go.transform.position;
                    grpSize++;

                    if (neighDistance < 1.0f)
                    {
                        avAvoid = avAvoid + (this.transform.position - go.transform.position);
                    }

                    Herd AlienHerd = go.GetComponent<Herd>();
                    globalSpeed = globalSpeed + AlienHerd.speed;

                }
            }
        }
        if (grpSize < 0)
        {
            avCenter = avCenter / grpSize + (zarsManager.targetPos - this.transform.position);     //average position (the goals pos minus fishes current pos.
            speed = globalSpeed / grpSize;

                    //Slerp code for smooth animation turning once we have enemies with legs
            Vector3 direction = (avCenter + avAvoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), zarsManager.rotationSpeed * Time.deltaTime);
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject grenade;
    public Transform tossPos;
    public Vector3 throwPos;
    public float throwSpeed = 0.1f;
    public float TOF = 100.0f;
    bool tossed = false;
    public LayerMask LayerMask;
    Vector3 direction;
    GameObject tossedGrenade;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        throwGrenade();

        if (tossed && tossedGrenade != null)
        {
            Debug.Log(direction);
            Debug.Log(direction * TOF );
          //  Debug.Log(Time.deltaTime);
           // Debug.Log(TOF);
            tossedGrenade.GetComponent<Rigidbody>().MovePosition(tossedGrenade.transform.position + direction * TOF * Time.deltaTime );
        }
    }
    void throwGrenade()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInvoking("toss"))
            {
                Invoke("toss", 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("toss");
        }
    }

    //throws grenade
    void toss()
    {
        //spawns grenade we want thrown
        tossedGrenade = Instantiate(grenade) as GameObject;
        //spawns it at set throw point
        tossedGrenade.transform.position = tossPos.position;
        tossedGrenade.transform.rotation = tossPos.rotation;
        //check where the player clicked
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            //set were the grenade should go and send it there
            throwPos = hit.point;

            direction = (throwPos - tossPos.position).normalized;

          //  Debug.Log(direction);
            tossedGrenade.GetComponent<Rigidbody>().MovePosition(transform.position + direction * TOF * Time.deltaTime);

            tossed = true;
            //tossedGrenade.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(transform.position, throwPos, TOF * Time.deltaTime));
        }

    }
}
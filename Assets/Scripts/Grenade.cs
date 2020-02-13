using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject grenade;
    public Transform tossPos;
    public Vector3 throwPos;
    public float throwSpeed = 0.1f;
    public float TOF = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        throwGrenade();
    }
    void throwGrenade()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInvoking("toss"))
            {
                InvokeRepeating("toss", 0f, throwSpeed);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("toss");
        }
    }

    void toss()
    {
        GameObject tossedGrenade = Instantiate(grenade) as GameObject;
        // 2   
        tossedGrenade.transform.position = tossPos.position;
        tossedGrenade.transform.rotation = tossPos.rotation;
        //calls the grenade to explode after the fuse goes off
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            throwPos = hit.point;
            tossedGrenade.transform.Translate(throwPos * TOF * Time.deltaTime, Space.World);
        }
    }
}

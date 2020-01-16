using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        pos.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        pos.z += speed * Input.GetAxis("Vertical") * Time.deltaTime;

        transform.position = pos;
    }
}
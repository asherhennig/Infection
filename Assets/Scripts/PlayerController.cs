using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float rotSpeed = 1.0f;
    public LayerMask layerMask;

    private CharacterController characterController;

    private Vector3 currentLookTarget = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 
            0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * speed);
    }

    void FixedUpdate()
    {
        //this makes the palyer look at the cursor when its on screen
        //creating hit and ray variables
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //fires ray from camera and returns hit
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.point != currentLookTarget)
            {

            }
        }

        //sets where we want to turn to
        Vector3 targetPositon = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //gives smooth roation
        Quaternion rotation = Quaternion.LookRotation(targetPositon - transform.position);
        //turns to cursor 
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
    }
}
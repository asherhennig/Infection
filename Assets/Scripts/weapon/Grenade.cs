using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject grenade;
    public Transform tossPos;
    public Vector3 targetPos;
    public float throwSpeed = 0.1f;
    public float TOF = 1000.0f;
    public LayerMask LayerMask;
    public Ammo ammo;
    float gravity = 60f;
    public MeshRenderer inHand;
    public bool isPurchased;


    // Start is called before the first frame update
    void Start()
    {
    }

    IEnumerator myCoroutine()
    {
        ammo.ConsumeAmmo(tag);
        //spawns grenade we want thrown
        GameObject tossedGrenade = Instantiate(grenade) as GameObject;
        //spawns it at set throw point
        tossedGrenade.transform.position = tossPos.position;
        //check where the player clicked
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //set were the grenade should go and send it there
            targetPos = hit.point;
        }

        tossedGrenade.transform.position = tossPos.position;
        float arcAmount = 2f;
        float heightOfShot = 6f;
        Vector3 newVel = new Vector3();
        // Find the direction vector without the y-component

        Debug.Log("Target click Pos" + targetPos);
        Vector3 direction = targetPos - new Vector3(tossPos.position.x, 0f, tossPos.position.z);
        // Find the distance between the two points (without the y-component)
        float range = direction.magnitude;
        Debug.Log("direction click Pos" + direction);

        Debug.Log("Range" + range);

        // Find unit direction of motion without the y component
        Vector3 unitDirection = direction.normalized;
        Debug.Log("direction normalized" + direction.normalized);
        // Find the max height
        float maxYPos = tossPos.position.y + heightOfShot;

        // find the initial velocity in y direction
        newVel.y = Mathf.Sqrt(-2.0f * -gravity * (maxYPos - tossPos.position.y));
        // find the total time by adding up the parts of the trajectory
        // time to reach the max
        float timeToMax = Mathf.Sqrt(-2.0f * (maxYPos - tossPos.position.y) / -gravity);
        // time to return to y-targe
        float timeToTargetY = Mathf.Sqrt(-2.0f * (maxYPos - targetPos.y) / -gravity);
        // add them up to find the total flight time
        float totalFlightTime = timeToMax + timeToTargetY;
        // find the magnitude of the initial velocity in the xz direction
        float horizontalVelocityMagnitude = range / totalFlightTime;
        // use the unit direction to find the x and z components of initial velocity
        newVel.x = horizontalVelocityMagnitude * unitDirection.x;
        newVel.z = horizontalVelocityMagnitude * unitDirection.z;
        Debug.Log("newVel" + newVel);

        float elapse_time = 0;
        while (elapse_time < totalFlightTime)
        {
            tossedGrenade.transform.Translate(newVel.x * Time.deltaTime, (newVel.y - (gravity * elapse_time)) * Time.deltaTime, newVel.z * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(ammo.HasAmmo(tag))
        {
            inHand.enabled = true;
            throwGrenade();
        }
        else
        {
            inHand.enabled = false;
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
        StartCoroutine(myCoroutine());
    }
}

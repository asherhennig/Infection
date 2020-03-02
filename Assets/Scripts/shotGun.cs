using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : Gun
{
    public Transform[] firePositions;
    public int spread = 1;
    public float fireSpeed = 0.5f;
}

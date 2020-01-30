﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    //Weapon Types
    public const string Pistol = "Pistol";
    public const string Shotgun = "Shotgun";
    public const string Granade = "Granade";

    // Pick-ups
    public const int healthPickUp = 1;
    public const int granadePickUp = 2;
    public const int maxUpPickUp = 3;
    public const int miniGunPickUp = 4;

    //Enemy Types
    public const string Spider = "Spider";

    public static readonly int[] AllPickUps = new int[4]
    {
        healthPickUp,
        granadePickUp,
        maxUpPickUp,
        miniGunPickUp
    };

}
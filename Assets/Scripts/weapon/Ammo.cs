using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public static Ammo instance;

    public int pistolAmmo = 1; //this is needed to fire the pistol bc it "needs" ammo but is never ticked down so
    //its still infinite

    public int grenadeAmmo = 5;

    public int lureAmmo = 5;
   
    public int shotgunAmmo = 25;

    public Dictionary<string, int> tagToAmmo;

    private void Awake()
    {
        tagToAmmo = new Dictionary<string, int>
        {
            {Constants.Pistol, pistolAmmo},
            {Constants.Grenade, grenadeAmmo},
            {Constants.Shotgun, shotgunAmmo},
            {Constants.lureGrenade, lureAmmo}
        };

        instance = this;
    }

    public void AddAmmo(string tag, int ammo)
    {
        tagToAmmo[tag] += ammo;
    }

    public bool HasAmmo(string tag)
    {
        return tagToAmmo[tag] > 0;
    }

    public int GetAmmo(string tag)
    {
        return tagToAmmo[tag];
    }

    public int ConsumeAmmo(string tag)
    {
        return tagToAmmo[tag]--;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEquipper : MonoBehaviour
{

    public static string activeWeaponType;

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject granade;

    Ammo Ammo;

    GameObject activeGun;
    // Start is called before the first frame update
    void Start()
    {
        activeWeaponType = Constants.Pistol;
        activeGun = pistol;
    }

    private void loadWeapons(GameObject weapon)
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        granade.SetActive(false);

        weapon.SetActive(true);
        activeGun = weapon;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            loadWeapons(pistol);
            activeWeaponType = Constants.Pistol;
        }
        else if (Input.GetKeyDown("2") && Ammo.HasAmmo("Grenade") == true)
        {
            loadWeapons(granade);
            activeWeaponType = Constants.Granade;
        }
        else if (Input.GetKeyDown("3") && Ammo.HasAmmo("Shotgun") == true)
        {
            loadWeapons(shotgun);
            activeWeaponType = Constants.Shotgun;
        }
    }
}

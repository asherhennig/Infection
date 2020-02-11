using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEquiper : MonoBehaviour
{

    public static string activeWeaponType;

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject granade;
    public GameObject miniGun;

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
        miniGun.SetActive(false);

        weapon.SetActive(true);
        activeGun = weapon;
    }

    private void activeMiniGun()
    {
        loadWeapons(miniGun);
        activeWeaponType = Constants.miniGun;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            loadWeapons(pistol);
            activeWeaponType = Constants.Pistol;
        }
        else if (Input.GetKeyDown("2"))
        {
            loadWeapons(granade);
            activeWeaponType = Constants.Granade;
        }
        else if (Input.GetKeyDown("3"))
        {
            loadWeapons(shotgun);
            activeWeaponType = Constants.Shotgun;
        }
    }
}

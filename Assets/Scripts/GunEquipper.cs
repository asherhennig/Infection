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

    public GameObject miniGun;
    GameObject gameObject;
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
    //ACTIVATES MINI GUN AND STARTS THE CORUTINE TO FIRE
    public void activeMiniGun()
    {
        
        //load  mini gun
        loadWeapons(miniGun);
        //set active gun to mini gun
        activeWeaponType = Constants.miniGun;
        Debug.Log("mini gun away");
       
    }

    public void deactiveMiniGun()
    {
        //makes minigun not active and pistol active
        activeWeaponType = Constants.Pistol;
        Debug.Log("deactive minigun");
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
        else if (Input.GetKeyDown("3") )
        {
            loadWeapons(shotgun);
            activeWeaponType = Constants.Shotgun;
        }
    }
}

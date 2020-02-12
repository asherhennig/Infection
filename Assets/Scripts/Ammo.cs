using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    private int grenadeAmmo = 5;
    [SerializeField]
    private int shotgunAmmo = 25;

    public Dictionary<string, int> tagToAmmo;

    private void Awake()
    {
        tagToAmmo = new Dictionary<string, int>
        {
            {Constants.Granade, grenadeAmmo},
            {Constants.Shotgun, shotgunAmmo}
        };
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddAmmo(string tag, int ammo)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("unrecognized gun type passed: " + tag);
        }
        tagToAmmo[tag] += ammo;
    }

    public bool HasAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }

        return tagToAmmo[tag] > 0;
    }

    public int GetAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }
        return tagToAmmo[tag];
    }

    public int ConsumeAmmo(string tag)
    {
        if (!tagToAmmo.ContainsKey(tag))
        {
            Debug.LogError("Unrecognized gun type passed: " + tag);
        }
        return tagToAmmo[tag]--;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public GameObject weapon;
    //public GameObject weaponClone;
    public GameObject spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        //Instantiate(weapon, spawnpoint.transform.position, spawnpoint.transform.rotation, transform);

        weapon.transform.position = spawnpoint.transform.position;
        weapon.transform.rotation = spawnpoint.transform.rotation;
        //weapon.transform.SetParent(gameObject.transform);
    }

    void Update()
    {
        if (weapon == null)
        {
            //weapon = GameObject.FindGameObjectWithTag("Weapon");
            //Instantiate(weaponClone, spawnpoint.transform.position, spawnpoint.transform.rotation, transform);
            //RespawnWeapon();
        }      
    }

    void RespawnWeapon()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        //Instantiate(weapon, spawnpoint.transform.position, spawnpoint.transform.rotation, transform);

        weapon.transform.position = spawnpoint.transform.position;
        weapon.transform.rotation = spawnpoint.transform.rotation;
        //weapon.transform.SetParent(gameObject.transform);
    }
}

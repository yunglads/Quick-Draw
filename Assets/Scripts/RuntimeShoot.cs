using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeShoot : MonoBehaviour
{
    Button button;

    public WeaponController weaponController;
    // Start is called before the first frame update
    void Start()
    {
        weaponController = FindObjectOfType<WeaponController>();
        
        button = GetComponent<Button>();
        button.onClick.AddListener(weaponController.MobileShoot);
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponController == null)
        {
            weaponController = FindObjectOfType<WeaponController>();
        }
    }

    //public void Shoot()
    //{
    //    weaponController.MobileShoot();
    //}
}

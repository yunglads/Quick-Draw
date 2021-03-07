using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public int ammoCount = 6;
    public float range = 100f;
    public float force = 10f;

    bool mobileShootPressed = false;

    [HideInInspector]
    public RaycastHit hit;

    public Camera playerCam;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    GameObject target;

    GameObject[] enemiesList;
    int enemies;

    Player player;

    private void Start()
    {
        enemiesList = GameObject.FindGameObjectsWithTag("Enemy");
        enemies = enemiesList.Length;

        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0) && ammoCount > 0)
        {
            Shoot();
        }
#endif
#if UNITY_IOS
        if (mobileShootPressed && ammoCount > 0)
        {
            Shoot();
            mobileShootPressed = false;
        }
#endif

        if (enemies <= 0)
        {
            player.allEnemiesDead = true;
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            target = hit.transform.gameObject;

            if (hit.transform.tag == "Enemy")
            {
                GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGO, 1f);
                target.GetComponent<EnemyAI>().isDead = true;
                target.GetComponent<EnemyAI>().Die();
                enemies--;
            }
        }

        ammoCount--;
    }

    public void MobileShoot()
    {
        mobileShootPressed = true;
    }
}

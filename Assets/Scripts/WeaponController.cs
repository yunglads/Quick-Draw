using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponController : MonoBehaviour
{
    public int ammoCount;
    public int resetAmmoCount;
    public float range = 100f;
    public float force = 10f;

    bool mobileShootPressed = false;

    [HideInInspector]
    public RaycastHit hit;

    public Camera playerCam;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private void Start()
    {
        playerCam = GameObject.FindGameObjectWithTag("PlayerCam").GetComponent<Camera>();
    }

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

#if UNITY_ANDROID
        if (mobileShootPressed && ammoCount > 0)
        {
            Shoot();
            mobileShootPressed = false;
        }
#endif

        if (playerCam == null && SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu"))
        {
            playerCam = GameObject.FindGameObjectWithTag("PlayerCam").GetComponent<Camera>();
        }
    }

    public void Shoot()
    {
        muzzleFlash.Play();

        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            BodyHit bodyHit = hit.transform.gameObject.GetComponent<BodyHit>();

            if (bodyHit != null)
            {
                EnemyAI enemy = bodyHit.GetComponentInParent<EnemyAI>();
                if (enemy.allHitsRequired)
                {
                    for (int i = 0; i < enemy.requiredHits.Count; i++)
                    {
                        if (enemy.requiredHits[i].bodyPart == bodyHit.bodyPart)
                        {
                            enemy.requiredHits.RemoveAt(i);
                            i--;
                        }
                    }
                    if (enemy.requiredHits.Count == 0)
                    {
                        Shoot(enemy);
                    }
                }
                else
                {
                    for (int i = 0; i < enemy.requiredHits.Count; i++)
                    {
                        if (enemy.requiredHits[i].bodyPart == bodyHit.bodyPart)
                        {
                            Shoot(enemy);
                        }
                    }
                    if (enemy.requiredHits.Count == 0)
                    {
                        Shoot(enemy);
                    }
                }
            }
        }
        ammoCount--;
    }

    void Shoot(EnemyAI enemy)
    {
        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 1f);
        enemy.EnemyDeath(hit.point, force);
        FightController.Instance.KillEnemy();
    }

    public void MobileShoot()
    {
        mobileShootPressed = true;
        //Debug.Log("shoot button pressed");
    }

    public void ResetAmmoCount()
    {
        ammoCount = resetAmmoCount;
        Debug.Log("resetting ammo from: " + ammoCount + " to " + resetAmmoCount);
    }
}

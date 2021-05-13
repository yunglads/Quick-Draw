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
    }

    void Shoot()
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
    }
}

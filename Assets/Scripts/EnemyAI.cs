using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float timeTilShoot = 3f;
    public float timer;
    public float chanceToHit = 65f;
    float rngHit;

    public GameObject gun;

    public ParticleSystem muzzleFlash;

    public bool isDead = false;
    public bool playerHit = false;
    public bool gunIsDrawn = false;
    //public bool gunDrawn = false;

    WeaponController weaponController;

    // Start is called before the first frame update
    void Start()
    {
        SetRigidbodyState(true);
        SetColliderState(false);
        GetComponent<Animator>().enabled = true;
        weaponController = FindObjectOfType<WeaponController>();

        rngHit = Random.Range(0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (gunIsDrawn)
        {
            timer += Time.deltaTime;

            if (!isDead && timer >= timeTilShoot && !playerHit)
            {
                EnemyShoot();
                timer = 0;
                if (chanceToHit >= rngHit)
                {
                    playerHit = true;
                }
                else
                {
                    rngHit = Random.Range(0, 100);
                }
            }
        }

        if (isDead)
        {
            Ragdoll();
            gun.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void EnemyShoot()
    {
        muzzleFlash.Play();
    }

    public void Die()
    {
        GetComponent<Animator>().enabled = false;
        SetRigidbodyState(false);
        SetColliderState(true);
        
        //if (gameObject != null)
        //{
        //    Destroy(gameObject, 3f);
        //}
    }

    void Ragdoll()
    {
        Collider[] colliders = Physics.OverlapSphere(weaponController.hit.point, 1f);

        foreach (Collider closeObjects in colliders)
        {
            Rigidbody rigidbody = closeObjects.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(weaponController.force, weaponController.hit.point, 1f);
            }
        }
    }

    void SetRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }


    void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

}

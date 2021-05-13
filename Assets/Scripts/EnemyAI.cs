using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    public List<RequiredHit> requiredHits = new List<RequiredHit>();

    public bool allHitsRequired;

    public float timeTilShoot = 3f;
    public float timer;
    [Tooltip("A higher number will lower the chance the enemy will sucessfully shoot player")]
    public float chanceToHit = 65f;
    float rngHit;

    public GameObject gun;

    public ParticleSystem muzzleFlash;

    public bool isDead = false;
    public bool playerHit = false;
    public bool gunIsDrawn = false;

    HitPoint hitPoint;
    void Start()
    {
        SetRigidbodyState(true);

        rngHit = Random.Range(0, 100);
    }

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
                    FightController.Instance.KillPlayer();
                }
                else
                {
                    rngHit = Random.Range(0, 100);
                }
            }
        }
    }

    void EnemyShoot()
    {
        muzzleFlash.Play();
    }

    public void EnemyDeath(Vector3 _position, float _force)
    {
        hitPoint = new HitPoint(_position, _force);
        isDead = true;
        Ragdoll();
        gun.GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Animator>().enabled = false;
        SetRigidbodyState(false);
    }

    void Ragdoll()
    {
        //Collider[] colliders = Physics.OverlapSphere(weaponController.hit.point, 1f);
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider closeObjects in colliders)
        {
            Rigidbody rigidbody = closeObjects.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                //rigidbody.AddExplosionForce(weaponController.force, weaponController.hit.point, 1f);
                rigidbody.AddExplosionForce(hitPoint.force, hitPoint.position, 1f);
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
}

public class HitPoint
{
    public Vector3 position;
    public float force;

    public HitPoint(Vector3 _position, float _force)
    {
        position = _position;
        force = _force;
    }
}


[System.Serializable]
public class RequiredHit
{
    public BodyPart bodyPart;
}

public enum BodyPart
{
    Head,
    Chest,
    ArmLeft,
    ArmRight,
    LegLeft,
    LegRight,
    None
}
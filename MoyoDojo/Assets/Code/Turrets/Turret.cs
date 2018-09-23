using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    private Transform target;
    private AudioSource source;

    private int MaxRangeUpgrade = 5;
    private int MaxDmgUpgrade = 5;
    private int MaxBulletSpeedUpgrade = 9;
    private float nextFire = 0.0f;
    private float bulletspeed = 5f;

   public Transform partToRotate;
    public Transform[] turretEnd;
    public AudioClip shootTurret;
    public GameObject TurretBullet;

    public float fireRate = 5f;
    public int BulletDmg = 30;
    public int bulletSpeedCost;
    public int bullDmgCost;
    public int rangeCost;
    public int cost;
    public float range = 15f;


    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (PauseSkript.GameIsPaused == false)
        {
            if (target == null) return;
            partToRotate.transform.LookAt(target);
            if (Time.time > nextFire)
            {
                for (int i = 0; i < turretEnd.Length; i++)
                {
                    float tempx, tempy, tempz;
                    Vector3 tempvelo = new Vector3();
                    source.PlayOneShot(shootTurret);
                    GameObject inst_bullet = Instantiate(TurretBullet, turretEnd[i].position, turretEnd[i].rotation) as GameObject;
                    TurretBullet tBSkript = inst_bullet.GetComponent<TurretBullet>();
                    tBSkript.BulletDMG = BulletDmg;
                    Rigidbody bulletRigidbody = inst_bullet.GetComponent<Rigidbody>();
                    tempx = (target.position.x - turretEnd[i].position.x) * bulletspeed;
                    tempy = (target.position.y - turretEnd[i].position.y) * bulletspeed;
                    tempz = (target.position.z - turretEnd[i].position.z) * bulletspeed;

                    tempvelo = new Vector3(tempx, tempy, tempz);

                    bulletRigidbody.velocity = tempvelo;


                }
                nextFire = Time.time + fireRate;
            }
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("NPC");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject item in enemies)
        {
            float distancceToEnemy = Vector3.Distance(transform.position, item.transform.position);
            if (distancceToEnemy < shortestDistance)
            {
                shortestDistance = distancceToEnemy;
                nearestEnemy = item;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public int BullDmgCost
    {
        get { return bullDmgCost; }
        set { bullDmgCost = value; }
    }

    public int BulletSpeedCost
    {
        get { return bulletSpeedCost; }
        set { bulletSpeedCost = value; }
    }

    public int RangeCost
    {
        get { return rangeCost; }
        set { rangeCost = value; }
    }

    public int maxRangeUpgrade
    {
        get { return MaxRangeUpgrade; }
        set { MaxRangeUpgrade = value; }
    }

    public int maxBulletSpeedUpgrade
    {
        get { return MaxBulletSpeedUpgrade; }
        set { MaxBulletSpeedUpgrade = value; }
    }

    public int maxBullDmgUpgrade
    {
        get { return MaxDmgUpgrade; }
        set { MaxDmgUpgrade = value; }
    }
}

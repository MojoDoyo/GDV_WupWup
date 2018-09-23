using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

    public float lifeDuration = 2f;
    private float lifeTimer;
    private int bulletDMG;

    // Use this for initialization
    void Start()
    {
        lifeTimer = lifeDuration;
    }

     void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public int BulletDMG
    {
        get { return bulletDMG; }
        set { bulletDMG = value; }
    }
        
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "NPC")
        {
            collider.GetComponent<NPCStats>().TakeDamage(BulletDMG);
        }

        Destroy(this.gameObject);
    }
}

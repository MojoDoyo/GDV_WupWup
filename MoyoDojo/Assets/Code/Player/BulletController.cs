using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletController : MonoBehaviour {

    public float lifeDuration = 2f;
    private float lifeTimer;
    private int damagePlayerBullet;



    void Start ()
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

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "NPC")
        {
            collider.GetComponent<NPCStats>().TakeDamage(damagePlayerBullet);
        }

        Destroy(this.gameObject);
    }

    public int PlayerDMG
    {
        get { return damagePlayerBullet; }
        set { damagePlayerBullet = value; }
    }
}


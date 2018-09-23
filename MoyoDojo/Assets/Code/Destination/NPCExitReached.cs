using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCExitReached : MonoBehaviour {
    PlayerStats PStats;
    GameObject player;                          // Reference to the player GameObject.
    public GameObject Startpoint;

    void Awake()
    {
        // Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");
        PStats = player.GetComponent<PlayerStats>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            Destroy(other.gameObject);
            PStats.TakeDamage(5);

        }

        if (other.tag == "WalkAgent")
        {

            Destroy(other.gameObject);
        }

    }
}

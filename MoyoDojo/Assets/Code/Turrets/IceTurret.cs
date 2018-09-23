using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTurret : MonoBehaviour {

    public float range = 15f;

    // Use this for initialization
    void Start () {
		//GameObject[] enemies = GameObject.FindGameObjectsWithTag("NPC");
	}

    // Update is called once per frame
    void Update()
    {
        if (PauseSkript.GameIsPaused == false)
        {

        }
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

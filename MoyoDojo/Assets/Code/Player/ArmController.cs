using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour { 

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Tower"))
        {
            GetComponent<Animation>().Play("armRunter");
            Debug.Log("Runter");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Tower"))
        {
            GetComponent<Animation>().Play("armHoch");
            Debug.Log("Hoch");
        }
    }
}

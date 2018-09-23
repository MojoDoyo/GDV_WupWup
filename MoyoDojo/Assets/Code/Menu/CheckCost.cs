using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCost : MonoBehaviour {

    private GroundPlacementController GPC;
    private int[] arr_Cost = new int[5];
	// Use this for initialization
	void Start () {
        GPC = Camera.main.GetComponent<GroundPlacementController>();
        GPC.getPreFabTurretCost(ref arr_Cost);
        GameObject.Find("TowerBaseMoney").GetComponent<Text>().text = arr_Cost[0].ToString();
        GameObject.Find("Image Turret 1").GetComponentInChildren<Text>().text = arr_Cost[1].ToString();
        GameObject.Find("Image Turret 2").GetComponentInChildren<Text>().text = arr_Cost[2].ToString();
        GameObject.Find("Image Turret 3").GetComponentInChildren<Text>().text = arr_Cost[3].ToString();
        GameObject.Find("Image Turret 4").GetComponentInChildren<Text>().text = arr_Cost[4].ToString();
    }
	// Update is called once per frame
	void Update () {
		
	}
}

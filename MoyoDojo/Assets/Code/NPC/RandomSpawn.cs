using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawn : MonoBehaviour {

    public GameObject StartPoint;
    public GameObject Endpoint;
    public Transform StartObjekt;

    private float SPlaceX;
    private float SPlaceZ;
    private float EPlaceX;
    private float EPlaceZ;

    private void Start()
    {
        SPlaceX = Random.Range(24, -24);
        SPlaceZ = Random.Range(24, -24);
        EPlaceX = Random.Range(24, -24);
        EPlaceZ = Random.Range(24, -24);

        StartPoint.transform.position = new Vector3(SPlaceX, 1, SPlaceZ);
        Instantiate(StartObjekt);
        StartObjekt.transform.position = new Vector3(SPlaceX, 1, SPlaceZ);
        Endpoint.transform.position = new Vector3(EPlaceX, 1, EPlaceZ);

    }

}

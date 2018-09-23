using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class GroundPlacementController : MonoBehaviour
{

    //Money System
    public GameObject MoneySystem;
    MoneySystem money;

    [SerializeField]
    private GameObject[] placeableObjectPrefabs = null;
    private int currentPrefabIndex = -1;

    //HotKeySelection-gO
    private GameObject prefabPlacementObject;

    //GreenCube
    public GameObject prefabOK;

    //RedCube 
    public GameObject prefabFail;

    //Playground
    public GameObject presetPlane;

    //Grid Resolution 
    public float grid = 2.0f;

    // Mask of the ground to hit
    [Tooltip("Which layer to use for raycast target")]
    public LayerMask mask = 24;

    // Store which spaces are in use
    // 0 = Free
    // 1 = TowerBase
    // 2 = Occupied
    int[,] usedSpace;

    private GameObject placementObject = null;
    private GameObject areaObject = null;
    private AudioSource source;
    private Bounds bounds;

    public AudioClip placeTurret;

    bool mouseClick = false;
    bool playToogle = false;
    Vector3 lastPos;

    private Mesh floorMesh;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        money = MoneySystem.GetComponent<MoneySystem>();

        Cursor.lockState = CursorLockMode.Locked;

        if (presetPlane.GetComponent<MeshFilter>() != null)
        {        
            floorMesh = presetPlane.GetComponent<MeshFilter>().mesh;
            bounds = floorMesh.bounds;
            bounds.Expand(presetPlane.transform.localScale*10);
        }

        Vector3 slots = bounds.size / grid;
        usedSpace = new int[Mathf.CeilToInt(slots.x), Mathf.CeilToInt(slots.z)];

        for (var x = 0; x < Mathf.CeilToInt(slots.x); x++)
        {
            for (var z = 0; z < Mathf.CeilToInt(slots.z); z++)
            {
                usedSpace[x, z] = 0;
            }
        }
    }

    private void Update()
    {
        if (PauseSkript.GameIsPaused == false)
        {
            if (playToogle == false)
            {
                HandleNewObjectHotkey();
                if (prefabPlacementObject != null)
                {
                    ReleaseIfClicked();
                }
            }
        }
    }
    public void toogleScript()
    {
        playToogle = !playToogle;
        Destroy(areaObject);
        Destroy(placementObject);
        prefabPlacementObject = null;
        currentPrefabIndex = -1;
    }

    public int[] getPreFabTurretCost(ref int[] refCost)
    {

        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (i == 0)
            {
                refCost[i] = placeableObjectPrefabs[0].GetComponent<TowerBaseStats>().cost;
            }
            else refCost[i] = placeableObjectPrefabs[i].GetComponent<Turret>().cost;
        }
        return refCost;
    }

    private void HandleNewObjectHotkey()
    {
        for (int i = 0; i < placeableObjectPrefabs.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                //De-select Hotkey
                if (PressedKeyOfCurrentPrefab(i))
                {
                    Destroy(placementObject);
                    prefabPlacementObject = null;
                    Destroy(areaObject);
                    currentPrefabIndex = -1;
                }
                else
                {
                    //Destroy last selection
                    if (placementObject != null)
                    {
                        Destroy(placementObject);
                        prefabPlacementObject = null;
                    }
                    prefabPlacementObject = placeableObjectPrefabs[i];
                    currentPrefabIndex = i;
                }
            }
        }
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return placementObject != null && currentPrefabIndex == i;
    }

    private void ReleaseIfClicked()
    {
        if (PauseSkript.GameIsPaused == false)
        {
            Vector3 point;

            // Check for mouse ray collision with this object
            if (getTargetLocation(out point))
            {
                Vector3 halfSlots = bounds.size / 2.0f;

                int x = (int)Math.Round(Math.Round(point.x - presetPlane.transform.position.x + halfSlots.x - grid / 2.0f) / grid);
                int z = (int)Math.Round(Math.Round(point.z - presetPlane.transform.position.z + halfSlots.z - grid / 2.0f) / grid);

                point.x = (float)(x) * grid - halfSlots.x + presetPlane.transform.position.x + grid / 2.0f;
                point.z = (float)(z) * grid - halfSlots.z + presetPlane.transform.position.z + grid / 2.0f;

                if (lastPos.x != x || lastPos.z != z || areaObject == null)
                {

                    lastPos.x = x;
                    lastPos.z = z;
                    if (areaObject != null)
                    {
                        Destroy(areaObject);
                    }
                    areaObject = (GameObject)Instantiate(usedSpace[x, z] == 0 || usedSpace[x, z] == 1 && prefabPlacementObject.name != placeableObjectPrefabs[0].name ? prefabOK : prefabFail, point, Quaternion.identity);
                }

                // Create or move the object
                if (!placementObject)
                {
                    placementObject = (Instantiate(prefabPlacementObject, point, Quaternion.identity)) as GameObject;
                }
                else
                {
                    placementObject.transform.position = point;
                }

                // On left click, insert the object to the area and mark it as "used"
                if (Input.GetMouseButtonDown(0) && mouseClick == false)
                {
                    mouseClick = true;
                    // Place the object TowerBase
                    if (usedSpace[x, z] == 0 && prefabPlacementObject.name == placeableObjectPrefabs[0].name)
                    {
                        if (money.decreaseMoney(prefabPlacementObject.GetComponent<TowerBaseStats>().cost))
                        {
                            source.PlayOneShot(placeTurret);
                            usedSpace[x, z] = 1;
                            // ToDo: place the result somewhere..
                            Instantiate(placeableObjectPrefabs[0], point, Quaternion.identity);
                        }
                    }
                    //Place Tower+Base
                    else if (usedSpace[x, z] == 0 && prefabPlacementObject.name != placeableObjectPrefabs[0].name)
                    {
                        int iCost;

                        iCost = prefabPlacementObject.GetComponent<Turret>().cost + placeableObjectPrefabs[0].GetComponent<TowerBaseStats>().cost;
                        if (money.decreaseMoney(iCost))
                        {
                            source.PlayOneShot(placeTurret);
                            usedSpace[x, z] = 2;
                            // ToDo: place the result somewhere..
                            Instantiate(placeableObjectPrefabs[0], point, Quaternion.identity);
                            point += new Vector3(0, 0.36f, 0);
                            Instantiate(prefabPlacementObject, point, Quaternion.identity);
                        }

                    }
                    //Place Tower on Base
                    else if (usedSpace[x, z] == 1 && prefabPlacementObject.name != placeableObjectPrefabs[0].name)
                    {
                        if (money.decreaseMoney(prefabPlacementObject.GetComponent<Turret>().cost))
                        {
                            source.PlayOneShot(placeTurret);
                            usedSpace[x, z] = 2;
                            // ToDo: place the result somewhere..
                            point += new Vector3(0, 0.36f, 0);
                            Instantiate(prefabPlacementObject, point, Quaternion.identity);
                        }

                    }
                    Destroy(placementObject);
                    prefabPlacementObject = null;

                    Destroy(areaObject);
                }
                else if (!Input.GetMouseButtonDown(0))
                {
                    mouseClick = false;
                }
            }
            else
            {
                if (placementObject)
                {
                    Destroy(placementObject);
                    placementObject = null;
                }
                if (areaObject)
                {
                    Destroy(areaObject);
                    areaObject = null;
                }
            }
        }
    }

    /// <param name="i_Enum">'0' = TowerBase, '1' = Turret.</param>
    public bool UsedSpace(int i_Enum, GameObject objRef)
    {
        Vector3 halfSlots = bounds.size / 2.0f;
        int x = (int)Math.Round(Math.Round(objRef.transform.position.x - presetPlane.transform.position.x + halfSlots.x - grid / 2.0f) / grid);
        int z = (int)Math.Round(Math.Round(objRef.transform.position.z - presetPlane.transform.position.z + halfSlots.z - grid / 2.0f) / grid);
            //Towerbase

            if (i_Enum == 0)
            {
            if (usedSpace[x, z] != 2)
            {
                usedSpace[x, z] = 0;
                return true;
            }
            return false;
            }
            //Turret
            else if (i_Enum == 1)
            {
                usedSpace[x, z] = 1;
            return true;
            }
        return false;
    }

    bool getTargetLocation(out Vector3 point)
    {
        Vector3 playerPosition = transform.position;
        Vector3 forwardDirection = transform.forward;

        Ray interactionRay = new Ray(playerPosition, forwardDirection);
        RaycastHit hitInfo;
        if (Physics.Raycast(interactionRay, out hitInfo, Mathf.Infinity, mask))
        {
            if (hitInfo.collider == presetPlane.GetComponent<Collider>())
            {
                point = hitInfo.point;
                return true;
            }
        }
        point = Vector3.zero;
        return false;
    }

}

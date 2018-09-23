using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPCMove : MonoBehaviour {

    [SerializeField]
    Transform _destination;
    NavMeshAgent _navMeshAgent;
    WaveSpawner wS;

    public bool cantReach = false;

    private void Awake()
    {
        wS = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<WaveSpawner>();
    }

    // Use this for initialization
    void Start () {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = true;

        if(_navMeshAgent == null)
        {
            Debug.LogError("Nav MEsh ist not attached to " + gameObject.name);
        }
        else
        {
            SetDestination();
        }
	}

    private void Update()
    {

        if (wS.GamePhaseStartButton.activeSelf == true)
        {
            CantReachEnd();
        }
    }

    private void SetDestination()
    {
        if(_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    public void CantReachEnd ()
    {
        _navMeshAgent.CalculatePath(this.transform.position, _navMeshAgent.path);

        if (_navMeshAgent.path.status == NavMeshPathStatus.PathPartial)
        {
            cantReach = true;
        }
        else cantReach = false;
    }
}

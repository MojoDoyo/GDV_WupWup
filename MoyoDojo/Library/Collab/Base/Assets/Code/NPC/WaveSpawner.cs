using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    public Transform enemyPrefab;
    public Transform walkAgent;
    public Transform SpawnPoint;
    public Transform EndObjekt;
    public GameObject PlayGround;
    public GameObject EnemyDetect;
    public GameObject BuildHud;
    public GameObject Hammer;
    public GameObject Gun;

    public float PlaceX;
    public float PlaceZ;
    bool SpawnEnemyPhase = false;
    bool toggle;
   
    private int waveNumber = 1;

    public GameObject GamePhaseStartButton;

    private void Awake()
    {
        GamePhaseStartButton.SetActive(true);
        Hammer.SetActive(true);

    }


    private void Update()
    {
        if(GamePhaseStartButton.active == true && GameObject.Find("WalkAgent(Clone)") == null)
        {
            Instantiate(walkAgent, SpawnPoint.position, SpawnPoint.rotation);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            toggle = !toggle;

            if (toggle == true && GamePhaseStartButton.active == true)
            { 
                Time.timeScale = 0f;
                Camera_Control.sensHorizontal = 0f;
                Camera_Control.sensVertical = 0f;
                Cursor.lockState = CursorLockMode.None;
            }else
            {
                Time.timeScale = 1f;
                Camera_Control.sensHorizontal = 8f;
                Camera_Control.sensVertical = 8f;
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

        if (SpawnEnemyPhase == true)
        {
            GamePhaseStartButton.SetActive(false);
            Hammer.SetActive(false);
            Gun.SetActive(true);
            BuildHud.SetActive(false);
            Time.timeScale = 1f;
            Camera_Control.sensHorizontal = 8f;
            Camera_Control.sensVertical = 8f;
            Cursor.lockState = CursorLockMode.Locked;
            SpawnWave();
            SpawnEnemyPhase = false;
            Debug.Log(SpawnEnemyPhase + "spawn phase");
        }
        
        if (GameObject.Find("Enemy(Clone)") == null)
        {
            GamePhaseStartButton.SetActive(true);
            Hammer.SetActive(true);
            Gun.SetActive(false);
            BuildHud.SetActive(true);

            waveNumber = 1;
            toggle = false;
        }
    }

    public void StarteWavePhase()
    {
        SpawnEnemyPhase = true;
        Destroy(GameObject.Find("WalkAgent(Clone)"));
    }
    
    void SpawnWave()
    {
        for (int i = 3; i >= waveNumber; waveNumber++)
        {
            SpawnEnemy();
        }
        
    }

    void SpawnEnemy()
    { 
        Instantiate(enemyPrefab, SpawnPoint.position, SpawnPoint.rotation);
    }

}

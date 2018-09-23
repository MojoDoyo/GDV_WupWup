using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    private PauseSkript pS;
    private GroundPlacementController GPC;
    private NPCMove nPCMove;
    private AudioSource source;
    private PlayerSkills playerSkill;
    private Tutorial tut;

    private int EnemyEnergyMaxCost = 10;
    private int temp = 0;
    private int pick = 0;
    private int getCost = 0;
    private int factor = 0;
    private int difficultyPick = 0;

    [SerializeField]
    private Transform[] enemyPrefab;
    [SerializeField]
    private Transform enemyBoss;

    public Transform walkAgent;
    public Transform SpawnPoint;
    public Transform EndObjekt;

    public Text waveCounter;

    public GameObject GamePhaseStartButton;
    public GameObject cantReachMessage;
    public GameObject PlayGround;
    public GameObject BuildHud;
    public GameObject SkillHuD;
    public GameObject Hammer;
    public GameObject Gun;

    public AudioClip ckickSound;
    public AudioClip deny;

    public int waveNumber = 1;

    bool SpawnEnemyPhase = false;
    bool toggle = false;
    bool waitWave = false;
    bool waitWaves = false;

    enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Impossible
    }

    public int DifficultyPick
    {
        get { return difficultyPick; }
        set { difficultyPick = value; }
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        tut = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<Tutorial>();
        GPC = Camera.main.GetComponent<GroundPlacementController>();
        pS = GameObject.Find("Canvas").GetComponent<PauseSkript>();
        playerSkill = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSkills>();

        playerSkill.enabled = false;
        GamePhaseStartButton.SetActive(true);
        Hammer.SetActive(true);
        waveCounter.text = ("Wave: ") + waveNumber.ToString();
        pS.entFreezeGame();
        BuildHud.SetActive(true);
        SkillHuD.SetActive(false);
    }
    private void Start()
    {
        difficultyPick = PlayerPrefs.GetInt("Dif");
        if (difficultyPick == (int)Difficulty.Easy)
        {
            factor = 1;
        }
        else if (difficultyPick == (int)Difficulty.Normal)
        {
            factor = 2;
        }
        else if (difficultyPick == (int)Difficulty.Hard)
        {
            factor = 3;
        }
        else if (difficultyPick == (int)Difficulty.Impossible)
        {
            factor = 5;
        }
        Debug.Log(difficultyPick);
    }

    private void Update()
    {
        if (PauseSkript.GameIsPaused == false)
        {

            if (GamePhaseStartButton.activeSelf == true && GameObject.Find("WalkAgent(Clone)") == null)
            {
                Instantiate(walkAgent, SpawnPoint.position, SpawnPoint.rotation);
                nPCMove = GameObject.FindGameObjectWithTag("WalkAgent").GetComponent<NPCMove>();
            }


            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                toggle = !toggle;
                if (SpawnEnemyPhase == false)
                {
                    GPC.toogleScript();
                }

                if (toggle == true && GamePhaseStartButton.activeSelf == true)
                {
                    pS.freezeGame();
                }
                else if (toggle == false)
                {
                    pS.entFreezeGame();
                }

            }
            if (SpawnEnemyPhase == true)
            {
                GamePhaseStartButton.SetActive(false);
                Hammer.SetActive(false);
                Gun.SetActive(true);
                SkillHuD.SetActive(true);
                playerSkill.enabled = true;
                GameObject.FindGameObjectWithTag("GameMaster").GetComponent<UpgradeTurret>().enabled = false;
                GameObject.Find("Player").GetComponent<ShootController>().enabled = true;
                BuildHud.SetActive(false);

                if (waitWave == false) StartCoroutine(SpawnWaves());

                if (GameObject.FindGameObjectWithTag("NPC") == null && waitWaves == true)
                {
                    GamePhaseStartButton.SetActive(true);
                    Hammer.SetActive(true);
                    Gun.SetActive(false);
                    SkillHuD.SetActive(false);
                    playerSkill.enabled = false;
                    GameObject.FindGameObjectWithTag("GameMaster").GetComponent<UpgradeTurret>().enabled = true;
                    GameObject.Find("Player").GetComponent<ShootController>().enabled = false;
                    BuildHud.SetActive(true);
                    SpawnEnemyPhase = false;
                    GPC.toogleScript();
                    toggle = false;
                    waitWave = false;
                    waitWaves = false;
                    waveCounter.text = ("Wave: ") + waveNumber.ToString();
                }

            }

        }
    }

    public void StarteWavePhase()
    {
        if (nPCMove.cantReach == false)
        {
            source.PlayOneShot(ckickSound);
            cantReachMessage.SetActive(false);
            SpawnEnemyPhase = true;
            Destroy(GameObject.Find("WalkAgent(Clone)"));
            waveCounter.text = ("Wave: ") + waveNumber.ToString();
            pS.entFreezeGame();
        }
        else if (nPCMove.cantReach == true)
        {
            source.PlayOneShot(deny);
            cantReachMessage.SetActive(true);
            GamePhaseStartButton.SetActive(true);
            toggle = false;
            pS.entFreezeGame();
            GPC.toogleScript();
        }
    }

    void SpawnWave()
    {
        waitWave = true;
        int count = 0;
        if (waveNumber <= 10)
        {
            temp = 0;
            while (EnemyEnergyMaxCost > temp)
            {
                pick = UnityEngine.Random.Range(0, enemyPrefab.Length);
                getCost = enemyPrefab[pick].GetComponent<NPCStats>().uebergabeEnemyEnergyCost;
                count = temp + getCost;
                if (EnemyEnergyMaxCost >= count)
                {
                    Instantiate(enemyPrefab[pick], SpawnPoint.position, SpawnPoint.rotation);
                    temp += getCost;
                }
            }
        }
        else if (waveNumber == 11)
        {
            Instantiate(enemyBoss, SpawnPoint.position, SpawnPoint.rotation);
        }
    }
    IEnumerator SpawnWaves()
    {

        for (int i = 0; i < (waveNumber * factor); i++)
        {
            if(waveNumber==11)
            {
                i = 456789;
            }
            SpawnWave();
            yield return new WaitForSeconds(10 - factor);
        }
        EnemyEnergyMaxCost += factor;
        waveNumber++;
        waitWaves = true;
    }
}

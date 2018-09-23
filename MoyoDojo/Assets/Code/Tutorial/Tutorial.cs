using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private WaveSpawner wS;
    private UpgradeTurret uT;
    private GroundPlacementController gPC;

    public GameObject welcomeText;
    public GameObject[] explainHudText;
    public GameObject[] placeTurretText;
    public GameObject upgradeTurretText;
    public GameObject upgradeTurretMenuText;
    public GameObject playGameText;
    public GameObject endTutorial;
    public GameObject ButtonBack;
    public GameObject PlayerHudText;
    public GameObject PlayerSkillText;
    public GameObject startGame;

    public static MainMenu instance;

    public enum MenuStates { welcome, explainHUD, placeTurret, UpgradeTurret, playGame , PlayerHud, endTutorial};
    public MenuStates currentState;

    public string newGameScene;
    public bool shift;
    public bool space;
    public bool w;
    public bool a;
    public bool s;
    public bool d;

    private void Awake()
    {
        currentState = MenuStates.welcome;
        wS = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<WaveSpawner>();
        uT = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<UpgradeTurret>();
        gPC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<GroundPlacementController>();
    }

    private void Update()
    {
        switch (currentState)
        {
            case MenuStates.welcome:
                welcomeText.SetActive(true);
                wS.enabled = false;
                uT.enabled = false;
                gPC.enabled = false;
                upgradeTurretText.SetActive(false);
                playGameText.SetActive(false);
                for (int i = 0; i < explainHudText.Length; i++)
                {
                    explainHudText[i].SetActive(false);
                }
                for (int i = 0; i < placeTurretText.Length; i++)
                {
                    placeTurretText[i].SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    w = true;
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    a = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    s = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    d = true;
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    space = true;
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    shift = true;
                }
                if (w == true && a == true && d == true && s == true && space == true && shift == true)
                {
                    currentState = MenuStates.explainHUD;
                }
                break; 

            case MenuStates.explainHUD:
                welcomeText.SetActive(false);
                for (int i = 0; i < explainHudText.Length; i++)
                {
                    explainHudText[i].SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    currentState = MenuStates.placeTurret;
                }
                break;

            case MenuStates.placeTurret:
                welcomeText.SetActive(false);
                for (int i = 0; i < explainHudText.Length; i++)
                {
                    explainHudText[i].SetActive(false);
                }
                for (int i = 0; i < placeTurretText.Length; i++)
                {
                    placeTurretText[i].SetActive(true);
                }
                gPC.enabled = true;
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    currentState = MenuStates.UpgradeTurret;
                }

                break;

            case MenuStates.UpgradeTurret:

                for (int i = 0; i < placeTurretText.Length; i++)
                {
                    placeTurretText[i].SetActive(false);
                }
                uT.enabled = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    upgradeTurretText.SetActive(false);
                    upgradeTurretMenuText.SetActive(true);
                }else if(uT.UpgradeMenu.activeSelf == false)
                {
                    upgradeTurretText.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    currentState = MenuStates.playGame;
                    upgradeTurretMenuText.SetActive(false);
                    upgradeTurretText.SetActive(false);
                }
                break;
            case MenuStates.playGame:
                wS.enabled = true;
                playGameText.SetActive(true);
                if(wS.Gun.activeSelf == true)
                {
                    playGameText.SetActive(false);
                    currentState = MenuStates.PlayerHud;
                }
                break;
            case MenuStates.PlayerHud:
                if (wS.Gun.activeSelf == true)
                {
                    Time.timeScale = 0f;
                    Camera_Control.sensHorizontal = 0f;
                    Camera_Control.sensVertical = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    PlayerHudText.SetActive(true);
                    PlayerSkillText.SetActive(true);
                    startGame.SetActive(true);
                    
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        currentState = MenuStates.endTutorial;
                    }
                }
                break;
            case MenuStates.endTutorial:
                PlayerHudText.SetActive(false);
                PlayerSkillText.SetActive(false);
                startGame.SetActive(false);
                Time.timeScale = 1f;
                Camera_Control.sensHorizontal = 8f;
                Camera_Control.sensVertical = 8f;
                Cursor.lockState = CursorLockMode.Locked;
                if (wS.Hammer.activeSelf == true)
                {
                    Time.timeScale = 0f;
                    Camera_Control.sensHorizontal = 0f;
                    Camera_Control.sensVertical = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    endTutorial.SetActive(true);
                    ButtonBack.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                    {
                        backToMenue();
                    }
                }
                break;
        }
        if(wS.Gun.activeSelf == true)
        {
            playGameText.SetActive(false);
        }
    }

    public void backToMenue()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        MainMenu.counter++;
    }
}

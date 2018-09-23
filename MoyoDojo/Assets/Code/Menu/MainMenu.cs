using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    private AudioSource source;
    private WaveSpawner wS;
    private GameObject[] temp;

    private int sureAbout;


    public AudioClip clickSound;
    public AudioClip sureAboutSound;
    public GameObject mainMenu;
    public GameObject OptionMenu;
    public GameObject Copyrigth;
    public GameObject Back;
    public GameObject TutorialCheck;
    public GameObject Difficulty;
    public GameObject sure;



    public static MainMenu instance;

    public enum MenuStates { MainMenu, Options, Copyrigth, Difficult };
    public MenuStates currentState;

    public static int counter = 0;
    public int difSpeicher;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        currentState = MenuStates.MainMenu;
        //temp = SceneManager.GetSceneByName("SampleScene").GetRootGameObjects();
        temp = SceneManager.GetSceneAt(0).GetRootGameObjects();

        if (temp.Length > 0 )
        {
            foreach (GameObject item in temp)
            {
                if (item.tag == "GameMaster")
                {
                    wS = item.GetComponent<WaveSpawner>();
                }
            }

        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case MenuStates.MainMenu:
                mainMenu.SetActive(true);
                OptionMenu.SetActive(false);
                Copyrigth.SetActive(false);
                Back.SetActive(false);
                Difficulty.SetActive(false);
                break;
            case MenuStates.Options:
                mainMenu.SetActive(false);
                OptionMenu.SetActive(true);
                Copyrigth.SetActive(false);
                Back.SetActive(false);
                Difficulty.SetActive(false);
                break;
            case MenuStates.Copyrigth:
                mainMenu.SetActive(false);
                OptionMenu.SetActive(false);
                Copyrigth.SetActive(true);
                Back.SetActive(true);
                Difficulty.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentState = MenuStates.MainMenu;
                }
                break;
             case MenuStates.Difficult:
                mainMenu.SetActive(false);
                TutorialCheck.SetActive(false);
                Difficulty.SetActive(true);
                break;
        }
    }

    public void OnStartGame() {
        source.PlayOneShot(clickSound);
        if(counter >= 1)
        {
            currentState = MenuStates.Difficult;

        }
        else
        {
            TutorialCheck.SetActive(true);
        }
        counter++;
    }

    public void onStartEasy()
    {
        source.PlayOneShot(clickSound);
        PlayerPrefs.SetInt("Dif", 0);
        sure.SetActive(false);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.Locked;
        TutorialCheck.SetActive(false);
    }
    public void onStartNormal()
    {
        sure.SetActive(false);
        source.PlayOneShot(clickSound);
        PlayerPrefs.SetInt("Dif", 1);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.Locked;
        TutorialCheck.SetActive(false);
    }
    public void onStartHard()
    {
        sure.SetActive(false);
        source.PlayOneShot(clickSound);
        PlayerPrefs.SetInt("Dif", 2);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.Locked;
        TutorialCheck.SetActive(false);
    }
    public void onStartImpossible()
    {
        if (sureAbout < 1)
        {
            sure.SetActive(true);
            source.PlayOneShot(sureAboutSound);
            sureAbout++;
        }
        else if (sureAbout >= 1)
        {
            source.PlayOneShot(clickSound);
            sure.SetActive(false);
            PlayerPrefs.SetInt("Dif", 3);
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            Cursor.lockState = CursorLockMode.Locked;
            TutorialCheck.SetActive(false);
        }
    }

    public void OnStartTutorial()
    {
        source.PlayOneShot(clickSound);
        PlayerPrefs.SetInt("Dif", 0);
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.Locked;
        TutorialCheck.SetActive(false);
        counter++;
    }

    public void OnOptions()
    {
        source.PlayOneShot(clickSound);
        Debug.Log("Clicked Options");
        currentState = MenuStates.Options;
        TutorialCheck.SetActive(false);
    }

    public void OnWindowsMode()
    {
        source.PlayOneShot(clickSound);
        Debug.Log("Cklicked Window Mode");
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void OnMainMenu()
    {
        source.PlayOneShot(clickSound);
        Debug.Log("Back");
        currentState = MenuStates.MainMenu;
    }

    public void QuitGame() {
        source.PlayOneShot(clickSound);
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OpenCopyright() {
        source.PlayOneShot(clickSound);
        Debug.Log("Copyright Scrollpannel open");
        currentState = MenuStates.Copyrigth;
        TutorialCheck.SetActive(false);
    }

    public void BacktoMenu()
    {
        source.PlayOneShot(clickSound);
        sure.SetActive(false);
        Debug.Log("Back to Menu");
        currentState = MenuStates.MainMenu;
    }
}

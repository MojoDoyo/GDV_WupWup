using UnityEngine;
using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseSkript : MonoBehaviour
{
    private AudioSource source;
    private ShootController sC;

    public GameObject pauseMenuUI;
    public AudioClip ckickSound;

    public static bool GameIsPaused = false;

    private void Awake()
    {
        GameIsPaused = false;
        source = GetComponent<AudioSource>();
        sC = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootController>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            source.PlayOneShot(ckickSound);
            sC.enabled = false;

            if (GameIsPaused)
            {
                Resume();
                entFreezeGame();
            }
            else
            {
                Pause();
                freezeGame();
            }
        }
    }

    public void Resume()
    {
        source.PlayOneShot(ckickSound);
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        sC.enabled = true;
        entFreezeGame();
    }

    void Pause()
    {
        source.PlayOneShot(ckickSound);
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void LoadMenu()
    {
        source.PlayOneShot(ckickSound);
        SceneManager.LoadScene("MainMenu");
        entFreezeGame();
        Cursor.lockState = CursorLockMode.None;
        

    }

    public void RestartGame()
    {
        source.PlayOneShot(ckickSound);
        SceneManager.LoadScene("SampleScene");
        GameIsPaused = false;
        entFreezeGame();
    }

    public void QuitGame()
    {
        source.PlayOneShot(ckickSound);
        Application.Quit();
    }

    public void freezeGame()
    {
        Time.timeScale = 0f;
        Camera_Control.sensHorizontal = 0f;
        Camera_Control.sensVertical = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void entFreezeGame()
    {
        Time.timeScale = 1f;
        Camera_Control.sensHorizontal = 8f;
        Camera_Control.sensVertical = 8f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}


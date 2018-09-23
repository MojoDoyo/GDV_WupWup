using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    private AudioSource source;

    public AudioClip ckickSound;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void RestartGame()
    {
        source.PlayOneShot(ckickSound);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        Debug.Log("Start");
    }

    public void BackToMenu()
    {
        source.PlayOneShot(ckickSound);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        MainMenu.instance.mainMenu.SetActive(true);
        Debug.Log("Back to Menu Screen");
    }
    
    public void QuitGame()
    {
        source.PlayOneShot(ckickSound);
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

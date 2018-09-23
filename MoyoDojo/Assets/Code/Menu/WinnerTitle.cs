using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerTitle : MonoBehaviour {

    private WaveSpawner wS;
    private MainMenu mM;
    private AudioSource source;
    private AudioSource dcsource;

    private bool clipDone;

    public GameObject winningTitle;
    public GameObject winningTitle2;
    public AudioClip jc;
    public AudioClip click;

    // Use this for initialization
    void Awake () {
        wS = this.GetComponent<WaveSpawner>();
        //mM = GameObject.Find("MenuScript").GetComponent<MainMenu>();
        source = GetComponent<AudioSource>();
        dcsource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		if(wS.waveNumber > 11)
        {
            Debug.Log(wS.waveNumber);
            if(wS.Hammer.activeSelf == true)
            {
                if (wS.DifficultyPick <= 2)
                {
                    winningTitle.SetActive(true);
                    Debug.Log("Winner winner turkey dinner");
                }
                else
                {
                    winningTitle2.SetActive(true);
                    Camera_Control.sensHorizontal = 0f;
                    Camera_Control.sensVertical = 0f;
                    Cursor.lockState = CursorLockMode.None;
                    wS.GamePhaseStartButton.SetActive(false);
                    wS.enabled = false;
                    if(clipDone == false)
                    {
                        dcsource.PlayOneShot(jc);
                        clipDone = true;
                    }
                }
            }
        }
	}

    public void MainMenue()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        source.PlayOneShot(click);
    }

    public void restartGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

        //mM.currentState = MainMenu.MenuStates.Difficult;
        source.PlayOneShot(click);
    }
}

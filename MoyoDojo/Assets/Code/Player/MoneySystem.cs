using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour {

    private AudioSource source;

    public Text moneyText;
    public GameObject message;
    public AudioClip deny;

    private float displayTime = 3f;

    public int money;

    bool displayMessage = false;

	// Use this for initialization
	void Start () {
        money = 2500;
        moneyText.text = money.ToString();
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update () {

        moneyText.text = money.ToString();

        if (displayMessage == true)
        {
            displayTime -= Time.deltaTime;
            if (displayTime <= 0.0)
            {
                message.SetActive(false);
                displayMessage = false;
            }
        }
    }
     
    public void AddMoney(int addMoney)
    {
        money += addMoney;
    }

   public bool decreaseMoney(int decreseMoney)
    {
        if (decreseMoney > money)
        {
            if(displayMessage == false)
            {
                source.PlayOneShot(deny);
                message.SetActive(true);
                displayTime = 3f;
                displayMessage = true;
            }
            return false;
        }
        else
        {
            money -= decreseMoney;
            return true;
        }
    }
}

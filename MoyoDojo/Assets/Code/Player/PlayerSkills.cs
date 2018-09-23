using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSkills : MonoBehaviour
{
    private Player_Movement pM;
    private ShootController sC;
    private AudioSource source;
    private GameObject[] turrets;
    private WaveSpawner wS;

    public Image Skill1Image;
    public Image Skill2Image;
    public Image Skill3Image;
    public Image Skill4Image;
    public Image Skill5Image;



    bool speedUpPlayer = false;
    bool speedUpBullet = false;
    bool dmg = false;
    bool slowDown = false;
    bool towerspeed = false;

    private void Start()
    {
        pM = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        sC = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootController>();
        wS = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<WaveSpawner>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Alpha1) || (Input.GetKeyDown(KeyCode.Keypad1))) && speedUpPlayer == false)
        {
            StartCoroutine(SKill1());

        }
        if ((Input.GetKeyDown(KeyCode.Alpha2) || (Input.GetKeyDown(KeyCode.Keypad2))) && speedUpBullet == false)
        {
            StartCoroutine(SKill2());
        }
        if ((Input.GetKeyDown(KeyCode.Alpha3) || (Input.GetKeyDown(KeyCode.Keypad3))) && dmg == false)
        {
            turrets = GameObject.FindGameObjectsWithTag("Tower");
            StartCoroutine(SKill3());
        }
        if ((Input.GetKeyDown(KeyCode.Alpha4) || (Input.GetKeyDown(KeyCode.Keypad4))) && slowDown == false)
        {
            if (GameObject.FindGameObjectsWithTag("Tower") != null)
            {
                turrets = GameObject.FindGameObjectsWithTag("Tower");
                StartCoroutine(SKill4());
            }
        }
        if ((Input.GetKeyDown(KeyCode.Alpha5) || (Input.GetKeyDown(KeyCode.Keypad5))) && towerspeed == false)
        {
            if (GameObject.FindGameObjectsWithTag("Tower") != null)
            {
                turrets = GameObject.FindGameObjectsWithTag("Tower");
                StartCoroutine(SKill5());
            }
        }
    }

    IEnumerator SKill1()
    {
        Skill1Image.color = new Color32(62, 150, 202, 114);
        speedUpPlayer = true;
        pM.speed += 5;
        yield return new WaitForSeconds(5);
        Skill1Image.color = new Color32(202, 71, 62, 114);
        pM.speed -= 5;
        yield return new WaitForSeconds(60);
        Skill1Image.color = new Color32(62, 202, 75, 114);
        speedUpPlayer = false;
    }

    IEnumerator SKill2()
    {
        Skill2Image.color = new Color32(62, 150, 202, 114);
        speedUpBullet = true;
        sC.fireRate -= 0.3f;
        yield return new WaitForSeconds(5);
        Skill2Image.color = new Color32(202, 71, 62, 114);
        sC.fireRate += 0.3f;
        yield return new WaitForSeconds(60);
        Skill2Image.color = new Color32(62, 202, 75, 114);
        speedUpBullet = false;
    }

    IEnumerator SKill3()
    {
        Skill3Image.color = new Color32(62, 150, 202, 114);
        dmg = true;
        sC.bulletDmg += 20;
        foreach (var item in turrets)
        {
            item.GetComponent<Turret>().BulletDmg += 20;
        }
        yield return new WaitForSeconds(5);
        Skill3Image.color = new Color32(202, 71, 62, 114);
        sC.bulletDmg -= 20;
        foreach (var item in turrets)
        {
            item.GetComponent<Turret>().BulletDmg -= 20;
        }
        yield return new WaitForSeconds(90);
        Skill3Image.color = new Color32(62, 202, 75, 114);
        dmg = false;
    }

    IEnumerator SKill4()
    {
        Skill4Image.color = new Color32(62, 150, 202, 114);
        slowDown = true;
        Time.timeScale = 0.5f;
        sC.bulletSpeed *= 2;
        foreach (var item in turrets)
        {
            item.GetComponent<Turret>().fireRate /= 2;
        }
        pM.speed *= 2;
        yield return new WaitForSeconds(5);
        Skill4Image.color = new Color32(202, 71, 62, 114);
        sC.bulletSpeed /= 2;
        foreach (var item in turrets)
        {
            item.GetComponent<Turret>().fireRate *= 2;
        }
        pM.speed /= 2;
        Time.timeScale = 1f;
        yield return new WaitForSeconds(90);
        Skill4Image.color = new Color32(62, 202, 75, 114);
        slowDown = false;
    }

    IEnumerator SKill5()
    {
        Skill5Image.color = new Color32(62, 150, 202, 114);
        towerspeed = true;
        foreach (var item in turrets)
        {
            item.GetComponent<Turret>().fireRate /= 2;
        }
        yield return new WaitForSeconds(5);
        Skill5Image.color = new Color32(202, 71, 62, 114);
        foreach (var item in turrets)
        {
            item.GetComponent<Turret>().fireRate *= 2;
        }
        yield return new WaitForSeconds(60);
        Skill5Image.color = new Color32(62, 202, 75, 114);
        towerspeed = false;
    }


}

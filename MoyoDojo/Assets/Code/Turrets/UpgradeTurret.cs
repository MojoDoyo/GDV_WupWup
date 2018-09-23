using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeTurret : MonoBehaviour
{
    private Turret tUpgrade = null;
    private TowerBaseStats tBase = null;
    private PauseSkript pS;
    private MoneySystem mS;
    private WaveSpawner wS;
    private GroundPlacementController gPC;
    private AudioSource source;

    private float displayTimeMax = 2f;
    private float displayTimeCost = 2f;

    public LayerMask mask, maskTowerBase;

    public GameObject UpgradeMenu;
    public GameObject TowerStatsMenu;
    public GameObject CantUpgradeMaxmessage;
    public GameObject CantUpgradeCostmessage;
    public GameObject Sell;
    public GameObject SellTowerFirst;
    public Text UpdateRadius;
    public Text UpdateDamge;
    public Text UpdateSpeed;
    public Text UpdateRadiusCost;
    public Text UpdateDamgeCost;
    public Text UpdateSpeedCost;
    public Text UpgradeSpeedMax;
    public Text UpdateDamgeMax;
    public Text UpdateRadiusMax;
    public Text UpdateBack;
    public AudioClip turretUpgrade;
    public AudioClip deny;
    public AudioClip click;

    bool displayMessageMax = false;
    bool displayMessageCost = false;

    private void Awake()
    {
        pS = GameObject.FindGameObjectWithTag("UI").GetComponent<PauseSkript>();
        mS = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<MoneySystem>();
        wS = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<WaveSpawner>();
        gPC = Camera.main.GetComponent<GroundPlacementController>();
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (getTowerBase(out tBase))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (tBase != null)
                {
                    wS.GamePhaseStartButton.SetActive(false);
                    Sell.SetActive(true);
                    pS.freezeGame();
                }
            }
        }
        else if (getTurret(out tUpgrade))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (tUpgrade != null)
                {
                    UpgradeMenu.SetActive(true);
                    TowerStatsMenu.SetActive(true);
                    pS.freezeGame();
                    UpdateSpeed.text = tUpgrade.fireRate.ToString();
                    UpdateRadius.text = tUpgrade.range.ToString();
                    UpdateDamge.text = tUpgrade.BulletDmg.ToString();

                    UpdateSpeedCost.text = tUpgrade.BulletSpeedCost.ToString();
                    UpdateRadiusCost.text = tUpgrade.RangeCost.ToString();
                    UpdateDamgeCost.text = tUpgrade.BullDmgCost.ToString();

                    UpgradeSpeedMax.text = (tUpgrade.maxBulletSpeedUpgrade).ToString();
                    UpdateDamgeMax.text = (tUpgrade.maxBullDmgUpgrade).ToString();
                    UpdateRadiusMax.text = (tUpgrade.maxRangeUpgrade).ToString();
                }
            }
        }

        if (displayMessageMax == true)
        {
            displayTimeMax -= Time.deltaTime;
            if (displayTimeMax <= 0.0)
            {
                CantUpgradeMaxmessage.SetActive(false);
                displayMessageMax = false;
            }
        }

        if (displayMessageCost == true)
        {
            displayTimeCost -= Time.deltaTime;
            if (displayTimeCost <= 0.0)
            {
                CantUpgradeCostmessage.SetActive(false);
                displayMessageCost = false;
            }
        }
    }
    #region getRays
    bool getTurret(out Turret refObject)
    {
        Vector3 playerPosition = Camera.main.transform.position;
        Vector3 forwardDirection = Camera.main.transform.forward;

        Ray interactionRay = new Ray(playerPosition, forwardDirection);
        RaycastHit hitInfo;
        if (Physics.Raycast(interactionRay, out hitInfo, Mathf.Infinity, mask))
        {
            refObject = hitInfo.rigidbody.GetComponent<Turret>();
            return true;
        }
        refObject = null;
        return false;
    }

    bool getTowerBase(out TowerBaseStats refObject)
    {
        Vector3 playerPosition = Camera.main.transform.position;
        Vector3 forwardDirection = Camera.main.transform.forward;

        Ray interactionRay = new Ray(playerPosition, forwardDirection);
        RaycastHit hitInfo;
        if (Physics.Raycast(interactionRay, out hitInfo, Mathf.Infinity, maskTowerBase))
        {
            refObject = hitInfo.collider.GetComponent<TowerBaseStats>();
            return true;
        }
        refObject = null;
        return false;
    }
    #endregion

    #region Upgrade
    public void SpeedUpgrade()
    {
        if (tUpgrade.maxBulletSpeedUpgrade > 0)
        {
            if (mS.decreaseMoney(tUpgrade.BulletSpeedCost))
            {
                tUpgrade.fireRate = tUpgrade.fireRate - 0.5f;
                UpdateSpeed.text = tUpgrade.fireRate.ToString();
                UpdateSpeedCost.text = tUpgrade.BulletSpeedCost.ToString();
                UpgradeSpeedMax.text = (tUpgrade.maxBulletSpeedUpgrade - 1f).ToString();
                tUpgrade.maxBulletSpeedUpgrade -= 1;
                source.PlayOneShot(turretUpgrade);
                MessageReset();

            }
            else CantUpgradeCost();
        }
        else
        {
            CantUpgradeMax();
        }
    }


    public void RadiusUpgrade()
    {
        if (tUpgrade.maxRangeUpgrade > 0)
        {
            if (mS.decreaseMoney(tUpgrade.rangeCost))
            {
                tUpgrade.range = tUpgrade.range + 1f;
                UpdateRadius.text = tUpgrade.range.ToString();
                UpdateRadiusCost.text = tUpgrade.RangeCost.ToString();
                UpdateRadiusMax.text = (tUpgrade.maxRangeUpgrade - 1).ToString();
                tUpgrade.maxRangeUpgrade -= 1;
                source.PlayOneShot(turretUpgrade);
                MessageReset();
            }
            else CantUpgradeCost();
        }
        else
        {
            CantUpgradeMax();
        }
    }

    public void DmgUpgrade()
    {
        if (tUpgrade.maxBullDmgUpgrade > 0)
        {
            if (mS.decreaseMoney(tUpgrade.BullDmgCost))
            {
                tUpgrade.BulletDmg = tUpgrade.BulletDmg + 5;
                UpdateDamge.text = tUpgrade.BulletDmg.ToString();
                UpdateDamgeCost.text = tUpgrade.BullDmgCost.ToString();
                UpdateDamgeMax.text = (tUpgrade.maxBullDmgUpgrade - 1).ToString();
                tUpgrade.maxBullDmgUpgrade -= 1;
                source.PlayOneShot(turretUpgrade);
                MessageReset();
            }
            else CantUpgradeCost();
        }
        else
        {
            CantUpgradeMax();
        }
    }
    #endregion

    public void Back()
    {
        source.PlayOneShot(click);
        UpgradeMenu.SetActive(false);
        TowerStatsMenu.SetActive(false);
        wS.GamePhaseStartButton.SetActive(true);
        Sell.SetActive(false);
        MessageReset();
        pS.entFreezeGame();
        SellTowerFirst.SetActive(false);
    }

    #region CantUpgrade
    public void CantUpgradeMax()
    {
        if (displayMessageMax == false)
        {
            source.PlayOneShot(deny);
            CantUpgradeMaxmessage.SetActive(true);
            displayTimeMax = 2f;
            displayMessageMax = true;
        }
    }

    public void CantUpgradeCost()
    {
        if (displayMessageCost == false)
        {
            source.PlayOneShot(deny);
            CantUpgradeCostmessage.SetActive(true);
            mS.message.SetActive(false);
            displayTimeCost = 2f;
            displayMessageCost = true;
        }
    }
    #endregion

    public void MessageReset()
    {
        displayTimeCost = -1f;
        displayTimeMax = -1f;
        CantUpgradeMaxmessage.SetActive(false);
        CantUpgradeCostmessage.SetActive(false);
    }

    public void SellObjects()
    {
        if (tBase != null )
        {
            source.PlayOneShot(click);
            if (gPC.UsedSpace(0, tBase.gameObject))
            {
                Destroy(tBase.gameObject);
                mS.AddMoney((int)(tBase.cost * 0.7));
                Sell.SetActive(false);
                wS.GamePhaseStartButton.SetActive(true);
                pS.entFreezeGame();
            }
            else
            {
                SellTowerFirst.SetActive(true);
            }
        }
        else if (tUpgrade != null)
        {
            source.PlayOneShot(click);
            if (gPC.UsedSpace(1, tUpgrade.gameObject))
            {
                SellTowerFirst.SetActive(false);
                Destroy(tUpgrade.gameObject);
                mS.AddMoney((int)(tUpgrade.cost * 0.7));
                UpgradeMenu.SetActive(false);
                pS.entFreezeGame();
            }

        }
    }
}

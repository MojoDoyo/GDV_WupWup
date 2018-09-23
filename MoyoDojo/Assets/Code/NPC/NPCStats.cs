using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class NPCStats : MonoBehaviour {

    private GameObject MoneySystem;

    MoneySystem money;
    
    public GameObject Rotate;
    public Slider healthslider;


    public int startingHealth = 100;
    public int EnemyEnergyCost;
    public int currentHealth;
    public int moneyForDestroy;



    bool isDead;                                                // Whether the npc is dead.

    void Awake()
    {
        // Set the initial health of the npc.
        currentHealth = startingHealth;
        MoneySystem = GameObject.Find("GameMaster");
        money = MoneySystem.GetComponent<MoneySystem>();
        healthslider.maxValue = startingHealth;
        healthslider.value = startingHealth;
    }

    private void Update()
    {
        Rotate.transform.LookAt(Camera.main.transform);

    }


    public void TakeDamage(int amount)
    {

        // Reduce the current health by the damage amount.
        currentHealth -= amount - 2;

        healthslider.value = currentHealth;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        Destroy(gameObject);
        money.AddMoney(moneyForDestroy);

    }

    public int uebergabeEnemyEnergyCost
    {
        get { return EnemyEnergyCost; }
    }
}

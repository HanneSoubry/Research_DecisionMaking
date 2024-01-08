using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerCharacter : MonoBehaviour
{
    public enum BehaviorType
    {
        TestBehavior,
        Input,
        BehaviorTree,
        UtilityAI,
        GOAP
    }

    [Serializable] public struct PlayerStats
    {
        // stats
        public int health;
        public int energy;

        // display
        [HideInInspector] public TMP_Text healthText;
        [HideInInspector] public TMP_Text energyText;
        [HideInInspector] public TMP_Text boostText;

        // damage
        [HideInInspector] public int pendingAttackDamage;
        [HideInInspector] public int boostTurnsLeft;

        public bool IsBoostActive
        {
            get { return boostTurnsLeft > 0; }
        }
    }

    // Behavior
    [SerializeField] private BehaviorType behaviorType;
    private PlayerBehavior behavior = null;
    private PlayerStats stats = new PlayerStats();

    // Display
    [SerializeField] private TMP_Text healthText = null;
    [SerializeField] private TMP_Text energyText = null;
    [SerializeField] private TMP_Text boostText = null;

    public void Initialize()
    {
        stats.health = CommonData.instance.MaxHealth;
        stats.energy = 0;

        stats.pendingAttackDamage = 0;
        stats.boostTurnsLeft = 0;

        if(healthText == null || energyText == null || boostText == null)
        {
            Debug.LogError("Player stat texts not assigned");
        }

        stats.healthText = healthText;
        stats.energyText = energyText;
        stats.boostText = boostText;

        switch(behaviorType)
        {
            case BehaviorType.TestBehavior:
                {
                    behavior = new BehaviorRandom(); 
                    FileWriter.instance.WriteToFile("random behavior");
                    break;
                }
            case BehaviorType.Input:
                {
                    behavior = new BehaviorInput();
                    FileWriter.instance.WriteToFile("input from player");
                    break;
                }
            case BehaviorType.BehaviorTree:
                {
                    behavior = new BehaviorBehaviorTree();
                    FileWriter.instance.WriteToFile("Behavior Tree");
                    break;
                }
        }

        behavior.Initialize();

        UpdateStatsDisplay();
    }

    public int MakeMove()
    {
        if(stats.energy < CommonData.instance.MaxEnergy)
        {
            ++stats.energy;
            UpdateStatsDisplay();         
        }

        FileWriter.instance.WriteToFile($"energy {stats.energy}\n");

        stats.pendingAttackDamage = 0;
        behavior.MakeMove(ref stats);
        UpdateStatsDisplay();

        // return damage to other player (gamemanager will apply this to the other player)
        return stats.pendingAttackDamage;
    }

    public bool TakeDamage(int damage)
    {
        stats.health -= damage;

        if(stats.health < 0)
        {
            stats.health = 0;
            UpdateStatsDisplay();

            // died
            return true;
        }

        UpdateStatsDisplay();
        return false;
    }

    public void WriteStatsToFile()
    {
        FileWriter.instance.WriteToFile($"HP: {stats.health}, Energy: {stats.energy}, Boost: {stats.IsBoostActive}");
    }

    private void UpdateStatsDisplay()
    {
        healthText.text = stats.health.ToString();
        energyText.text = stats.energy.ToString();
        boostText.text = stats.boostTurnsLeft.ToString();
    }
}

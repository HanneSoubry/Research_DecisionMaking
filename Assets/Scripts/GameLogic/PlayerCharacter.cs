using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;
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
        [HideInInspector] public Image healthImage;
        [HideInInspector] public Image energyImage;
        [HideInInspector] public Image boostImage;

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
    public PlayerStats Stats { get { return stats; } }

    // Display
    [SerializeField] private Image healthImage = null;
    [SerializeField] private Image energyImage = null;
    [SerializeField] private Image boostImage = null;
    private static string emptyText = "";

    public void Initialize()
    {
        stats.health = CommonData.instance.MaxHealth;
        stats.energy = 0;

        stats.pendingAttackDamage = 0;
        stats.boostTurnsLeft = 0;

        if(healthImage == null || energyImage == null || boostImage == null)
        {
            Debug.LogError("Player stat texts not assigned");
        }

        stats.healthImage = healthImage;
        stats.energyImage = energyImage;
        stats.boostImage = boostImage;

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
            case BehaviorType.UtilityAI:
                {
                    behavior = new BehaviorUtilityAI();
                    FileWriter.instance.WriteToFile("Utility AI");
                    break;
                }
        }

        behavior.Initialize();

        UpdateStatsDisplay();
    }

    public void RechargeEnergy()
    {
        if (stats.energy < CommonData.instance.MaxEnergy)
        {
            ++stats.energy;
            UpdateStatsDisplay();
        }

        FileWriter.instance.WriteToFile($"energy {stats.energy}\n");
        CommonData.instance.actionText.text = emptyText;
    }

    public int MakeMove()
    {
        stats.pendingAttackDamage = 0;
        behavior.MakeMove(ref stats);
        UpdateStatsDisplay();

        // return damage to other player (gamemanager will apply this to the other player)
        return stats.pendingAttackDamage;
    }

    public bool TakeDamage(int damage)
    {
        stats.health -= damage;

        if(stats.health <= 0)
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
        CommonData data = CommonData.instance;
        healthImage.fillAmount = (float)stats.health / (float)data.MaxHealth;
        energyImage.fillAmount = (float)stats.energy / (float)data.MaxEnergy;
        boostImage.fillAmount = (float)stats.boostTurnsLeft / (float)data.BoostDuration;
    }
}

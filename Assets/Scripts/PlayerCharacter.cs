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
        BinaryTree,
        UtilityAI,
        GOAP
    }

    [Serializable] public struct PlayerStats
    {
        // stats
        public int health;
        public int maxHealth;
        public int energy;
        public int maxEnergy;

        // display
        [HideInInspector] public TMP_Text healthText;
        [HideInInspector] public TMP_Text energyText;
        [HideInInspector] public TMP_Text boostText;

        // damage
        [HideInInspector] public int pendingAttackDamage;
        public int boostDuration;
        public int attackBoostValue;
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

    public void Initialize(PlayerStats initialStats)
    {
        stats = initialStats;

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
                    break;
                }
            case BehaviorType.Input:
                {
                    behavior = new BehaviorInput();
                    break;
                }
        }

        UpdateStatsDisplay();
    }

    public int MakeMove()
    {
        if(stats.energy < stats.maxEnergy)
        {
            ++stats.energy;
            UpdateStatsDisplay();
        }

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

    private void UpdateStatsDisplay()
    {
        healthText.text = stats.health.ToString();
        energyText.text = stats.energy.ToString();
        boostText.text = stats.boostTurnsLeft.ToString();
    }
}

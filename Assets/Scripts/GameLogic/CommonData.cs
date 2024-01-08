using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonData : MonoBehaviour
{
    // Singleton
    public static CommonData instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // DATA
    [SerializeField] private int maxHealth = 40;
    [SerializeField] private int maxEnergy = 3;
    [SerializeField] private int boostDuration = 3;
    [SerializeField] private int boostValue = 2;
    [SerializeField] private int basicAttackDamage = 3;
    [SerializeField] private int heavyAttackDamage = 7;
    [SerializeField] private int healingBonus = 5;
    [SerializeField] private int energyForPowerUp = 1;
    [SerializeField] private int energyForHeavyAttack = 2;
    [SerializeField] private int energyForHeal = 3;

    public int MaxHealth { get { return maxHealth; } }
    public int MaxEnergy { get { return maxEnergy; } }
    public int BoostDuration { get { return boostDuration; } }
    public int BoostValue { get { return boostValue; } }
    public int BasicAttackDamage { get { return basicAttackDamage; } }
    public int HeavyAttackDamage { get { return heavyAttackDamage; } }
    public int HealingBonus { get { return healingBonus; } }
    public int EnergyForPowerUp { get { return energyForPowerUp; } }
    public int EnergyForHeavyAttack { get { return energyForHeavyAttack; } }
    public int EnergyForHeal { get { return energyForHeal; } }


}

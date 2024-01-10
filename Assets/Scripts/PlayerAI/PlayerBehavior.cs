using System.Collections;
using System.Collections.Generic;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerBehavior
{
    public enum Action
    {
        None,
        BasicAttack,
        PowerUp,
        HeavyAttack,
        Heal
    }

    protected Action chosenAction = Action.None;

    public virtual void Initialize() {}

    public virtual void MakeMove(ref PlayerCharacter.PlayerStats stats)
    {
        Debug.LogWarning("Base Class Move, should be overridden");
    }

    // TODO: Handle action display
    protected void BasicAttack(ref PlayerCharacter.PlayerStats stats)
    {
        CommonData data = CommonData.instance;
        FileWriter.instance.WriteToFile("Basic Attack\n");
        if (stats.IsBoostActive)
        {
            stats.pendingAttackDamage = data.BoostedBasicAttackDamage;
            FileWriter.instance.WriteToFile($"Doing {data.BoostedBasicAttackDamage} damage\n");
        }
        else
        {
            stats.pendingAttackDamage = data.BasicAttackDamage;
            FileWriter.instance.WriteToFile($"Doing {data.BasicAttackDamage} damage\n");
        }

        if (stats.IsBoostActive)
        {
            // decrease turns left
            --stats.boostTurnsLeft;
        }
    }

    protected void PowerUp(ref PlayerCharacter.PlayerStats stats)
    {
        CommonData data = CommonData.instance;
        if (stats.energy < data.EnergyForPowerUp)
        {
            Debug.Log($"Trying to execute power up, requires {data.EnergyForPowerUp} energy, but has only {stats.energy} available");
            return;
        }

        FileWriter.instance.WriteToFile("Power Up\n");
        stats.boostTurnsLeft = data.BoostDuration; 
        stats.energy -= data.EnergyForPowerUp;
    }

    protected void HeavyAttack(ref PlayerCharacter.PlayerStats stats)
    {
        CommonData data = CommonData.instance;
        if (stats.energy < data.EnergyForHeavyAttack)
        {
            Debug.Log($"Trying to execute heavy attack, requires {data.EnergyForHeavyAttack} energy, but has only {stats.energy} available");
            return;
        }

        FileWriter.instance.WriteToFile("Heavy Attack\n");
        if (stats.IsBoostActive)
        {
            stats.pendingAttackDamage = data.BoostedHeavyAttackDamage;
            FileWriter.instance.WriteToFile($"Doing {data.BoostedHeavyAttackDamage} damage\n");
        }
        else
        {
            stats.pendingAttackDamage = data.HeavyAttackDamage;
            FileWriter.instance.WriteToFile($"Doing {data.HeavyAttackDamage} damage\n");
        }

        stats.energy -= CommonData.instance.EnergyForHeavyAttack;

        if (stats.IsBoostActive)
        {
            // decrease turns left
            --stats.boostTurnsLeft;
        }
    }

    protected void Heal(ref PlayerCharacter.PlayerStats stats)
    {
        CommonData data = CommonData.instance;
        if (stats.energy < data.EnergyForHeal)
        {
            Debug.Log($"Trying to execute heal, requires {data.EnergyForHeal} energy, but has only {stats.energy} available");
            return;
        }

        FileWriter.instance.WriteToFile("Heal\n");
        stats.health += data.HealingBonus;
        if(stats.health > data.MaxHealth)
        {
            stats.health = data.MaxHealth;
        }

        stats.energy -= data.EnergyForHeal;

        if (stats.IsBoostActive)
        {
            // decrease turns left
            --stats.boostTurnsLeft;
        }
    }
}

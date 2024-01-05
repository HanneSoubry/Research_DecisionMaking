using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerBehavior
{
    protected int energyForPowerUp = 1;
    protected int energyForHeavyAttack = 2;
    protected int energyForHeal = 3;

    public virtual void MakeMove(ref PlayerCharacter.PlayerStats stats)
    {
        Debug.LogWarning("Base Class Move, should be overridden");
    }

    // TODO: Handle action display
    protected void BasicAttack(ref PlayerCharacter.PlayerStats stats)
    {
        FileWriter.instance.WriteToFile("Basic Attack\n");
        if (stats.IsBoostActive)
        {
            stats.pendingAttackDamage = 3 + stats.attackBoostValue;
            FileWriter.instance.WriteToFile($"Doing {3 + stats.attackBoostValue} damage\n");
        }
        else
        {
            stats.pendingAttackDamage = 3;
            FileWriter.instance.WriteToFile($"Doing 3 damage\n");
        }

        if (stats.IsBoostActive)
        {
            // decrease turns left
            --stats.boostTurnsLeft;
        }
    }

    protected void PowerUp(ref PlayerCharacter.PlayerStats stats)
    {
        if (stats.energy <  energyForPowerUp)
        {
            Debug.Log($"Trying to execute power up, requires {energyForPowerUp} energy, but has only {stats.energy} available");
            return;
        }

        FileWriter.instance.WriteToFile("Power Up\n");
        stats.boostTurnsLeft = stats.boostDuration; 
        stats.energy -= energyForPowerUp;
    }

    protected void HeavyAttack(ref PlayerCharacter.PlayerStats stats)
    {
        if (stats.energy < energyForHeavyAttack)
        {
            Debug.Log($"Trying to execute heavy attack, requires {energyForHeavyAttack} energy, but has only {stats.energy} available");
            return;
        }

        FileWriter.instance.WriteToFile("Heavy Attack\n");
        if (stats.IsBoostActive)
        {
            stats.pendingAttackDamage = 7 + stats.attackBoostValue;
            FileWriter.instance.WriteToFile($"Doing {7 + stats.attackBoostValue} damage\n");
        }
        else
        {
            stats.pendingAttackDamage = 7;
            FileWriter.instance.WriteToFile($"Doing 7 damage\n");
        }

        stats.energy -= energyForHeavyAttack;

        if (stats.IsBoostActive)
        {
            // decrease turns left
            --stats.boostTurnsLeft;
        }
    }

    protected void Heal(ref PlayerCharacter.PlayerStats stats)
    {
        if (stats.energy < energyForHeal)
        {
            Debug.Log($"Trying to execute heal, requires {energyForHeal} energy, but has only {stats.energy} available");
            return;
        }

        FileWriter.instance.WriteToFile("Heal\n");
        stats.health += 5;
        if(stats.health < stats.maxHealth)
        {
            stats.health = stats.maxHealth;
        }

        stats.energy -= energyForHeal;

        if (stats.IsBoostActive)
        {
            // decrease turns left
            --stats.boostTurnsLeft;
        }
    }
}

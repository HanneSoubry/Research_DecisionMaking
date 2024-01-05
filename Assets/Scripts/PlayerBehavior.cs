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
        Debug.Log("Excecuting Basic Attack");
        if (stats.IsBoostActive)
        {
            stats.pendingAttackDamage = 3 + stats.attackBoostValue;
        }
        else
        {
            stats.pendingAttackDamage = 3;
        }
    }

    protected void PowerUp(ref PlayerCharacter.PlayerStats stats)
    {
        if (stats.energy <  energyForPowerUp)
        {
            Debug.Log($"Trying to execute power up, requires {energyForPowerUp} energy, but has only {stats.energy} available");
            return;
        }

        Debug.Log("Excecuting Power Up");
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

        Debug.Log("Excecuting Heavy Attack");
        if (stats.IsBoostActive)
        {
            stats.pendingAttackDamage = 7 + stats.attackBoostValue;
        }
        else
        {
            stats.pendingAttackDamage = 7;
        }

        stats.energy -= energyForHeavyAttack;
    }

    protected void Heal(ref PlayerCharacter.PlayerStats stats)
    {
        if (stats.energy < energyForHeal)
        {
            Debug.Log($"Trying to execute heal, requires {energyForHeal} energy, but has only {stats.energy} available");
            return;
        }

        Debug.Log("Excecuting Heal");
        stats.health += 5;
        if(stats.health < stats.maxHealth)
        {
            stats.health = stats.maxHealth;
        }

        stats.energy -= energyForHeal;
    }
}

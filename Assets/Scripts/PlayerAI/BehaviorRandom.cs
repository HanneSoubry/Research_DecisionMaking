using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BehaviorRandom : PlayerBehavior
{
    public override void MakeMove(ref PlayerCharacter.PlayerStats stats)
    {
        // TEST BEHAVIOR
        int requiredEnery = 0;
        int action;
        do
        {
            action = Random.Range(0, 4);
            switch (action)
            {
                case 0:
                    {
                        requiredEnery = 0;
                        break;
                    }
                case 1:
                    {
                        requiredEnery = CommonData.instance.EnergyForPowerUp;
                        break;
                    }
                case 2:
                    {
                        requiredEnery = CommonData.instance.EnergyForHeavyAttack;
                        break;
                    }
                case 3:
                    {
                        requiredEnery = CommonData.instance.EnergyForHeal;
                        break;
                    }
            }
        } while (stats.energy < requiredEnery);

        switch (action)
        {
            case 0:
                {
                    BasicAttack(ref stats);
                    break;
                }
            case 1:
                {
                    PowerUp(ref stats);
                    break;
                }
            case 2:
                {
                    HeavyAttack(ref stats);
                    break;
                }
            case 3:
                {
                    Heal(ref stats);
                    break;
                }
        }
    }
}
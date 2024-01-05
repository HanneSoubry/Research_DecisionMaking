using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorRandom : PlayerBehavior
{
    public override void MakeMove(ref PlayerCharacter.PlayerStats stats)
    {
        // TEST BEHAVIOR
        int requiredEnery = 0;
        int action = 0;
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
                        requiredEnery = energyForPowerUp;
                        break;
                    }
                case 2:
                    {
                        requiredEnery = energyForHeavyAttack;
                        break;
                    }
                case 3:
                    {
                        requiredEnery = energyForHeal;
                        break;
                    }
            }
        } while (stats.energy <= requiredEnery);

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
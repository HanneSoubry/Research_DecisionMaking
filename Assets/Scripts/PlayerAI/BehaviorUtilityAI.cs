using UtilityAI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BehaviorUtilityAI : PlayerBehavior
{
    private UtilityAIBrain utilityAI = null;

    public override void Initialize()
    {
        // SET UP HERE
        utilityAI = new UtilityAIBrain();
        List<UtilityAction> actions = new List<UtilityAction>();

        actions.Add(new UtilityAction(
            PlayerBehavior.Action.BasicAttack,
            new List<Condition> { },   // no requirements
            new List<Consideration> { new ConsiderationBasicAttackValue() }  
            ));

        actions.Add(new UtilityAction(
            PlayerBehavior.Action.PowerUp,
            new List<Condition> { new ConditionMinimumEnergy(CommonData.instance.EnergyForPowerUp) },
            new List<Consideration> { new ConsiderationPowerUpValue() }
            ));

        actions.Add(new UtilityAction(
            PlayerBehavior.Action.HeavyAttack,
            new List<Condition> { new ConditionMinimumEnergy(CommonData.instance.EnergyForHeavyAttack) },
            new List<Consideration> { new ConsiderationHeavyAttackValue() }
            ));

        actions.Add(new UtilityAction(
            PlayerBehavior.Action.Heal,
            new List<Condition> { new ConditionMinimumEnergy(CommonData.instance.EnergyForHeal),
                                  new ConditionNotFullHealth() },  // do not allow to heal if full health
            new List<Consideration> { new ConsiderationHealValue() }
            ));

        utilityAI.SetActions(actions);
    }

    public override void MakeMove(ref PlayerCharacter.PlayerStats stats)
    {
        chosenAction = Action.None;

        chosenAction = utilityAI.ChooseAction();

        switch (chosenAction)
        {
            case Action.BasicAttack:
                {
                    BasicAttack(ref stats);
                    break;
                }
            case Action.PowerUp:
                {
                    PowerUp(ref stats);
                    break;
                }
            case Action.HeavyAttack:
                {
                    HeavyAttack(ref stats);
                    break;
                }
            case Action.Heal:
                {
                    Heal(ref stats);
                    break;
                }
            default:
                {
                    Debug.LogWarning("Utility AI has not chosen any action");
                    break;
                }
        }
    }
}
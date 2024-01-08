using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BehaviorBehaviorTree : PlayerBehavior
{
    private BehaviorTree.Tree behaviorTree;

    public override void Initialize()
    {
        // SET UP TREE HERE
        behaviorTree = new BehaviorTree.Tree();

        Node rootNode = new Selector(new List<Node>
        {
            // 1) if need healing and can heal 
            new BehaviorTree.Sequence(new List<Node>
            {
                new NodeCheckNeedHealing(),
                new NodeCheckCanHeal(),
                new NodeChooseHeal()
            }),

            // 2) (if low health, save energy) if not boosted and can boost
            new BehaviorTree.Sequence(new List<Node>
            {
                new DecoratorInvert(new NodeCheckLowHealth()),  
                new DecoratorInvert(new NodeCheckBoostActive()),
                new NodeCheckCanBoost(),
                new NodeChoosePowerUp()
            }),

            // 3) (if low health, save energy) if can do heavy attack
            new BehaviorTree.Sequence(new List<Node>
            {
                new DecoratorInvert(new NodeCheckLowHealth()),
                new NodeCheckCanHeavyAttack(),
                new NodeChooseHeavyAttack()
            }),

            // 4) always available backup plan: basic attack
            new NodeChooseBasicAttack()
        });

        behaviorTree.SetTree(rootNode);
    }

    public override void MakeMove(ref PlayerCharacter.PlayerStats stats)
    {
        chosenAction = Action.None;

        chosenAction = behaviorTree.ChooseAction(ref stats);

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
                    Debug.LogWarning("Behavior Tree has not chosen any action");
                    break;
                }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class NodeCheckNeedHealing : Node
    {
        public NodeCheckNeedHealing() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            if(stats.health < CommonData.instance.MaxHealth - CommonData.instance.HealingBonus)  // full healing effect
            {
                // Need healing? YES
                state = NodeState.Succes;
            }
            else
            {
                // Need healing? NO
                state = NodeState.Failure;
            }
            return state;
        }
    }

    public class NodeCheckCanHeal : Node
    {
        public NodeCheckCanHeal() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            if (stats.energy >= CommonData.instance.EnergyForHeal) 
            {
                state = NodeState.Succes;
            }
            else
            {
                state = NodeState.Failure;
            }
            return state;
        }
    }

    public class NodeCheckBoostActive : Node
    {
        public NodeCheckBoostActive() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            if (stats.IsBoostActive)
            {
                state = NodeState.Succes;
            }
            else
            {
                state = NodeState.Failure;
            }
            return state;
        }
    }

    public class NodeCheckCanBoost : Node
    {
        public NodeCheckCanBoost() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            if (stats.energy >= CommonData.instance.EnergyForPowerUp)
            {
                state = NodeState.Succes;
            }
            else
            {
                state = NodeState.Failure;
            }
            return state;
        }
    }

    public class NodeCheckCanHeavyAttack : Node
    {
        public NodeCheckCanHeavyAttack() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            if (stats.energy >= CommonData.instance.EnergyForHeavyAttack)
            {
                state = NodeState.Succes;
            }
            else
            {
                state = NodeState.Failure;
            }
            return state;
        }
    }

    public class NodeCheckLowHealth : Node
    {
        public NodeCheckLowHealth() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            if (stats.health < CommonData.instance.MaxHealth / 2)
            {
                state = NodeState.Succes;
            }
            else
            {
                state = NodeState.Failure;
            }
            return state;
        }
    }
}
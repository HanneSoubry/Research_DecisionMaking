using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class NodeChooseBasicAttack : Node
    {
        public NodeChooseBasicAttack() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            action = PlayerBehavior.Action.BasicAttack;
            state = NodeState.Succes;
            return state;
        }
    }

    public class NodeChoosePowerUp : Node
    {
        public NodeChoosePowerUp() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            action = PlayerBehavior.Action.PowerUp;
            state = NodeState.Succes;
            return state;
        }
    }

    public class NodeChooseHeavyAttack : Node
    {
        public NodeChooseHeavyAttack() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            action = PlayerBehavior.Action.HeavyAttack;
            state = NodeState.Succes;
            return state;
        }
    }

    public class NodeChooseHeal : Node
    {
        public NodeChooseHeal() { }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            action = PlayerBehavior.Action.Heal;
            state = NodeState.Succes;
            return state;
        }
    }
}
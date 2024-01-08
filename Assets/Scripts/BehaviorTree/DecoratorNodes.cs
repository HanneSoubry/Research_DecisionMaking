using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class DecoratorInvert : Node
    {
        public DecoratorInvert(Node _child)
        {
            Attach(_child);
        }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            NodeState result = children[0].Evaluate(ref stats, ref action);
            if(result == NodeState.Succes)
            {
                state = NodeState.Failure;
            }
            else if(result == NodeState.Failure)
            {
                state = NodeState.Succes;
            }
            else
            {
                state = NodeState.Running;
            }

            return state;
        }
    }
}
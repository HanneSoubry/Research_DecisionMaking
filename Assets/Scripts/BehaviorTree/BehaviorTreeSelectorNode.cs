using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class Selector : Node
    {
        public Selector(List<Node> _children)
        {
            foreach (Node child in _children)
            {
                Attach(child);
            }
        }

        public override NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action)
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate(ref stats, ref action))
                {
                    case NodeState.Succes:
                        {
                            state = NodeState.Succes;
                            return state;
                        }
                    case NodeState.Running:
                        {
                            state = NodeState.Running;
                            return state;
                        }
                    case NodeState.Failure:
                        {
                            continue;   // next node
                        }
                }
            }

            state = NodeState.Failure;
            return state;
        }
    }
}
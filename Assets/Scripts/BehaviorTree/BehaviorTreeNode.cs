using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BehaviorTree
{
    public enum NodeState
    {
        Running,
        Succes,
        Failure
    }

    public class Node
    {
        protected NodeState state;

        public Node parent;
        protected List<Node> children = new List<Node>();

        public Node(List<Node> _children)
        {
            foreach(Node child in _children)
            {
                Attach(child);
            }
        }

        public Node(){ }    // empty constructor

        protected void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate(ref PlayerCharacter.PlayerStats stats, ref PlayerBehavior.Action action) 
        {
            Debug.LogError("Binary tree node with not overridden function Evaluate()");
            return NodeState.Failure; 
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{ 
    public class Tree
    {
        private Node root = null;

        public PlayerBehavior.Action ChooseAction(ref PlayerCharacter.PlayerStats stats)
        {
            PlayerBehavior.Action action = PlayerBehavior.Action.None;

            if(root != null)
            {
                root.Evaluate(ref stats, ref action);
            }

            return action;  
        }

        public void SetTree(Node rootNode)
        {
            if(root != null)
            {
                Debug.LogWarning("Behavior Tree: Overriding existing root node");
            }

            root = rootNode;
        }
    }
}

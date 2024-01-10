using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI
{
    public class UtilityAIBrain
    {
        private List<UtilityAction> actions;

        public PlayerBehavior.Action ChooseAction()
        {
            UtilityAction bestAction = null;
            int bestScore = 0;

            foreach(UtilityAction a in actions)
            {
                // first check conditions
                bool canExecute = true;
                foreach(Condition condition in a.Conditions)
                {
                    if(condition.Evaluate() == false)
                    {
                        canExecute = false;
                        break;
                    }
                }

                if(!canExecute)
                {
                    // skip this action
                    continue;
                }

                // if can execute -> how usefull is it?
                int score = 0;
                foreach(Consideration consideration in a.Considerations)
                {
                    score += consideration.Evaluate();
                }

                // check with current best action
                if(score > bestScore)
                {
                    bestScore = score;
                    bestAction = a;
                }
            }

            if(bestAction != null)
            {
                return bestAction.GetAction();
            }
            else
            {
                Debug.LogWarning("UtilityBrain does not have any valid action");
                return PlayerBehavior.Action.None;
            }
        }

        public void SetActions(List<UtilityAction> _actions)
        {
            actions = _actions;
        }
    }
}

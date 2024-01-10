using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI
{
    public class UtilityAction
    {
        private List<Consideration> considerations;
        private List<Condition> conditions;
        private PlayerBehavior.Action action;

        public UtilityAction(PlayerBehavior.Action _action, List<Condition> _conditions, List<Consideration> _considerations)
        {
            action = _action;
            considerations = _considerations;
            conditions = _conditions;
        }

        public PlayerBehavior.Action GetAction()
        {
            return action;
        }

        public List<Consideration> Considerations
        {
            get { return considerations; }
        }

        public List<Condition> Conditions
        {
            get { return conditions; }
        }
    }
}
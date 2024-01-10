using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAI
{
    public abstract class Condition
    {
        private GameManager gameManager = null;
        protected GameManager GetGameManager
        {
            get
            {
                if (gameManager == null)
                {
                    gameManager = UnityEngine.Object.FindObjectOfType<GameManager>();
                }
                return gameManager;
            }
        }

        // YES or NO question
        // Is it possible to execute? Does it make sense at all?
        public abstract bool Evaluate();
    }

    public class ConditionMinimumEnergy : Condition
    {
        private int requiredEnergy = 0;

        public ConditionMinimumEnergy(int _requiredEnergy)
        {
            requiredEnergy = _requiredEnergy;
        }

        public override bool Evaluate()
        {
            if (GetGameManager.GetCurrentPlayerStats().energy >= requiredEnergy)
            {
                return true;
            }
            else return false;
        }
    }

    public class ConditionNotFullHealth : Condition
    { 
        public override bool Evaluate()
        {
            if (GetGameManager.GetCurrentPlayerStats().health == CommonData.instance.MaxHealth)
            {
                return false;
            }
            else return true;
        }
    }
}
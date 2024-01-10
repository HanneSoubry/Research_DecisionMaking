namespace UtilityAI
{
    public abstract class Consideration
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

        // Is it usefull? Give score
        public abstract int Evaluate();
    }

    public class ConsiderationBasicAttackValue : Consideration
    {
        public override int Evaluate()
        {
            GameManager gameManager = GetGameManager;
            CommonData data = CommonData.instance;
            PlayerCharacter.PlayerStats myStats = gameManager.GetCurrentPlayerStats();
            PlayerCharacter.PlayerStats enemyStats = gameManager.GetEnemyPlayerStats();

            if(enemyStats.health <= data.BasicAttackDamage ||
                (myStats.IsBoostActive && enemyStats.health <= data.BoostedBasicAttackDamage))
            {
                // finishing attack
                return 10;
            }

            int energy = myStats.energy;
            if(energy <= 1)
            {
                // low energy, this action does not require energy
                return 5;
            }
            else if (energy == 3)
            {
                // full energy, prefer to use it instead of a basic action
                return 0;
            }
            // else energy == 2

            if(myStats.health >= data.MaxHealth - data.HealingBonus)
            {
                // healing cannot apply full effect
                // use energy on a better attack move
                return 0;
            }

            if(enemyStats.health < myStats.health)
            {
                // prefer strong attacks to finish the enemy quickly
                return 1;
            }
            else  // my health lower than enemy
            {
                // save energy to heal next turn
                return 5;
            }
        }
    }

    public class ConsiderationPowerUpValue : Consideration
    {
        public override int Evaluate()
        {
            GameManager gameManager = GetGameManager;
            CommonData data = CommonData.instance;
            PlayerCharacter.PlayerStats myStats = gameManager.GetCurrentPlayerStats();
            PlayerCharacter.PlayerStats enemyStats = gameManager.GetEnemyPlayerStats();

            if (myStats.IsBoostActive || myStats.energy <= 1)
            {
                return 0;
            }

            // else boost not active and energy == 2 or 3

            if (myStats.health >= data.MaxHealth - data.HealingBonus)
            {
                // makes no sense to heal and I am not boosted
                return 5;
            }

            if (enemyStats.health < myStats.health)
            {
                // prefer strong attacks to finish the enemy quickly
                return 4;
            }
            else  // my health lower than enemy
            {
                // save energy to heal next turn
                return 0;
            }
        }
    }
    public class ConsiderationHeavyAttackValue : Consideration
    {
        public override int Evaluate()
        {
            GameManager gameManager = GetGameManager;
            CommonData data = CommonData.instance;
            PlayerCharacter.PlayerStats myStats = gameManager.GetCurrentPlayerStats();
            PlayerCharacter.PlayerStats enemyStats = gameManager.GetEnemyPlayerStats();

            if (enemyStats.health <= data.EnergyForHeavyAttack ||
                (myStats.IsBoostActive && enemyStats.health <= data.BoostedHeavyAttackDamage))
            {
                // finishing attack
                return 10;
            }

            if(myStats.IsBoostActive)
            {
                // Get most value out of the active boost with heavy attacks
                return 6;
            }

            if (enemyStats.health < myStats.health)
            {
                // prefer strong attacks to finish the enemy quickly
                // but might prefer to boost above this
                return 3;
            }
            else  // my health lower than enemy
            {
                // save energy to heal next turn
                return 0;
            }
        }
    }

    public class ConsiderationHealValue : Consideration
    {
        public override int Evaluate()
        {
            GameManager gameManager = GetGameManager;
            CommonData data = CommonData.instance;
            PlayerCharacter.PlayerStats myStats = gameManager.GetCurrentPlayerStats();
            PlayerCharacter.PlayerStats enemyStats = gameManager.GetEnemyPlayerStats();

            if(myStats.health <= data.BasicAttackDamage ||
                (enemyStats.IsBoostActive && myStats.health <= data.BoostedBasicAttackDamage) ||
                (enemyStats.energy >= data.EnergyForHeavyAttack - 1 && myStats.health <= data.HeavyAttackDamage) || 
                // -1 because enemy will gain 1 energy at the start of its turn
                (enemyStats.IsBoostActive && enemyStats.energy >= data.EnergyForHeavyAttack - 1 && myStats.health <= data.BoostedHeavyAttackDamage))
            {
                // Enemy can kill me next turn
                return 9;
            }

            if (myStats.health < enemyStats.health)
            {
                return 4;
            }

            // enemy health lower than mine
            // prefer attacking
            return 2;
        }
    }
}
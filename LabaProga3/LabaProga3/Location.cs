using System;

namespace LabaProga3
{
    public interface ILocation
    {
        Enemy SpawnEnemy(); 
    }

    public class Goblin : Enemy
    {
        private GameLogger logger;

        public Goblin()
        {
            logger = GameLogger.GetInstance();
            name = "Гоблин";
            health = 50;
            damage = 10;
        }

        public override void TakeDamage(int damage)
        {
            logger.Log($"{name} получает {damage} урона!");
            health -= damage;
            if (health > 0)
            {
                logger.Log($"У {name} осталось {health} здоровья");
            }
        }

        public override void Attack(PlayableCharacter player)
        {
            logger.Log($"{name} атакует {player.GetName()}!");
            player.TakeDamage(damage);
        }
    }

    public class Dragon : Enemy
    {
        private readonly GameLogger gameLogger;
        private float resistance;

        public Dragon()
        {
            gameLogger = GameLogger.GetInstance();
            name = "Дракон";
            resistance = 0.2f;
            health = 100;
            damage = 30;
        }

        public override void TakeDamage(int damage)
        {
            damage = (int)Math.Round(damage * (1 - resistance));
            gameLogger.Log($"{name} получает {damage} урона!");
            health -= damage;
            if (health > 0)
            {
                gameLogger.Log($"У {name} осталось {health} здоровья");
            }
        }

        public override void Attack(PlayableCharacter player)
        {
            gameLogger.Log("Дракон дышит огнем!");
            player.TakeDamage(damage);
        }
    }

    public class Forest : ILocation
    {
        public Enemy SpawnEnemy()
        {
            return new Goblin();
        }
    }

    public class DragonBarrow : ILocation
    {
        public Enemy SpawnEnemy()
        {
            return new Dragon();
        }
    }
}

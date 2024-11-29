using System;

namespace LabaProga3
{
    public interface IWeapon
    {
        int GetDamage();
        void Use();
    }

    public class Sword : IWeapon
    {
        private int damage;
        private readonly GameLogger logger;

        public Sword()
        {
            damage = 20;
            logger = GameLogger.GetInstance();
        }

        public int GetDamage()
        {
            return damage;
        }

        public void Use()
        {
            logger.Log("Удар мечом!");
        }
    }

    public class Bow : IWeapon
    {
        private int damage;
        private double criticalChance;
        private int criticalModifier;
        private readonly GameLogger logger;

        public Bow()
        {
            damage = 15;
            criticalChance = 0.3;
            criticalModifier = 2;
            logger = GameLogger.GetInstance();
        }

        public int GetDamage()
        {
            Random random = new Random();
            double roll = random.NextDouble();
            if (roll <= criticalChance)
            {
                logger.Log("Критический урон!");
                return damage * criticalModifier;
            }

            return damage;
        }

        public void Use()
        {
            logger.Log("Выстрел из лука!");
        }
    }

    public class Staff : IWeapon
    {
        private int damage;
        private double scatter;
        private readonly GameLogger logger;

        public Staff()
        {
            damage = 25;
            scatter = 0.2;
            logger = GameLogger.GetInstance();
        }

        public int GetDamage()
        {
            Random random = new Random();
            double roll = random.NextDouble();
            double factor = 1 + (roll * 2 * scatter - scatter);

            return (int)Math.Round(damage * factor);
        }

        public void Use()
        {
            logger.Log("Воздух накаляется, из посоха вылетает огненный шар!");
        }
    }
}

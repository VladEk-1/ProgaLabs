namespace LabaProga3
{
    public interface IArmor
    {
        float GetDefense();
        void Use();
    }
    public class HeavyArmor : IArmor
    {
        private float defense;
        private readonly GameLogger logger;

        public HeavyArmor()
        {
            logger = GameLogger.GetInstance();
            defense = 0.3f;
        }

        public float GetDefense()
        {
            return defense;
        }

        public void Use()
        {
            logger.Log("Тяжелая броня блокирует значительную часть урона");
        }
    }

    public class LightArmor : IArmor
    {
        private float defense;
        private readonly GameLogger logger;

        public LightArmor()
        {
            logger = GameLogger.GetInstance();
            defense = 0.2f;
        }

        public float GetDefense()
        {
            return defense;
        }

        public void Use()
        {
            logger.Log("Легкая броня блокирует урон");
        }
    }

    public class Robe : IArmor
    {
        private float defense;
        private readonly GameLogger logger;

        public Robe()
        {
            logger = GameLogger.GetInstance();
            defense = 0.1f;
        }

        public float GetDefense()
        {
            return defense;
        }

        public void Use()
        {
            logger.Log("Роба блокирует немного урона");
        }
    }
}

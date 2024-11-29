namespace LabaProga3
{
    public class PlayableCharacter
    {
        private GameLogger logger;
        private string name;
        private CharacterClass characterClass;
        private IWeapon weapon;
        private IArmor armor;
        private int health;

        
        private PlayableCharacter(Builder builder)
        {
            logger = GameLogger.GetInstance();
            name = builder.Name;
            characterClass = builder.CharacterClass;
            weapon = builder.Weapon;
            armor = builder.Armor;
            health = characterClass.GetStartingHealth();
        }

        
        public class Builder
        {
            public string Name { get; private set; }
            public CharacterClass CharacterClass { get; private set; }
            public IWeapon Weapon { get; private set; }
            public IArmor Armor { get; private set; }

            public Builder SetName(string name)
            {
                Name = name;
                return this;
            }

            public Builder SetCharacterClass(CharacterClass characterClass)
            {
                CharacterClass = characterClass;
                return this;
            }

            public Builder SetWeapon(IWeapon weapon)
            {
                Weapon = weapon;
                return this;
            }

            public Builder SetArmor(IArmor armor)
            {
                Armor = armor;
                return this;
            }

            
            public PlayableCharacter Build()
            {
                return new PlayableCharacter(this);
            }
        }

        
        public void TakeDamage(int damage)
        {
            int reducedDamage = (int)(damage * (1 - armor.GetDefense()));

            if (reducedDamage < 0)
                reducedDamage = 0;

            health -= reducedDamage;
            armor.Use();
            logger.Log($"{name} получил урон: {reducedDamage}");

            if (health > 0)
            {
                logger.Log($"У {name} осталось {health} здоровья");
            }
        }

        
        public void Attack(Enemy enemy)
        {
            logger.Log($"{name} атакует врага {enemy.GetName()}");
            weapon.Use();
            enemy.TakeDamage(weapon.GetDamage());
        }

        public bool IsAlive()
        {
            return health > 0;
        }

        public string GetName()
        {
            return name;
        }
    }
}

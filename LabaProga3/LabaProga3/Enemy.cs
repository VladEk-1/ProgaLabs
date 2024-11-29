namespace LabaProga3
{
    public abstract class Enemy
    {
        protected string name;
        protected int health;
        protected int damage;

        public string GetName()
        {
            return name;
        }

        public int GetHealth()
        {
            return health;
        }

        public abstract void TakeDamage(int damage);

        public abstract void Attack(PlayableCharacter player);

        public bool isAlive()
        {
            return health > 0;
        }
    }
}

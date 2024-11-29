using System;

namespace LabaProga3
{
    public enum CharacterClass
    {
        WARRIOR = 100,
        THIEF = 90,
        MAGE = 80
    }

    public static class CharacterClassExtensions
    {
        public static int GetStartingHealth(this CharacterClass characterClass)
        {
            switch(characterClass)
            {
                case CharacterClass.WARRIOR: return 100;
                case CharacterClass.THIEF: return 90;
                case CharacterClass.MAGE: return 80;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
}

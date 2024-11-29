using System;

namespace LabaProga3
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Создайте своего персонажа:");

            Console.WriteLine("Введите имя:");
            string name = Console.ReadLine();

            Console.WriteLine("Выберите класс из списка: " + string.Join(", ", Enum.GetNames(typeof(CharacterClass))));
            CharacterClass characterClass = (CharacterClass)Enum.Parse(typeof(CharacterClass), Console.ReadLine().ToUpper());

            IEquipmentChest startingEquipmentChest = GetChest(characterClass);
            IArmor startingArmor = startingEquipmentChest.GetArmor();
            IWeapon startingWeapon = startingEquipmentChest.GetWeapon();

            PlayableCharacter player = new PlayableCharacter.Builder()
                .SetName(name)
                .SetCharacterClass(characterClass)
                .SetArmor(startingArmor)
                .SetWeapon(startingWeapon)
                .Build();

            GameLogger gameLogger = GameLogger.GetInstance();
            gameLogger.Log($"{player.GetName()} очнулся на распутье!");

            Console.WriteLine("Куда вы двинетесь? Выберите локацию: (мистический лес, логово дракона)");
            string locationName = Console.ReadLine();
            ILocation location = GetLocation(locationName);

            gameLogger.Log($"{player.GetName()} отправился в {locationName}");
            Enemy enemy = location.SpawnEnemy();
            gameLogger.Log($"У {player.GetName()} на пути возникает {enemy.GetName()}, начинается бой!");

            Random random = new Random();
            while (player.IsAlive() && enemy.isAlive())
            {
                Console.WriteLine("Введите что-нибудь чтобы атаковать!");
                Console.ReadLine();
                player.Attack(enemy);
                bool stunned = random.Next(2) == 0;
                if (stunned)
                {
                    gameLogger.Log($"{enemy.GetName()} был оглушен атакой {player.GetName()}!");
                    continue;
                }
                enemy.Attack(player);
            }

            Console.WriteLine();

            if (!player.IsAlive())
            {
                gameLogger.Log($"{player.GetName()} был убит...");
                return;
            }

            gameLogger.Log($"Злой {enemy.GetName()} был побежден! {player.GetName()} отправился дальше по тропе судьбы...");
            Console.ReadLine();
        }

        
        private static IEquipmentChest GetChest(CharacterClass characterClass)
        {
            switch(characterClass)
            {
              
                case CharacterClass.WARRIOR: return new WarriorEquipmentChest();
                case CharacterClass.THIEF: return new ThiefEquipmentChest();
                case CharacterClass.MAGE: return new MagicalEquipmentChest();
            }
            throw new ArgumentException("Неизвестный класс персонажа");
        }

        
        private static ILocation GetLocation(string locationName)
        {
            switch(locationName.ToLower())
            {
                case "мистический лес": return new Forest();
                case "логово дракона": return new DragonBarrow();
            }
            throw new ArgumentException("Неизвестная локация");
        }
    }
}
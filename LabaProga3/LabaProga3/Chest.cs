namespace LabaProga3
{
    public interface IEquipmentChest
    {
        // Возвращает некое оружие
        IWeapon GetWeapon();

        // Возвращает некую броню
        IArmor GetArmor();
    }

    public class WarriorEquipmentChest : IEquipmentChest
    {
        public IWeapon GetWeapon()
        {
            return new Sword();
        }

        public IArmor GetArmor()
        {
            return new HeavyArmor();
        }
    }

    public class MagicalEquipmentChest : IEquipmentChest
    {
        public IWeapon GetWeapon()
        {
            return new Staff();
        }

        public IArmor GetArmor()
        {
            return new Robe();
        }
    }

    public class ThiefEquipmentChest : IEquipmentChest
    {
        public IWeapon GetWeapon()
        {
            return new Bow();
        }

        public IArmor GetArmor()
        {
            return new LightArmor();
        }
    }
}

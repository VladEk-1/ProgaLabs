using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба_2
{
    public class Person
    {
        private string name;
        private readonly Alive companion;

        public Person(string name, Alive companion)
        {
            this.name = name;
            this.companion = companion;
        }

        public void GetCompanionInfo()
        {
            Console.WriteLine("--------------------");
            Console.WriteLine($"Companion for {name} is named {companion.GetName()}");
            Console.WriteLine($"It is {companion.GetAge()} years old");
            Console.WriteLine("And it sounds like this:");
            companion.MakeSound();
            Console.WriteLine("--------------------");
        }
    }
    public abstract class Animal : Alive
    {

        private string sound1;
        private int age1;
        private string name1;

        public Animal(int age, string sound, string name)
        {
            age1 = age;
            name1 = name;
            sound1 = sound;
        }

        public virtual void MakeSound()
        {
            Console.WriteLine(sound1);
        }

        public int GetAge()
        {
            return age1;
        }

        public string GetName()
        {
            return name1;
        }

        public void SetName(string name)
        {
            name1 = name;
        }

    }
    public class Dog : Animal
    {
        public Dog(int age, string name)
            : base(age,
                "           /^\\ /^\\ \n" +
                " /        // o  o | \n" +
                "/\\       /    *__/\n" +
                "\\ \\______\\  /     -ARF!\n" +
                " \\         /\n" +
                "  \\ \\----\\ \\\n" +
                "   \\_\\_   \\_\\_",
                name)
        {
        }
    }

    public class Cat : Animal
    {
        private static Random random = new Random();

        public Cat(int age, string name)
            : base(age,
               "" +
                "    /\\___/\\\n" +
                "   /       \\\n" +
                "  l  u   u  l\n" +
                "--l----*----l--\n" +
                "   \\   w   /     - Meow!\n" +
                "     ======\n" +
                "   /       \\ __\n" +
                "   l        l\\ \\\n" +
                "   l        l/ /\n" +
                "   l  l l   l /\n" +
                "   \\ ml lm /_/",
                name)
        {
        }
        public override void MakeSound()
        {
            if (random.Next(2) == 0)
            {
                Console.WriteLine(
                    "      /\\_/\\\n" +
                    " /\\  / o o \\\n" +
                    "//\\\\ \\~(*)~/\n" +
                    "`  \\/   ^ /\n" +
                    "   | \\|| ||\n" +
                    "   \\ '|| ||\n" +
                    $"    \\)()-()) [{GetName()} is not in the mood to meow]"
                );
                return;
            }
            base.MakeSound();
        }
    }
    public interface Alive
    {
        void MakeSound();

        int GetAge();

        string GetName();
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Dog Sobaka = new Dog(8, "Bublik");
            Cat Koshka = new Cat(2, "Barsik");

            Person Bill = new Person("Bill", Sobaka);
            Person Janete = new Person("Jenete", Koshka);

            Bill.GetCompanionInfo();
            Janete.GetCompanionInfo();
            Console.ReadLine();
        }
    }
}

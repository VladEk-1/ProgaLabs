using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба_4
{
    internal class Program
    {
        //декоратор
        public interface ICoffee
        {
            string GetDescription();
            int GetCost();
        }
        public class SimpleCoffee : ICoffee
        {
            public string GetDescription()
            {
                return "Simple Coffee";
            }

            public int GetCost()
            {
                return 5;
            }
        }
        public abstract class CoffeeDecorator : ICoffee
        {
            public ICoffee newcoffee;
            

            public CoffeeDecorator(ICoffee coffee)
            {
                this.newcoffee = coffee;
            }

            public virtual string GetDescription()
            {
                return newcoffee.GetDescription();
            }

            public virtual int GetCost()
            {
                return newcoffee.GetCost();
            }
        }
        public class MilkDecorator : CoffeeDecorator
        {
            public MilkDecorator(ICoffee coffee) : base(coffee) { }

            public override string GetDescription()
            {
                return base.GetDescription() + ", Milk";
            }

            public override int GetCost()
            {
                return base.GetCost() + 2;
            }
        }
        public class SugarDecorator : CoffeeDecorator
        {
            public SugarDecorator(ICoffee coffee) : base(coffee) { }

            public override string GetDescription()
            {
                return base.GetDescription() + ", Sugar";
            }

            public override int GetCost()
            {
                return base.GetCost() + 1;
            }
        }
       
        //заместитель
        public interface IFile
        {
            string ReadFile();
            void WriteFile(string content);
        }

        
        public class RealFile : IFile
        {
            private string _filePath;

            public RealFile(string filePath)
            {
                _filePath = filePath;
                LoadFile();
            }

            private void LoadFile()
            {
                Console.WriteLine($"Loading file from {_filePath}...");
                
            }

            public string ReadFile()
            {
                Console.WriteLine("Reading file...");
                
                return "File content";
            }

            public void WriteFile(string content)
            {
                Console.WriteLine($"Writing content to file: {content}");
                
            }
        }
        public class FileProxy : IFile
        {
            private RealFile _realFile;
            private string _filePath;
            private string _cachedContent;
            

            public FileProxy(string filePath)
            {
                _filePath = filePath;
               
            }

            private RealFile GetRealFile()
            {
                if (_realFile == null)
                {
                    _realFile = new RealFile(_filePath);
                }
                return _realFile;
            }

            public string ReadFile()
            {
                if (_cachedContent == null)
                {
                    _cachedContent = GetRealFile().ReadFile();
                }

                return _cachedContent;
            }

            public void WriteFile(string content)
            {
                 GetRealFile().WriteFile(content);
                _cachedContent = content; 
            }
        }
        //адаптер
        
        public interface IEmployee
        {
            string GetEmployeeDetails();
        }

        
        public class Employee : IEmployee
        {
            private string _name;
            private int _id;

            public Employee(string name, int id)
            {
                _name = name;
                _id = id;
            }

            public string GetEmployeeDetails()
            {
                return $"Employee ID: {_id}, Name: {_name}";
            }
        }
        
        public class ThirdPartyEmployee
        {
            private string _name;
            private int _id;

            public ThirdPartyEmployee(string name, int id)
            {
                _name = name;
                _id = id;
            }

            public string GetEmployeeData()
            {
                return $"ThirdPartyEmployee ID: {_id}, Name: {_name}";
            }
        }
        public class EmployeeAdapter : IEmployee
        {
            private ThirdPartyEmployee _thirdPartyEmployee;

            public EmployeeAdapter(ThirdPartyEmployee thirdPartyEmployee)
            {
                _thirdPartyEmployee = thirdPartyEmployee;
            }

            public string GetEmployeeDetails()
            {
                return _thirdPartyEmployee.GetEmployeeData();
            }
        }


        //фасад
        public class Engine
        {
            public void Start()
            {
                Console.WriteLine("Engine started.");
            }

            public void Stop()
            {
                Console.WriteLine("Engine stopped.");
            }
        }

        public class Wheels
        {
            public void Inflate()
            {
                Console.WriteLine("Wheels inflated.");
            }

            public void Deflate()
            {
                Console.WriteLine("Wheels deflated.");
            }
        }

        public class Car
        {
            private Engine _engine;
            private Wheels _wheels;

            public Car()
            {
                _engine = new Engine();
                _wheels = new Wheels();
            }

            public void StartCar()
            {
                _engine.Start();
                _wheels.Inflate();
                Console.WriteLine("Car started.");
            }

            public void StopCar()
            {
                _engine.Stop();
                _wheels.Deflate();
                Console.WriteLine("Car stopped.");
            }

            
        }
        public class CarFacade
        {
            private Car _car;

            public CarFacade()
            {
                _car = new Car();
            }

            public void Start()
            {
                _car.StartCar();
            }

            public void Stop()
            {
                _car.StopCar();
            }
        }

        static void Main(string[] args)
        {
           //декоратор
            ICoffee coffee = new SimpleCoffee();
            Console.WriteLine($"Description: {coffee.GetDescription()}");
            Console.WriteLine($"Cost: {coffee.GetCost()}");

            
            coffee = new MilkDecorator(coffee);
            Console.WriteLine($"Description: {coffee.GetDescription()}");
            Console.WriteLine($"Cost: {coffee.GetCost()}");

            
            coffee = new SugarDecorator(coffee);
            Console.WriteLine($"Description: {coffee.GetDescription()}");
            Console.WriteLine($"Cost: {coffee.GetCost()}");

            
            coffee = new MilkDecorator(coffee);
            Console.WriteLine($"Description: {coffee.GetDescription()}");
            Console.WriteLine($"Cost: {coffee.GetCost()}");
            Console.WriteLine();
            //заместитель
            
            IFile fileProxy = new FileProxy("path/to/file.txt");

            
            Console.WriteLine("Trying to read file without authorization:");
            string content = fileProxy.ReadFile();
            Console.WriteLine(content ?? "No content");

            
            Console.WriteLine("\nTrying to read file after authorization:");
            content = fileProxy.ReadFile();
            Console.WriteLine(content ?? "No content");

            
            Console.WriteLine("\nTrying to write to file after authorization:");
            fileProxy.WriteFile("New content");

            
            Console.WriteLine("\nReading file after writing (from cache):");
            content = fileProxy.ReadFile();
            Console.WriteLine(content ?? "No content");

            //адаптер
            
            IEmployee employee = new Employee("John Doe", 1);
            Console.WriteLine(employee.GetEmployeeDetails());

           
            ThirdPartyEmployee thirdPartyEmployee = new ThirdPartyEmployee("Jane Smith", 2);

            
            IEmployee adaptedEmployee = new EmployeeAdapter(thirdPartyEmployee);
            Console.WriteLine(adaptedEmployee.GetEmployeeDetails());
            Console.WriteLine();

            //фасад

            
            CarFacade carFacade = new CarFacade();

            
            carFacade.Start();

            
        }
    }
}

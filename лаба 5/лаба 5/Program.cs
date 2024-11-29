using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace лаба_5
{
    //стратегия
    public interface ISortStrategy
    {
        void Sort(int[] array);
    }

    
    public class BubbleSortStrategy : ISortStrategy
    {
        public void Sort(int[] array)
        {
            Console.WriteLine("Sorting using Bubble Sort");
            int n = array.Length;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }

    
    public class QuickSortStrategy : ISortStrategy
    {
        public void Sort(int[] array)
        {
            Console.WriteLine("Sorting using Quick Sort");
            Array.Sort(array);
        }

        
    }
    public class Sorter
    {
        private ISortStrategy _strategy;

        public Sorter(ISortStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(ISortStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SortArray(int[] array)
        {
            _strategy.Sort(array);
        }
    }

    //наблюдатель
    public interface IEvent
    {
        string GetDescription();
    }

    
    public interface IObserver
    {
        void Update(IEvent e);
    }

    
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(IEvent e);
    }

    
    public class TemperatureChangedEvent : IEvent
    {
        private double _temperature;

        public TemperatureChangedEvent(double temperature)
        {
            _temperature = temperature;
        }

        public string GetDescription()
        {
            return $"Temperature changed to {_temperature}°C";
        }
    }

    
    public class HumidityChangedEvent : IEvent
    {
        private double _humidity;

        public HumidityChangedEvent(double humidity)
        {
            _humidity = humidity;
        }

        public string GetDescription()
        {
            return $"Humidity changed to {_humidity}%";
        }
    }

    
    public class TemperatureObserver : IObserver
    {
        public void Update(IEvent e)
        {
            if (e is TemperatureChangedEvent)
            {
                Console.WriteLine($"TemperatureObserver: {e.GetDescription()}");
            }
        }
    }

    
    public class HumidityObserver : IObserver
    {
        public void Update(IEvent e)
        {
            if (e is HumidityChangedEvent)
            {
                Console.WriteLine($"HumidityObserver: {e.GetDescription()}");
            }
        }
    }

    
    public class WeatherStation : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(IEvent e)
        {
            foreach (var observer in _observers)
            {
                observer.Update(e);
            }
        }

        public void ChangeTemperature(double temperature)
        {
            var e = new TemperatureChangedEvent(temperature);
            Notify(e);
        }

        public void ChangeHumidity(double humidity)
        {
            var e = new HumidityChangedEvent(humidity);
            Notify(e);
        }
    }

    //цепочка обязанностей

    public interface IOrderHandler
    {
        IOrderHandler SetNext(IOrderHandler handler);
        bool Handle(Order order);
    }

    
    public abstract class AbstractOrderHandler : IOrderHandler
    {
        private IOrderHandler _nextHandler;

        public IOrderHandler SetNext(IOrderHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual bool Handle(Order order)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.Handle(order);
            }
            else
            {
                return true;
            }
        }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public decimal CreditLimit { get; set; }
    }

    public class InventoryChecker : AbstractOrderHandler
    {
        public override bool Handle(Order order)
        {
            Console.WriteLine("Checking inventory for product: " + order.ProductName);
            if (order.Quantity <= 10) 
            {
                Console.WriteLine("Inventory check passed.");
                return base.Handle(order);
            }
            else
            {
                Console.WriteLine("Inventory check failed. Not enough stock.");
                return false;
            }
        }
    }

    
    public class CreditLimitChecker : AbstractOrderHandler
    {
        public override bool Handle(Order order)
        {
            Console.WriteLine("Checking credit limit for customer: " + order.CustomerName);
            if (order.TotalAmount <= order.CreditLimit)
            {
                Console.WriteLine("Credit limit check passed.");
                return base.Handle(order);
            }
            else
            {
                Console.WriteLine("Credit limit check failed. Insufficient credit limit.");
                return false;
            }
        }
    }

    
    public class OrderApprover : AbstractOrderHandler
    {
        public override bool Handle(Order order)
        {
            Console.WriteLine("Approving order: " + order.OrderId);
            Console.WriteLine("Order approved.");
            return true;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 64, 34, 25, 12, 22, 11, 90 };

            
            Sorter sorter = new Sorter(new BubbleSortStrategy());
            sorter.SortArray(array);
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();


            sorter.SetStrategy(new QuickSortStrategy());
            sorter.SortArray(array);
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            //наблюдатель
            WeatherStation weatherStation = new WeatherStation();

            
            IObserver temperatureObserver1 = new TemperatureObserver();
            IObserver temperatureObserver2 = new TemperatureObserver();
            IObserver humidityObserver = new HumidityObserver();

            
            weatherStation.Attach(temperatureObserver1);
            weatherStation.Attach(temperatureObserver2);
            weatherStation.Attach(humidityObserver);

            
            weatherStation.ChangeTemperature(25.5);
            weatherStation.ChangeHumidity(60.0);

            
            weatherStation.Detach(temperatureObserver2);

            
            weatherStation.ChangeTemperature(26.0);
            weatherStation.ChangeHumidity(70.0);

            Console.WriteLine();

            //цепочка обязанностей
            Order order = new Order
            {
                OrderId = 1,
                ProductName = "Laptop",
                Quantity = 5,
                TotalAmount = 2500,
                CustomerName = "John Doe",
                CreditLimit = 3000
            };

            
            IOrderHandler inventoryChecker = new InventoryChecker();
            IOrderHandler creditLimitChecker = new CreditLimitChecker();
            IOrderHandler orderApprover = new OrderApprover();

            
            inventoryChecker.SetNext(creditLimitChecker).SetNext(orderApprover);

            
            bool isOrderApproved = inventoryChecker.Handle(order);

            
            Console.WriteLine("\nOrder approval status: " + (isOrderApproved ? "Approved" : "Rejected"));
        }
        
    }
}

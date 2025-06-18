using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testevent
{
    internal class Program
    {
        /* private class tempretertest : EventArgs
         {
             public decimal oldtempretuer{ get;  }
             public decimal newtempretuer { get; }
             public decimal deffrince { get; } 
             public tempretertest(decimal test,decimal test2)
             {
                 this.oldtempretuer = test;
                 this.newtempretuer = test2;
                 this.deffrince = test2 - test;
             }
         }*/
        /*private class tempretuer
        {
            public event EventHandler<tempretertest> testEvent;
            public decimal oldtempretuer { get; private set; }
            public decimal newtempretuer { get; private set; }
            public decimal deffrince { get; private set;     }
            public void changetempretuer(decimal test, decimal test2)
            {
                this.oldtempretuer = test;
                this.newtempretuer = test2;
                this.deffrince = test2 - test;
                testEvent?.Invoke(this, new tempretertest(test, test2));
            }
           
        }*/
        /* private class tempretuerhandler
         {

             public void tempretuerchange()
             {
                 tempretuer test = new tempretuer();
                 test.testEvent += Tempretuer_TestEvent;
                 test.changetempretuer(10, 20);
             }

             private void Tempretuer_TestEvent(object sender, tempretertest e)
             {
                 Console.WriteLine($"oldtempretuer: {e.oldtempretuer}, newtempretuer: {e.newtempretuer}, deffrince: {e.deffrince}");
             }
         }*/
        /*
        static void Main(string[] args)
        {
            tempretuerhandler test = new tempretuerhandler();
            test.tempretuerchange();
        }*/
        public class TemperatureChangedEventArgs : EventArgs
        {
            public decimal OldTemperature { get; }
            public decimal NewTemperature { get; }
            public decimal Difference => NewTemperature - OldTemperature;

            public TemperatureChangedEventArgs(decimal oldTemp, decimal newTemp)
            {
                OldTemperature = oldTemp;
                NewTemperature = newTemp;
            }
        }
        public class TemperatureSensor
        {
            public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

            private decimal currentTemperature;

            public void UpdateTemperature(decimal newTemperature)
            {
                if (newTemperature != currentTemperature)
                {
                    var oldTemp = currentTemperature;
                    currentTemperature = newTemperature;
                    OnTemperatureChanged(new TemperatureChangedEventArgs(oldTemp, newTemperature));
                }
            }

            protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
            {
                EventHandler<TemperatureChangedEventArgs> handler = TemperatureChanged; // ✅ Local copy
                handler?.Invoke(this, e); // Safe invocation
            }
        }
        public class DetrmaineTemperature : IDisposable
        {
            private readonly TemperatureSensor sensor;
            public DetrmaineTemperature(TemperatureSensor sensor)
            {
                this.sensor = sensor;
                sensor.TemperatureChanged += HandleTemperatureChange;
            }

            public void Dispose()
            {
                sensor.TemperatureChanged -= HandleTemperatureChange;
            }

            private void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
            {
                Console.WriteLine(((e.Difference > 0) ? "Coold" : "Hot"));
            }
        }
        public class TemperatureMonitor : IDisposable
        {
            private readonly TemperatureSensor sensor;

            public TemperatureMonitor(TemperatureSensor sensor)
            {
                this.sensor = sensor;
                this.sensor.TemperatureChanged += HandleTemperatureChange;
            }

            private void HandleTemperatureChange(object sender, TemperatureChangedEventArgs e)
            {
                Console.WriteLine($"Old: {e.OldTemperature}, New: {e.NewTemperature}, Difference: {e.Difference}");
            }

            public void Dispose()
            {
                sensor.TemperatureChanged -= HandleTemperatureChange;
            }
        }
        public class NewsArtical
        {
            public string Title { get; }
            public string Description { get; }
            public NewsArtical(string title, string description)
            {
                this.Title = title;
                this.Description = description;
            }
        }
        public class NewsPublicher
        {
            public event EventHandler<NewsArtical> _NewsArtical;
            public event EventHandler<NewsArtical> NewsArtical
            {
                add
                {
                    _NewsArtical += value;
                }
                remove
                {
                    _NewsArtical -= value;
                }
            }
            public string Title { get; private set; }
            public string Description { get; private set; }
            protected virtual void Updated(NewsArtical newsArtical)
            {
                var handler = _NewsArtical;
                handler?.Invoke(this, newsArtical);
            }
            public void UpdateNews(NewsArtical newsArtical)
            {
                if (newsArtical == null) return;
                if (newsArtical.Title != Title)
                {
                    Title = newsArtical.Title;
                    Description = newsArtical.Description;
                    Updated(newsArtical);

                }
            }
        }
        public class NewsSub
        {
            public void sub(ref NewsPublicher newsPublicher) => newsPublicher.NewsArtical += News;
            public void News(object s, NewsArtical a)
            {
                Console.WriteLine(a.Title + a.Description);
            }

        }
        public class OrderEventArgs : EventArgs
        {
            public int Id { get; }
            public string Title { get;  }
            public string Description { get; }
            public decimal Price { get; }
            public int Countity {  get; }
            public OrderEventArgs(int Id,string Title,string Description,decimal Price,int Countity) 
            {
                this.Id = Id;
                this.Title = Title;
                this.Description = Description;
                this.Price = Price;
                this.Countity = Countity;
            }
            
        }
        public class Order 
        {
            private int Id=0;
            public event EventHandler<OrderEventArgs> OnNewOrder {
            add { _OnNewOrder += value; } remove { _OnNewOrder -= value; }
            }
            private event EventHandler<OrderEventArgs> _OnNewOrder;
            public void NewOrder(string Title,string Description,int Countity)
            {
                Id++;
                decimal Price = Id*5/2+Countity;
                New(new OrderEventArgs(Id, Title, Description, Price, Countity));
            }
            protected virtual void New(OrderEventArgs e)
            {
                var handler = _OnNewOrder;
                handler?.Invoke(this,e);
            }
        }
        public class Email
        {
            string Name;
            Order Order;

            public Email(string Name) => this.Name = Name;
            public void Sub(Order order) { if (Order == null) Order = order; else if (Order != order) { UnSub(Order); Order = order; } Order.OnNewOrder += Send; } 
            public void UnSub(Order order) => order.OnNewOrder -= Send;
            private void Send(object sender, OrderEventArgs e)
            {
                if (Name == "email2" && e.Id > 2)
                    UnSub(Order);
                else if (Name == "email1" && e.Id > 1)
                    UnSub(Order);
                Console.WriteLine("You Got This Order {Gmail} MR "+Name);
            }
        }
        public class SMS
        {
            string Name;
            Order Order;

            public SMS(string Name) => this.Name = Name;
            public void Sub(Order order) { if (Order == null) Order = order; else if (Order != order) { UnSub(Order); Order = order; } Order.OnNewOrder += Send; }

            public void UnSub(Order order) => order.OnNewOrder -= Send;

            private void Send(object sender, OrderEventArgs e)
            {
                if (Name == "sms2" && e.Id > 2)
                    UnSub(Order);
                else if (Name == "sms1" && e.Id > 1)
                    UnSub(Order);
                Console.WriteLine("You Got This Order {SMS} MR "+Name);
            }
        }
        public class Shipping
        {
            Order Order;
            string Name;
            public Shipping(string Name) => this.Name = Name;
            public void Sub(Order order) { if (Order == null) Order = order; else if (Order != order) { UnSub(Order);Order = order; } Order.OnNewOrder += Send; }
            public void UnSub(Order order) => order.OnNewOrder -= Send;

            private void Send(object sender, OrderEventArgs e)
            {
                if (Name == "Shipping1" && e.Id > 2)
                    UnSub(Order);
                else if(Name == "Shipping2" && e.Id > 1)
                    UnSub(Order);
                    Console.WriteLine("You Got This Order {To Ship} MR " + Name);
            }
        }

        static void Main(string[] args)
        {
            Email email1 = new Email("email1");
            Email email2 = new Email("email2");
            Shipping shipping1 = new Shipping("shipping1");
            Shipping shipping2 = new Shipping("shipping2");
            SMS sms1 = new SMS("sms1");
            SMS sms2 = new SMS("sms2");
            Order order1 = new Order();
            Order order2 = new Order();
            email2.Sub(order2);
            email2.Sub(order1);
            email1.Sub(order2);
            email1.Sub(order1);
            sms2.Sub(order2);
            sms2.Sub(order1);
            sms1.Sub(order1);
            sms1.Sub(order2);
            shipping2.Sub(order2);
            shipping2.Sub(order1);
            shipping1.Sub(order2);
            shipping1.Sub(order1);
            order1.NewOrder("x", "y", 1);
            order1.NewOrder("z", "b", 5);
            order1.NewOrder("r", "m", 1);
            order2.NewOrder("h", "g", 8);
            order2.NewOrder("c", "v", 2);
            order2.NewOrder("t", "l", 3);
            //NewsPublicher newsPublicher = new NewsPublicher();
            //NewsSub newsSub = new NewsSub();
            //newsSub.sub(ref newsPublicher);
            //newsPublicher.UpdateNews(new NewsArtical("x", "a"));
            //newsPublicher.UpdateNews(new NewsArtical("x", "a"));
            //newsPublicher.UpdateNews(new NewsArtical("c", "a"));
            //newsPublicher.UpdateNews("x", "a");

            //var sensor = new TemperatureSensor();
            ///*using (var monitor = new TemperatureMonitor(sensor))
            //{
            //    using (var determine = new DetrmaineTemperature(sensor))
            //    {
            //        // Simulate temperature changes
            //        sensor.UpdateTemperature(20.0m);
            //        sensor.UpdateTemperature(25.0m);
            //        sensor.UpdateTemperature(30.0m);
            //        sensor.UpdateTemperature(25.0m); // No change, no event triggered
            //        sensor.UpdateTemperature(25.0m);
            //        sensor.UpdateTemperature(30.0m);
            //        sensor.UpdateTemperature(30.0m); // No change, no event triggered
            //        sensor.UpdateTemperature(20.0m);
            //    }
            //}*/
            //var moin = new TemperatureMonitor(sensor);
            //var determine = new DetrmaineTemperature(sensor);
            //// Simulate temperature changes
            //sensor.UpdateTemperature(20.0m);
            //sensor.UpdateTemperature(25.0m);
            //sensor.UpdateTemperature(30.0m);
            //sensor.UpdateTemperature(25.0m); // No change, no event triggered
            //sensor.UpdateTemperature(25.0m);
            //sensor.UpdateTemperature(30.0m);
            //sensor.UpdateTemperature(30.0m); // No change, no event triggered
            //sensor.UpdateTemperature(20.0m);
            //// Dispose of the monitor and determine objects to unsubscribe from events
            //moin.Dispose();
            //determine.Dispose();
        }
    } 
    
}

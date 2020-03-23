using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFModelDesignerFirst {
    internal class Program {
        private static void Main()
        {
            TestPerson();
            Console.WriteLine("Press any key to exit: ");
            Console.ReadLine();
        }

        private static void TestPerson()
        {
            using (var context = new Model1Container())
            {
                var p = new Person()
                {
                    FirstName = "Julie",
                    LastName = "Andrew",
                    MiddleName = "T",
                    TelephoneNumber = "09876"
                };
                context.People.Add(p);
                context.SaveChanges();
                var items = context.People;
                foreach (var x in items)
                    Console.WriteLine("{0} {1}", x.Id, x.FirstName);
            }

            ;
        }

        private static void TestOneToMany()
        {
            Console.WriteLine("One to many association");
            using (var context = new Model1Container())
            {
                var c = new Customer()
                {
                    Name = "Customer 1",
                    City = "Iasi"
                };
                var o1 = new Order()
                {
                    TotalValue = 200.ToString(),
                    Date = DateTime.Now.ToString(),
                    Customer = c
                };
                var o2 = new Order()
                {
                    TotalValue = 300.ToString(),
                    Date = DateTime.Now.ToString(),
                    Customer = c
                };
                context.Customers.Add(c);
                context.Orders.Add(o1);
                context.Orders.Add(o2);
                context.SaveChanges();
                var items = context.Customers;
                foreach (var x in items)
                {
                    Console.WriteLine("Customer : {0}, {1}, {2}", x.Id, x.Name, x.City);
                    foreach (var ox in x.Orders)
                        Console.WriteLine("\tOrders: {0}, {1}, {2}", ox.Id, ox.Date, ox.TotalValue);
                }
            }
        }

    }
}
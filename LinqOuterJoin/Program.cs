using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqOuterJoin
{
    //In a Left Outer Join, each element of the first collection is returned, regardless of whether it has any correlated elements in the second collection.
    //The code shown below, the query uses the join clause to match Computer objects with Order objects testing it for equality using the equals operator.
    //In order to include each element of the Computer collection in the result set even if that element has no matches in the Order collection, we are 
    //using DefaultIfEmpty() and passing in an empty instance of the Order class, when there is no Order for that Computer.
    // Ex: { ComputerID = 2, Name = Surface Book, PaymentMode = , OrderID = 0 }
    //The Select clause defines how the result will appear using anonymous types that consist of the ComputerID, Computer Name, Order Payment Mode and Order ID.
    class Program
    {
        static void Main(string[] args)
        {
            List<Computer> ComputerList = new List<Computer>
            {
                new Computer{ComputerID=1, ComputerName="Lenovo Yoga 900"},
                new Computer{ComputerID=2, ComputerName="Surface Book"},
                new Computer{ComputerID=3, ComputerName="Razer Blade Stealth"},
                new Computer{ComputerID=4, ComputerName="HP Chromebook 13"},
                new Computer{ComputerID=5, ComputerName="Asus ZenBook UX305"}
            };

            List<Order> ComputerOrders = new List<Order>
            {

                new Order{OrderID=1, ComputerID=1, PaymentMode="Check"},
                new Order{OrderID=2, ComputerID=5, PaymentMode="Credit"},
                new Order{OrderID=3, ComputerID=1, PaymentMode="Cash"},
                new Order{OrderID=4, ComputerID=3, PaymentMode="Check"},
                new Order{OrderID=5, ComputerID=5, PaymentMode="Check"},
                new Order{OrderID=6, ComputerID=4, PaymentMode="Cash"}
            };

            var orderForComputers = from cm in ComputerList
                                join ordr in ComputerOrders
                                on cm.ComputerID equals ordr.ComputerID
                                into a
                                from c in a.DefaultIfEmpty(new Order())
                                select new
                                {
                                    cm.ComputerID,
                                    Name = cm.ComputerName,
                                    c.PaymentMode,
                                    c.OrderID
                                };

            foreach (var item in orderForComputers)
                Console.WriteLine(item);

            // Complex Linq 

            //List<OrderX> O;
            //var M = new List<Manufacturer>() { new Manufacturer(1, "Abc"), new Manufacturer(2, "Def") };
            //O = new List<OrderX>()
            //    {
            //        new OrderX(1, 1, 2),
            //        new OrderX(1, 2, 2),
            //        new OrderX(1, 2, 3),
            //        new OrderX(2, 3, 1),
            //        new OrderX(2, 3, 1),
            //        new OrderX(2, 3, 2)
            //    };

            //var OTop = from o in O
            //           group o by new { id = o.id, orderid = o.orderid }
            //           into p
            //           let topType = p.Min(tt => tt.type)
            //           select new OrderX(p.Key.id, p.Key.orderid, topType);

            //var ljoin = from m in M
            //            join t in OTop on m.id equals t.id into ts
            //            from u in ts.DefaultIfEmpty()
            //            select new { u.id, u.orderid, u.type };

            //foreach (var item in M)
            //    Console.WriteLine(item.name);

            Console.ReadLine();

        }

        public class Computer
        {
            public int ComputerID { get; set; }
            public string ComputerName { get; set; }
        }

        public class Order
        {
            public int OrderID { get; set; }
            public int ComputerID { get; set; }
            public string PaymentMode { get; set; }
        }

        //Complex Join Example
        //SELECT A.*
        //FROM A LEFT JOIN (
        //  SELECT* FROM B AS B1 WHERE B1.Type = (SELECT TOP 1 B2.Type FROM B AS B2
        //                                         WHERE B2.JoinID = B1.JoinID
        //                                         ORDER BY B2.Type )
        //) AS B ON B.JoinID = A.JoinID

        public class Manufacturer
        {
            public Manufacturer(int id, string name)
            {
                this.id = id;
                this.name = name;
            }

            public int id { get; set; }
            public string name { get; set; }
        }

        public class OrderX
        {
            public OrderX(int id, int orderid, int type)
            {
                this.orderid = orderid;
                this.id = id;
                this.type = type;
            }

            public int orderid { get; set; }
            public int id { get; set; }
            public int type { get; set; }
        }

    }
}

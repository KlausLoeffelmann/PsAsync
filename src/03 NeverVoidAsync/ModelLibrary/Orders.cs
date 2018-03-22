using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    using ActiveDevelop.MvvmBaseLib;
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class Order : BindableBase
    {
        private Guid myIDOrder;
        private Guid myIDCustomer;
        private int myOrderNo;
        private DateTime myOrderDate;
        private decimal myAmount;

        [DataMember]
        public Guid IDOrder
        {
            get
            {
                return myIDOrder;
            }
            set
            {
                SetProperty(ref myIDOrder, value);
            }
        }

        [DataMember]
        public Guid IDCustomer
        {
            get
            {
                return myIDCustomer;
            }
            set
            {
                base.SetProperty(ref myIDCustomer, value);
            }
        }

        [DataMember]
        public int OrderNo
        {
            get
            {
                return myOrderNo;
            }
            set
            {
                base.SetProperty(ref myOrderNo, value);
            }
        }

        [DataMember]
        public DateTime OrderDate
        {
            get
            {
                return myOrderDate;
            }
            set
            {
                base.SetProperty(ref myOrderDate, value);
            }
        }

        public String OrderDateString
        {
            get
            {
                return myOrderDate.ToString("yyyy-MM-dd");
            }
        }


        [DataMember]
        public decimal Amount
        {
            get
            {
                return myAmount;
            }
            set
            {
                base.SetProperty(ref myAmount, value);
            }
        }

        public static IEnumerable<Order> GenerateOrders(IEnumerable<Customer> forCustomers)
        {

            Random randomGen = new Random(DateTime.Now.Millisecond);
            List<Order> ordersToReturn = new List<Order>();
            var orderCounter = 1;

            foreach (var customerItem in forCustomers)
            {
                customerItem.Orders = new List<Order>();
                int tempVar = 10 + randomGen.Next(90);
                for (int i = 0; i <= tempVar; i++)
                {
                    Order order = new Order
                    {
                        IDCustomer = customerItem.IDCustomer,
                        IDOrder = Guid.NewGuid(),
                        Amount = Convert.ToDecimal(Math.Round(randomGen.NextDouble() * 10000, 2)),
                        OrderDate = new DateTime(2007 + randomGen.Next(10), 1 + randomGen.Next(11), 1 + randomGen.Next(27)),
                        OrderNo = orderCounter
                    };
                    customerItem.Orders.Add(order);
                    orderCounter += 1;
                }
            }
            return ordersToReturn;
        }
    }
}

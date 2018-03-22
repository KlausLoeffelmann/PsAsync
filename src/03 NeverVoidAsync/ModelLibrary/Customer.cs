using System;
using System.Collections.Generic;
using System.Linq;
using ActiveDevelop.MvvmBaseLib;
using System.Runtime.Serialization;

namespace ModelLibrary
{

    [DataContract]
    public class Customer : BindableBase
    {
        private Guid myIDCustomer;
        private int myCustomerNo;
        private string myMainName;
        private string mySecondaryName;
        private string myAdditionalName;
        private string myAddressLine1;
        private string myAddressLine2;
        private string myZip;
        private string myCity;
        private string myState;
        private string myCountry;
        private bool myIsCompany;

        private List<Order> myOrders;

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
        public int CustomerNo
        {
            get
            {
                return myCustomerNo;
            }
            set
            {
                base.SetProperty(ref myCustomerNo, value);
            }
        }

        [DataMember]
        public string MainName
        {
            get
            {
                return myMainName;
            }
            set
            {
                base.SetProperty(ref myMainName, value);
            }
        }

        [DataMember]
        public string SecondaryName
        {
            get
            {
                return mySecondaryName;
            }
            set
            {
                base.SetProperty(ref mySecondaryName, value);
            }
        }

        [DataMember]
        public string AdditionalName
        {
            get
            {
                return myAdditionalName;
            }
            set
            {
                base.SetProperty(ref myAdditionalName, value);
            }
        }

        [DataMember]
        public string AddressLine1
        {
            get
            {
                return myAddressLine1;
            }
            set
            {
                base.SetProperty(ref myAddressLine1, value);
            }
        }

        [DataMember]
        public string AddressLine2
        {
            get
            {
                return myAddressLine2;
            }
            set
            {
                base.SetProperty(ref myAddressLine2, value);
            }
        }

        [DataMember]
        public string Zip
        {
            get
            {
                return myZip;
            }
            set
            {
                base.SetProperty(ref myZip, value);
            }
        }

        [DataMember]
        public string City
        {
            get
            {
                return myCity;
            }
            set
            {
                base.SetProperty(ref myCity, value);
            }
        }

        [DataMember]
        public string State
        {
            get
            {
                return myState;
            }
            set
            {
                base.SetProperty(ref myState, value);
            }
        }

        [DataMember]
        public string Country
        {
            get
            {
                return myCountry;
            }
            set
            {
                base.SetProperty(ref myCountry, value);
            }
        }

        [DataMember]
        public bool IsCompany
        {
            get
            {
                return myIsCompany;
            }
            set
            {
                base.SetProperty(ref myIsCompany, value);
            }
        }

        [DataMember]
        public List<Order> Orders
        {
            get
            {
                return myOrders;
            }
            set
            {
                base.SetProperty(ref myOrders, value);
                OnPropertyChanged("RevenueInCurrentYear");
            }
        }

        [DataMember]
        public decimal? RevenueInCurrentYear
        {
            get
            {
                if (Orders != null)
                {
                    return (
                        from item in Orders
                        where item.OrderDate > new DateTime(DateTime.Now.Year - 1, 1, 1)
                        select item).Sum(item => item.Amount);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                //Just forcing that the value will be reread.
                OnPropertyChanged();
            }
        }

        public static IEnumerable<Customer> GenerateRandomCustomers(int count)
        {

            Random randomGen = new Random(DateTime.Now.Millisecond);

            string[] someLastNames = { "Ardelean", "Löffelmann", "Feigenbaum", "Clarke", "Spooner", "Schwarzenegger",
                                       "Stallone", "Huffmann", "Heilsberg", "Vüllers", "Schindler", "Merkel", "Klinsmann",
                                       "Müller", "Cruise", "Schuhmacher", "Newman", "Petzold", "Brown", "Black", "C:ActiveDevelop",
                                       "C:Ikea", "C:Microsoft", "C:Apple", "C:Cisco", "C:Nokia", "C:Samsung", "C:Amazon", "C:Commodore",
                                       "C:Digital Research" };

            string[] someStreetNames = { "Church Ave.", "Mainstreet", "Alter Postweg", "Buchweg", "Garden Ave.",
                                         "Wiedenbrücker Str.", "Edison Street", "8th Street", "Lange Straße",
                                         "Kurze Straße", "Mühlweg", "Coalmine Ave." };

            string[] someFirstnames = { "Jürgen", "Gabriele", "Uwe", "Katrin", "Hans", "Anders", "Lucian",
                                        "Pete", "Dave", "Catrine", "Anne", "Anja", "Theo", "Momo", "Clair",
                                        "Guido", "Barbara", "Klaus-Leo-Joseph", "Margarete", "Alfred", "Melanie",
                                        "Britta", "José", "Thomas", "Daja", "Gareth", "Axel", "Wolfgang", "Susanne" };

            string[] someCities = { "Lippstadt", "Dortmund", "Düsseldorf", "Berlin", "Seattle", "Anaheim", "München",
                                    "Oceanside", "Encinitas", "Paris", "Nice", "Milano", "Roma", "Amsterdam", "Eickelborn",
                                    "Frankfurt", "New York", "San José" };

            string[] someCountries = { "Germany", "USA", "France", "Italy", "The Netherlands" };

            List<Customer> customersToReturn = new List<Customer>();
            var customerCounter = 1;

            int tempVar = count / 10 + randomGen.Next(count - (count / 10));
            for (int i = 0; i <= tempVar; i++)
            {

                Customer cust = new Customer
                {
                    IDCustomer = Guid.NewGuid(),
                    CustomerNo = customerCounter
                };

                var mainName = someLastNames[randomGen.Next(someLastNames.Length - 1)];
                if (mainName.StartsWith("C:"))
                {
                    cust.MainName = mainName.Substring(2);
                    cust.IsCompany = true;
                }
                else
                {
                    cust.MainName = mainName;
                    cust.SecondaryName = someFirstnames[randomGen.Next(someLastNames.Length - 1)];
                }

                cust.AddressLine1 = someStreetNames[randomGen.Next(someStreetNames.Length - 1)];
                cust.City = someCities[randomGen.Next(someCities.Length - 1)];
                cust.Zip = randomGen.Next(99999).ToString("00000");
                customersToReturn.Add(cust);
                customerCounter += 1;
            }
            return customersToReturn;
        }

    }
}

﻿using System;
using System.Linq;

namespace TPHWithConventions
{
    partial class Program
    {
        static void Main(string[] args)
        {
            CreateDatabase();
            AddSampleData();
            QuerySample();
            DeleteDatabase();
        }

        private static void QuerySample()
        {
            using (var context = new BankContext())
            {
                var creditcardPayments = context.Payments.OfType<CreditcardPayment>();
                foreach (var payment in creditcardPayments)
                {
                    Console.WriteLine($"{payment.Name}, {payment.Amount}");
                }
            }
        }

        private static void AddSampleData()
        {
            using (var context = new BankContext())
            {
                context.CashPayments.Add(new CashPayment { Name = "Donald", Amount = 0.5 });
                context.CashPayments.Add(new CashPayment { Name = "Scrooge", Amount = 20000 });
                context.CreditcardPayments.Add(new CreditcardPayment { Name = "Gus Goose", Amount = 300, CreditcardNumber = "987654321" });
                context.SaveChanges();
            }
        }

        private static void CreateDatabase()
        {
            using (var context = new BankContext())
            {
                bool created = context.Database.EnsureCreated();
                string creationInfo = created ? "created" : "exists";
                Console.WriteLine($"database {creationInfo}");
            }
        }

        private static void DeleteDatabase()
        {
            using (var context = new BankContext())
            {
                bool deleted = context.Database.EnsureDeleted();
                string deletionInfo = deleted ? "deleted" : "not deleted";
                Console.WriteLine($"database {deletionInfo}");
            }
        }

    }
}

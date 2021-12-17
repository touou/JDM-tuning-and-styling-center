using System;
using System.Linq;
using JDMTuneV2.Models;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace JDMTuneV2.Data
{
    public class DataRepository
    {
        private DataConnection Connection { get; }

        public DataRepository(string connectionString)
        {
            var optionsBuilder = new LinqToDbConnectionOptionsBuilder();
            optionsBuilder.UsePostgreSQL(connectionString);
            Connection = new DataConnection(optionsBuilder.Build());
        }
        
        
        public ITable<User> Users => Connection.GetTable<User>();

        public ITable<BillingAddress> Addresses => Connection.GetTable<BillingAddress>();
        
        public ITable<Order> Orders => Connection.GetTable<Order>();

        public void AccountCreate(User acc)
        {
            Connection.Insert(acc);
        }

        public void AddAddress(BillingAddress address)
        {
            Connection.Insert(address);
        }

        public void AddOrder(Order order)
        {
            Connection.Insert(order);
        }

        public User GetAccountById(Guid id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetAccountByEmail(string email)
        {
            return Users.FirstOrDefault(u => u.Email == email);
        }

        public BillingAddress GetAddressById(Guid id)
        {
            return Addresses.FirstOrDefault(a => a.UserId == id);
        }

        public void RemoverAddress(BillingAddress address)
        {
            Connection.Delete(address);
        }

        public Order SearchOrderByUserId(Guid Id)
        {
            return Orders.FirstOrDefault(o => o.UserId == Id);
        }
    }
}
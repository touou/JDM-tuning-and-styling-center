using JDMTune.Models;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;

namespace JDMTune.DataRepo
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

        public void AccountCreate(User acc)
        {
            Connection.Insert(acc);
        }
        
        ///will added later
    }
}
using LoginServer.Model;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Database
{
    public class AccountMDB
    {
        private static AccountMDB Instance;

        private MongoClient client;
        private MongoServer server;
        private MongoDatabase database;

        public static AccountMDB GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new AccountMDB();
        }

        public AccountMDB()
        {
            client = new MongoClient(Config.Config.Db_Url);
            server = client.GetServer();
            database = server.GetDatabase(Config.Config.Db_Name);
        }

        public void CreateAccounnt(Account account)
        {
            var collection = database.GetCollection<Account>("accounts");
            collection.Insert(account);
        }

        public Account GetAccountByLogin(string login)
        {
            var collection = database.GetCollection<Account>("accounts");
            var query = Query<Account>.EQ(e => e.Login, login);
            var account = collection.FindOne(query);
            return (account != null) ? account : null;
        }
    }
}

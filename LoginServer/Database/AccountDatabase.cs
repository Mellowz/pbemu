using LoginServer.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Database
{
    public class AccountDatabase
    {
        private static AccountDatabase Instance;
        public string ConnectStr;

        public static AccountDatabase GetInstance()
        {
            return (Instance != null) ? Instance : Instance = new AccountDatabase();
        }

        public AccountDatabase()
        {
            ConnectStr = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", 
                Config.Config.Db_Host,
                Config.Config.Db_Name,
                Config.Config.Db_User,
                Config.Config.Db_Pass
            );
        }

        public Account GetAccountByToken(string token)
        {
            using (MySqlConnection connection = new MySqlConnection(ConnectStr))
            {
                connection.Open();
                Account account = new Account();

                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM account WHERE token = @token";
                    command.Parameters.AddWithValue("@token", token);
                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            account.Uid = reader.GetInt32(0);
                            account.Name = reader.GetString(1);
                            account.Password = reader.GetString(2);
                            account.Token = reader.GetString(3);
                            account.Cash = reader.GetInt32(5);
                        }
                    }
                    reader.Close();
                }

                return (account.Name != null) ? account : null;
            }
        }
    }
}

using Nini.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginServer.Config
{
    public class Config
    {
        public IConfigSource source;

        private static string CfgPath = Path.GetFullPath("Config/LoginServer.ini");

        private static IConfig Network = new IniConfigSource(CfgPath).Configs["network"];
        private static IConfig Database = new IniConfigSource(CfgPath).Configs["database"];
        private static IConfig Misc = new IniConfigSource(CfgPath).Configs["misc"];

        public static string Login_Host = Network.GetString("login.host");
        public static int Login_Port = Network.GetInt("login.port");

        public static string Game_Host = Network.GetString("game.listener.ip");
        public static int Game_Port = Network.GetInt("game.listener.port");

        public static string Db_Host = Database.GetString("db.mysql.host");
        public static string Db_User = Database.GetString("db.mysql.user");
        public static string Db_Pass = Database.GetString("db.mysql.pass");
        public static string Db_Name = Database.GetString("db.mysql.name");

        public static bool Misc_AutoAccount = Misc.GetBoolean("misc.autoaccount");
        public static bool Misc_Debug = Misc.GetBoolean("misc.debug");
        public static bool Misc_GMOnly = Misc.GetBoolean("misc.gmonly");
    }
}

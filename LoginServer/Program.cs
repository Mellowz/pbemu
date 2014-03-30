using LoginServer.Network;
using System;
using System.Diagnostics;

namespace LoginServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "PointBlank LoginServer";
            Console.WriteLine("----===== PointBlank LoginServer =====----\n\n"
                              + "Authors: Jenose\n"
                              + "Authorized representative: pb.netgame.in.th\n\n"
                              + "-------------------------------------------");

            Opcode.Init();

            ClientManager.GetInstance();
            LoginFactory.GetInstance();
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}

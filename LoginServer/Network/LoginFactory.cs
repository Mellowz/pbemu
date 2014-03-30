using Common.Utilities;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LoginServer.Network
{
    internal class LoginFactory
    {
        private static LoginFactory Instance;
        private static TcpListener LoginListener;

        public LoginFactory()
        {
            new Thread(new ThreadStart(NetworkStart)).Start();
        }

        public static LoginFactory GetInstance()
        {
            return (LoginFactory.Instance != null) ? LoginFactory.Instance : Instance = LoginFactory.Instance = new LoginFactory();
        }

        private void NetworkStart()
        {
            try
            {
                LoginFactory.LoginListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 39190);
                LoginFactory.LoginListener.Start();
                Log.Info("Auth server listening clients at {0}:{1}...", ((IPEndPoint) LoginFactory.LoginListener.LocalEndpoint).Address, 39190);
                LoginFactory.LoginListener.BeginAcceptTcpClient(new AsyncCallback(BeginAcceptTcpClient), (object)null);
            }
            catch (Exception ex)
            {
                Log.ErrorException("NetworkStart:", ex);
            }
        }

        private void BeginAcceptTcpClient(IAsyncResult ar)
        {
            LoginFactory.accept(LoginFactory.LoginListener.EndAcceptTcpClient(ar));
            LoginFactory.LoginListener.BeginAcceptTcpClient(new AsyncCallback(this.BeginAcceptTcpClient), (object)null);
        }

        private static void accept(TcpClient tcpClient)
        {
            ClientManager.GetInstance().AddClient(tcpClient);
        }
    }
}

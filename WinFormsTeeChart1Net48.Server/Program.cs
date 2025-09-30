using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

using WinFormsDevExTC1Net48.Library;

namespace WinFormsTeeChart1Net48.Server
{
    internal class Program
    {
        private static IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Any, Common.TEECHART_NET_SERVER_PORT);
        private static Socket listener;
        private static readonly Dictionary<int, TcpClient> Clients = new Dictionary<int, TcpClient>();
        private static int clientID = 1;
        private static object objLock = new object();



        public static void Main(string[] args)
        {
            TcpListener server = new TcpListener(ipEndpoint);
            int currentClientID = -1;

            server.Start();
            Console.WriteLine($"{DateTime.Now} [서버] TCP 서비스가 시작되었습니다. 클라이언트를 대기 중 ...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();

                Task.Factory.StartNew(async () =>
                {
                    lock (objLock)
                    {
                        currentClientID = clientID;
                        Clients.Add(currentClientID, client);
                        clientID++;
                    }
                    

                    Console.WriteLine($"{DateTime.Now} [서버][클라이언트 ID : {currentClientID}] 서비스 연결됨.");

                    Random rnd = new Random();

                    using (NetworkStream ns = client.GetStream())
                    {
                        while ((ns != null) && (ns.CanWrite))
                        {
                            int number = rnd.Next(Common.TEECHART_NET_RANDOM_MAX_NUMBER);
                            byte[] bytesNumber = Encoding.UTF8.GetBytes(number.ToString() + Common.TEECHART_NET_EOM);

                            try
                            {
                                await ns.WriteAsync(bytesNumber, 0, bytesNumber.Length);
                                await ns.FlushAsync();
                                Console.WriteLine($"{DateTime.Now} [서버][클라이언트 ID : {currentClientID}] {number} 송신.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{DateTime.Now} [서버][클라이언트 ID : {currentClientID}] 서버 오류 => {ex.Message}");
                                break;
                            }
                            finally
                            {
                                await Task.Delay(Common.TEECHART_NET_DELAY_COUNT);
                            }
                        }
                    }
                });
            }
        }
    }
}

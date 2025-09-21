using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WinFormsTeeChart1Net48.Server
{
    internal class Program
    {
        private static IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Any, 8800);
        private static Socket listener;
        private static readonly Dictionary<int, TcpClient> Clients = new Dictionary<int, TcpClient>();
        private static int clientID = 1;



        public static void Main(string[] args)
        {
            TcpListener server = new TcpListener(ipEndpoint);
            int currentClientID = -1;

            server.Start();
            Console.WriteLine($"{DateTime.Now} [서버] TCP 서비스가 시작되었습니다. 클라이언트를 대기 중 ...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                currentClientID = clientID;
                Clients.Add(currentClientID, client);
                clientID++;

                Task.Factory.StartNew(async () =>
                {
                    Console.WriteLine($"{DateTime.Now} [서버] [클라이언트 ID : {currentClientID}] 서비스 연결됨.");

                    Random rnd = new Random();

                    using (NetworkStream ns = client.GetStream())
                    {
                        while ((ns != null) && (ns.CanWrite))
                        {
                            string number = rnd.Next(1000).ToString();
                            byte[] bytesNumber = Encoding.UTF8.GetBytes(number);

                            try
                            {
                                await ns.WriteAsync(bytesNumber, 0, bytesNumber.Length);
                                await ns.FlushAsync();
                                Console.WriteLine($"{DateTime.Now} [클라이언트 ID : {currentClientID}] {number} 송신.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{DateTime.Now} [서버][클라이언트 ID : {currentClientID}] 서버 오류 => {ex.Message}");
                                break;
                            }

                            await Task.Delay(1);
                        }
                    }
                });                
            }
        }
    }
}

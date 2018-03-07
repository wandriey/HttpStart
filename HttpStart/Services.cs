using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpStart
{
    class Services
    {
        private TcpClient _connectionSocket;
        private string _uri;

        public Services(TcpClient connection)
        {
            _connectionSocket = connection;
        }

        public void doIt()
        {
            Stream ns = _connectionSocket.GetStream();
            //Stream ns = new NetworkStream(connectionSocket);
            StreamReader sr = new StreamReader(ns); 
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string[] messagesplit = message.Split(' ');           //Plads nummer 0 = get, 1 = filnavn,  2 = http

            if (messagesplit.ElementAt(0) == "GET")       
            {
                string answer1 = $"{messagesplit.ElementAt(1)}";
                Console.WriteLine(answer1);
                string answer = "HTTP/1.1 200 OK\r\n  Content - Type: text / html\r\n";

                do
                {
                    Console.WriteLine("Client: " + message);
                    sw.WriteLine(answer);
                    message = sr.ReadLine();
                } while (message != null && message != "STOP");
                Console.WriteLine("Server Stopped");
            }

            _connectionSocket.Close();
            ns.Close();
        }
    }
}

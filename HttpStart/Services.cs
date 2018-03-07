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
        private string _url;

        public Services(TcpClient connection)
        {
            _connectionSocket = connection;
            _url = @"C:\Users\Jonas\Documents\Tekst filer til httpStart";
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
            _url = _url + messagesplit.ElementAt(1);

            if (messagesplit.ElementAt(0) == "GET")
            {
                string answer = $"{messagesplit.ElementAt(1)}";
                // Console.WriteLine(answer);                      //bruges til at tjekke om split virkede.
                string answer1 = "HTTP/1.1 200 OK\r\n Content-Type: text/html\r\n Connection: close\r\n\r\n";

                
                    Console.WriteLine("Client: " + message);
                    sw.WriteLine(answer1);
                    message = sr.ReadLine();

                    FileStream fs = new FileStream(_url, FileMode.Open, FileAccess.Read);
                    fs.CopyTo(sw.BaseStream);
                    sw.BaseStream.Flush(); 

                Console.WriteLine("Connection Stopped");
            }

            _connectionSocket.Close();
            ns.Close();
        }
    }
}

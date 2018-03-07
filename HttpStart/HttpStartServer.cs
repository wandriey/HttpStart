﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpStart
{
    class HttpStartServer
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");   //
            TcpListener serverSocket = new TcpListener(ip, 6789);
            serverSocket.Start();

            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                //Socket connectionSocket = serverSocket.AcceptSocket();
                Console.WriteLine("Server activated");

                Services services = new Services(connectionSocket);
                // services.doIt();                                      //Vi skal ikke kalde doit metoden selv, da thread tager det som parameter


                Thread thread = new Thread(services.doIt);              //Hver gang man løber en while loopen igennem, så laves der en ny tråd.
                thread.Start();

                // Task.Run(()=>services.doIt());          //ved at bruge task, har vi en "pool" så vi genbruger threads istedet for at smide dem væk efter brug
            }
            serverSocket.Stop();
        }
    }
}
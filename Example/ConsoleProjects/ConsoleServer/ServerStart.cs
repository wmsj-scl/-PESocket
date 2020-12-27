using PENet;
using System;
using Protocol;
using System.Collections.Generic;
using Protocol.S2C;

namespace ConsoleServer
{
    class ServerStart
    {

        public static PESocket<ServerSession, NetMsg> server { get; private set; }

        static void Main(string[] args)
        {
            server = new PESocket<ServerSession, NetMsg>();
            server.StartAsServer(IPCfg.srvIP, IPCfg.srvPort);
            Console.WriteLine("\nInput 'quit' to stop server!");
            var ALL = DBHelper.TxtHelp.GetFileList(DBHelper.FileType.AccountSingle);
            Console.WriteLine(String.Join("\\n", ALL));
            while (true)
            {
                string ipt = Console.ReadLine();
                if (ipt == "quit")
                {
                    server.Close();
                    break;
                }
                if (ipt == "all")
                {
                    SendAll(new S2ACMsg() { });
                }
            }
        }

        public static void SendAll(NetMsg msg)
        {
            PETool.LogMsg("广播消息" + msg.msgType.ToString());
            List<ServerSession> sessionLst = server.GetSesstionLst();
            for (int i = 0; i < sessionLst.Count; i++)
            {
                sessionLst[i].SendMsg(msg);
            }
        }
    }
}
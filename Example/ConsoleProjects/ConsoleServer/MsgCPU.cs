using PENet;
using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer
{
    public static class MsgCPU
    {
        public static void OnReciveMsg(NetMsg msg)
        {
            PETool.LogMsg("Client Request MsgType:" + ((MsgType)msg.cmd).ToString());
            switch ((MsgType)msg.cmd)
            {
                case MsgType.RegisterAccount:
                   
                    break;
            }
        }

    }
}

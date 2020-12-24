using PENet;
using Protocol;
using Protocol.C2S;
using Protocol.S2C;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer
{
    public static class MsgCPU
    {
        public static NetMsg OnReciveMsg(NetMsg msg)
        {
            PETool.LogMsg("Client Request MsgType:" + ((MsgType)msg.cmd).ToString());
            switch ((MsgType)msg.cmd)
            {
                case MsgType.RegisterAccount:
                    var data = msg as C2SRegisterAccount;
                    PETool.LogMsg(((MsgType)msg.cmd).ToString() + data.account + data.password + data.name + data.phone);
                    DBHelper.TxtHelp.Write(data.account, string.Format("{0} {1} {2} {3}", data.account, data.password, data.name, data.phone));
                    return new S2CBase() { errorCode = ErrorCode.Succeed };
            }
            return new S2CBase() { errorCode = ErrorCode.NoExecution };
        }

    }
}

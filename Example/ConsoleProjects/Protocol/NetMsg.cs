using System;
using PENet;

namespace Protocol {
    [Serializable]
    public class NetMsg : PEMsg {
        public MsgType msgType { get; protected set; }

        public ErrorCode errorCode { get; set; }
    }


    public class IPCfg {
        public const string srvIP = "192.168.2.8";
        public const int srvPort = 17666;
    }
}
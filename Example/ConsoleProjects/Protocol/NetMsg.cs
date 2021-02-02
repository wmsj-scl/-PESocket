using System;
using PENet;

namespace Protocol {
    [Serializable]
    public class NetMsg : PEMsg {
        public MsgType msgType { get; protected set; }

        public ErrorCode errorCode { get; set; }

        public override string ToString()
        {
            return string.Format("msgType:{0},errorCode:{1}", msgType.ToString(), errorCode.ToString());
        }
    }


    public class IPCfg {
        public const string srvIP = "0.0.0.0";

        public const string srvOpenIP = "106.54.86.102";

        public const int srvPort = 17666;
    }
}
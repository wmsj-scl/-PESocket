using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CLoginAccount:S2CBase
    {
        public S2CLoginAccount()
        {
            msgType = MsgType.LoginAccount;
        }

        public CommonData.CommonAccountData data;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

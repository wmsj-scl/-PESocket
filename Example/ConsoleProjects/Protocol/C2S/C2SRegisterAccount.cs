using Protocol.CommonData;
using System;

namespace Protocol.C2S
{
    [Serializable]
    public class C2SRegisterAccount:C2SBase
    {
        public CommonAccountData comData;

        public C2SRegisterAccount()
        {
            msgType = MsgType.RegisterAccount;
        }
    }
}

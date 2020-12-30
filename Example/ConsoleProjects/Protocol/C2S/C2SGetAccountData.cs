using System;

namespace Protocol.C2S
{
    [Serializable]
    public class C2SGetAccountData:C2SBase
    {
        public C2SGetAccountData()
        {
            msgType = MsgType.GetAccountData;
        }
    }
}

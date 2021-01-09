using Protocol.CommonData;
using System.Collections.Generic;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CGetAccountData: S2CBase
    {
        public CommonAccountData comData;

        public List<BorrowInformatio> informatio;

        public S2CGetAccountData()
        {
            msgType = MsgType.GetAccountData;
        }
    }
}

using Protocol.CommonData;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CGetAccountData: S2CBase
    {
        public CommonAccountData comData;

        public S2CGetAccountData()
        {
            msgType = MsgType.GetAccountData;
        }
    }
}

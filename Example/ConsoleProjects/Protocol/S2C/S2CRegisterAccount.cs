using Protocol.CommonData;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CRegisterAccount: S2CBase
    {
        public CommonAccountData data;

        public S2CRegisterAccount()
        {
            msgType = MsgType.RegisterAccount;
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

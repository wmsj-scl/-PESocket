namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CSetAccountData : S2CBase
    {
        public S2CSetAccountData()
        {
            msgType = MsgType.SetAccountData;
        }
        public CommonData.CommonAccountData data;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

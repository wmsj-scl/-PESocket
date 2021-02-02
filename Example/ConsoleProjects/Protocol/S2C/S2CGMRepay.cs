using Protocol.CommonData;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CGMRepay : S2CBase
    {
        public S2CGMRepay()
        {
            msgType = MsgType.GMRepay;
        }

        public BorrowInformatio borrow;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

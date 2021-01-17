
using Protocol.CommonData;

namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SGMRepay : C2SBase
    {
        public C2SGMRepay()
        {
            msgType = MsgType.GMRepay;
        }


        public string otherAccount;
        public int borrowInfoId;
        public PaymentInfo PaymentInfo;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

using Protocol.CommonData;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CGMDisposeBorrow : S2CBase
    {
        public S2CGMDisposeBorrow()
        {
            msgType = MsgType.GMDisposeBorrow;
        }

        public string dipAccount;

        public BorrowInformatio borrow;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

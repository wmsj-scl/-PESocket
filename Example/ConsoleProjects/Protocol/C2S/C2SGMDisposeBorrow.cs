using Protocol.CommonData;

namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SGMDisposeBorrow : C2SBase
    {
        public C2SGMDisposeBorrow()
        {
            msgType = MsgType.GMDisposeBorrow;
        }

        public string dispAccount;
        public BorrowInformatio data;
    }
}

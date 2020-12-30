
using Protocol.CommonData;

namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SBorrowMoney : C2SBase
    {
        public C2SBorrowMoney()
        {
            msgType = MsgType.BorrowMoney;
        }

        public BorrowInformatio borrowInformatio;
    }
}

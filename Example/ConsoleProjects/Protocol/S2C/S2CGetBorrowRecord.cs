using Protocol.CommonData;
using System.Collections.Generic;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CGetBorrowRecord : S2CBase
    {
        public S2CGetBorrowRecord()
        {
            msgType = MsgType.GetBorrowRecord;
        }

        public string account;

        public List<BorrowInformatio> list = new List<BorrowInformatio>();
    }
}

namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SGetBorrowRecord:C2SBase
    {
        public string getAccount;

        public C2SGetBorrowRecord()
        {
            msgType = MsgType.GetBorrowRecord;
        }

    }
}

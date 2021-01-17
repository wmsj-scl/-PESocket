namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CBorrowMoney : S2CBase
    {
        public S2CBorrowMoney()
        {
            msgType = MsgType.BorrowMoney;
        }

        public int count;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

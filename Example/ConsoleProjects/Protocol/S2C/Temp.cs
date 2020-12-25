namespace Protocol.S2C
{
    [System.Serializable]
    public class Temp:S2CBase
    {
        public Temp()
        {
            msgType = MsgType.GetAccountData;
        }
    }
}

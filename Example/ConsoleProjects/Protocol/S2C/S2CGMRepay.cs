namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CGMRepay : S2CBase
    {
        public S2CGMRepay()
        {
            msgType = MsgType.GetAccountData;
        }
    }
}

namespace Protocol.S2C
{
    [System.Serializable]
    public class C2SLoginAccount : S2CBase
    {
        public C2SLoginAccount()
        {
            msgType = MsgType.LoginAccount;
        }

        public string account;
        public string password;
    }
}

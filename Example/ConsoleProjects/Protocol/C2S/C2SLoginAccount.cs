using Protocol.C2S;

namespace Protocol.S2C
{
    [System.Serializable]
    public class C2SLoginAccount : C2SBase
    {
        public C2SLoginAccount()
        {
            msgType = MsgType.LoginAccount;
        }

        public string password;
    }
}

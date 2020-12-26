
namespace Protocol.C2S
{
    [System.Serializable]
    public class C2STemp:C2SBase
    {
        public C2STemp()
        {
            msgType = MsgType.GetAccountData;
        }
    }
}

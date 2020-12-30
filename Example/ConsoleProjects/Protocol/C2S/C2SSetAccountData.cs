
namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SSetAccountData : C2SBase
    {
        public C2SSetAccountData()
        {
            msgType = MsgType.SetAccountData;
        }

        public CommonData.CommonAccountData data;

    }
}

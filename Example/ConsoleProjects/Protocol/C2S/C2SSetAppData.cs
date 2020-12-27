using Protocol.CommonData;

namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SSetAppData : C2SBase
    {
        public AppData appData;

        public C2SSetAppData()
        {
            msgType = MsgType.SetAppData;
        }
    }
}

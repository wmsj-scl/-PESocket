using Protocol.CommonData;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CSetAppData : S2CBase
    {
        public AppData appData;

        public S2CSetAppData()
        {
            msgType = MsgType.SetAppData;
        }

    }
}

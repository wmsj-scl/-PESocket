using Protocol.CommonData;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CGetAppData : S2CBase
    {
        public AppData appData;

        public S2CGetAppData()
        {
            msgType = MsgType.GetAppData;
        }

    }
}

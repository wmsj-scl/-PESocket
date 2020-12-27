using Protocol.CommonData;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2ACAppDataChanged:S2ACMsg
    {
        public S2ACAppDataChanged()
        {
            msgType = MsgType.AppDataChanged;
        }

        public AppData appData;
    }
}

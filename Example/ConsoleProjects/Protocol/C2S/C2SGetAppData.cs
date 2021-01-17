using Protocol.CommonData;

namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SGetAppData : C2SBase
    {
        public C2SGetAppData()
        {
            msgType = MsgType.GetAppData;
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

using System;

namespace Protocol.C2S
{
    [Serializable]
    public class C2SGetAllAccountList:C2SBase
    {
        public C2SGetAllAccountList()
        {
            msgType = MsgType.GetAllAccountList;
        }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

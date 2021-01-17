using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CGetAllAccountList:S2CBase
    {
        public S2CGetAllAccountList()
        {
            msgType = MsgType.GetAllAccountList;
        }

        public List<CommonData.CommonAccountData> accountDatas;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

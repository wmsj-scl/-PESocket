using System;
using System.Collections.Generic;
using System.Linq;

namespace Protocol.C2S
{
    [Serializable]
    public class C2SRegisterAccount:C2S.C2SBase
    {
        public string account;
        public string name;
        public string password;
        public string phone;
    }
}

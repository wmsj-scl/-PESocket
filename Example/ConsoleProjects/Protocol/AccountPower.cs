using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    public enum AccountPower
    {
        /// <summary>
        /// 普通账号
        /// </summary>
        None = 0,

        /// <summary>
        /// 普通管理员
        /// </summary>
        NoneGm = 1,

        /// <summary>
        /// 最高权限
        /// </summary>
        Gm = 2
    }
}

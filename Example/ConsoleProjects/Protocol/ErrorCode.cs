using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    public enum ErrorCode
    {
        /// <summary>
        /// 未执行
        /// </summary>
        NoExecution = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 1,

        /// <summary>
        /// 读取文件失败
        /// </summary>
        FailedReadFile = 2,

        /// <summary>
        /// 文件不存在
        /// </summary>
        FailedFileNotExists = 3,

        /// <summary>
        /// 文件已存在
        /// </summary>
        AccountExists = 4,
    }
}

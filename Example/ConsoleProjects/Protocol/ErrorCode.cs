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
        /// 账号已存在
        /// </summary>
        AccountExists = 4,

        /// <summary>
        /// 电话号码长度不够
        /// </summary>
        PhoneLengthError = 5,

        /// <summary>
        /// 账号过长
        /// </summary>
        AccountOverLength = 6,

        /// <summary>
        /// 账号过短
        /// </summary>
        AccountShort = 7,

        /// <summary>
        /// 密码过长
        /// </summary>
        PasswordOverLength = 8,

        /// <summary>
        /// 密码过短
        /// </summary>
        PasswordShort = 9,

        /// <summary>
        /// 身份证长度错误
        /// </summary>
        IDLengthError = 10,

        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError = 11,

        /// <summary>
        /// 账号不存在
        /// </summary>
        AccountNotExists = 12,
    }
}

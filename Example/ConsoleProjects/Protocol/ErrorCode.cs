using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    public enum ErrorCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Succeed = 0,
       
        /// <summary>
        /// 未执行
        /// </summary>
        NoExecution = 1,
       
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

        /// <summary>
        /// 利率设置错误
        /// </summary>
        InterestsError = 13,

        /// <summary>
        /// 贷款上限错误
        /// </summary>
        LimitOfMoneyError = 14,

        /// <summary>
        /// 分期期数上限设置失败
        /// </summary>
        NumberStagesError = 15,

        /// <summary>
        /// 借款协议title为空
        /// </summary>
        BorrowTitleError = 16,

        /// <summary>
        /// 借款协议为空
        /// </summary>
        BorrowContentError = 17,

        /// <summary>
        /// 没有权限
        /// </summary>
        AccountNoRight = 18,

        /// <summary>
        /// 应用数据还未设置 请先联系管理员设置应用数据
        /// </summary>
        AppCfgNotSet = 19,

        /// <summary>
        /// 借款金额超出上限
        /// </summary>
        OutLimitOfMoney = 20,
    }
}

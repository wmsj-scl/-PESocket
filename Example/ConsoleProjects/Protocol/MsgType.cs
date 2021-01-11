namespace Protocol
{
    public enum MsgType
    {
        /// <summary>
        /// 无效协议
        /// </summary>
        None = 0,

        /// <summary>
        /// 注册账号
        /// </summary>
        RegisterAccount = 1,

        /// <summary>
        /// 获取账号列表
        /// </summary>
        GetAllAccountList = 2,

        /// <summary>
        /// 获取账号信息
        /// </summary>
        GetAccountData = 3,

        /// <summary>
        /// 登陆账号
        /// </summary>
        LoginAccount = 4,

        /// <summary>
        /// 获取应用数据
        /// </summary>
        GetAppData = 5,

        /// <summary>
        /// 设置应用数据
        /// </summary>
        SetAppData = 6,

        /// <summary>
        /// 应用数据变化
        /// </summary>
        AppDataChanged = 7,

        /// <summary>
        /// 设置账户数据
        /// </summary>
        SetAccountData = 8,

        /// <summary>
        /// 借钱
        /// </summary>
        BorrowMoney = 9,

        /// <summary>
        /// 还钱
        /// </summary>
        RepayMoney = 10,
        GetBorrowRecord = 11,
        GMDisposeBorrow = 12,
    }
}

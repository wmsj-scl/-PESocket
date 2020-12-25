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
        GetAccountList = 2,

        /// <summary>
        /// 获取账号信息
        /// </summary>
        GetAccountData = 3,
    }
}

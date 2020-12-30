namespace Protocol.CommonData
{
    [System.Serializable]
    public struct CommonAccountData
    {
        /// <summary>
        /// 账户
        /// </summary>
        public string account;

        /// <summary>
        /// 姓名
        /// </summary>
        public string name;

        /// <summary>
        /// 密码
        /// </summary>
        public string password;

        /// <summary>
        /// 手机号
        /// </summary>
        public string phone;

        /// <summary>
        /// 身份证号
        /// </summary>
        public string id;

        /// <summary>
        /// 权限
        /// </summary>
        public AccountPower accountPower;
    }
}

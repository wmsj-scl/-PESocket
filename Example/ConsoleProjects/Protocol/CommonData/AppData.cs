namespace Protocol.CommonData
{
    [System.Serializable]
    public struct AppData
    {
        /// <summary>
        /// 贷款上限
        /// </summary>
        public float limitOfMoney;

        /// <summary>
        /// 贷款利率
        /// </summary>
        public float interests;

        /// <summary>
        /// 分期上限
        /// </summary>
        public int numberStages;

        /// <summary>
        /// 协议标题
        /// </summary>
        public string borrowTitle;

        /// <summary>
        /// 协议体
        /// </summary>
        public string borrowContent;
    }
}

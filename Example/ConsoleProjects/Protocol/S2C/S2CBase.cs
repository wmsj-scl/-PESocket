namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CBase:NetMsg
    {
        public S2CBase(MsgType msgType)
        {
            this.msgType = msgType;
        }

        public S2CBase()
        {
            this.msgType = MsgType.None;
        }
    }
}

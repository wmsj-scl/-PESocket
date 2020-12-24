namespace Protocol.S2C
{
    [System.Serializable]
    public class S2CBase:NetMsg
    {
        public ErrorCode errorCode { get { return (ErrorCode)err; } set { err = (int)value; } }
    }
}


namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SBase:NetMsg
    {
        public ErrorCode errorCode { get { return (ErrorCode)err; } set { err = (int)value; } } 
    }
}

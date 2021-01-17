
namespace Protocol.C2S
{
    [System.Serializable]
    public class C2SBase:NetMsg
    {
        public string account;

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}

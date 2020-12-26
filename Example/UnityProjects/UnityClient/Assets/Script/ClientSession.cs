using PENet;
using Protocol;

public class ClientSession : PESession<NetMsg>
{
    protected override void OnConnected()
    {
    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        NotifyManager.OnReciveMsg(msg);
    }

    protected override void OnDisConnected()
    {
    }
}

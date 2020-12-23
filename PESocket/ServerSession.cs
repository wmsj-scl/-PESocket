using PENet;


class ServerSession : PESession<PEMsg>
{
    protected override void OnConnected()
    {
    }

    protected override void OnReciveMsg(PEMsg msg)
    {
        PETool.LogMsg("Server Response:" + msg.cmd);
    }

    protected override void OnDisConnected()
    {
    }
}

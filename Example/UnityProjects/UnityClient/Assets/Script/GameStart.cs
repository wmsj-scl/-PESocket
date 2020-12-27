/************************************************************
    文件：GameStart.cs
	作者：Plane
    QQ ：1785275942
    日期：2018/10/29 5:18
	功能：PESocket客户端使用示例
*************************************************************/

using Protocol;

public class GameStart : SingleBase<GameStart>
{
    PENet.PESocket<ClientSession, NetMsg> skt = null;

    private void Start()
    {
        skt = new PENet.PESocket<ClientSession, NetMsg>();
        skt.StartAsClient(IPCfg.srvIP, IPCfg.srvPort);
    }

    public void SendMsg(NetMsg netMsg)
    {
        if (skt.session == null)
        {
            GameManager.Single.PushTextDlg.ShowText("已与服务器失去连接！");
            return;
        }
        skt.session.SendMsg(netMsg);
    }
}
/************************************************************
    文件：GameStart.cs
	作者：Plane
    QQ ：1785275942
    日期：2018/10/29 5:18
	功能：PESocket客户端使用示例
*************************************************************/

using Protocol;
using Protocol.C2S;

public class GameStart : SingleBase<GameStart>
{
    PENet.PESocket<ClientSession, NetMsg> skt = null;

    private string ipLocal = "127.0.0.1";

    private void Start()
    {
        var ip = GameManager.Single.servetType == ServetType.Local ? ipLocal : IPCfg.srvOpenIP;
        skt = new PENet.PESocket<ClientSession, NetMsg>();
        skt.StartAsClient(ip, IPCfg.srvPort);
    }

    public void SendMsg(C2SBase netMsg)
    {
        if (skt.session == null)
        {
            skt.StartAsClient(IPCfg.srvIP, IPCfg.srvPort);
            GameManager.Single.PushTextDlg.ShowText("已与服务器失去连接！正在重连...");
            return;
        }
        if (!string.IsNullOrEmpty(GameManager.Single.userData.account))
        {
            netMsg.account = GameManager.Single.userData.account;
        }

        skt.session.SendMsg(netMsg);
    }
}
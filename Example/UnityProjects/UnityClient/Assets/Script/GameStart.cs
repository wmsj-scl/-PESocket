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
        skt.session.SendMsg(netMsg);
    }


   /* private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skt.session.SendMsg(new Protocol.C2S.C2SGetAccountData()
            {
                account = "111111",
            });
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skt.session.SendMsg(new Protocol.C2S.C2SGetAccountData()
            {
                account = "111112",
            });
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skt.session.SendMsg(new Protocol.C2S.C2SGetAccountData()
            {
                account = "111113",
            });
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            skt.session.SendMsg(new Protocol.C2S.C2SRegisterAccount()
            {
                comData = {
                    account = "111111",
                    name = "SCL",
                    password = "123123",
                    phone = "12345678901",
                    id = "372925199512102338"
                },

            });
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            skt.session.SendMsg(new Protocol.C2S.C2SRegisterAccount()
            {
                comData = {
                    account = "111112",
                    name = "SCL",
                    password = "123123",
                    phone = "12345678901",
                    id = "372925199512102338"
                },

            });
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            skt.session.SendMsg(new Protocol.C2S.C2SRegisterAccount()
            {
                comData = {
                    account = "111113",
                    name = "SCL",
                    password = "123123",
                    phone = "12345678901",
                    id = "372925199512102338"
                },

            });
        }
    }*/
}
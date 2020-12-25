/************************************************************
    文件：GameStart.cs
	作者：Plane
    QQ ：1785275942
    日期：2018/10/29 5:18
	功能：PESocket客户端使用示例
*************************************************************/

using Protocol;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    PENet.PESocket<ClientSession, NetMsg> skt = null;

    private void Start()
    {
        skt = new PENet.PESocket<ClientSession, NetMsg>();
        skt.StartAsClient(IPCfg.srvIP, IPCfg.srvPort);

        skt.SetLog(true, (string msg, int lv) =>
        {
            switch (lv)
            {
                case 0:
                    msg = "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "Warn:" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:
                    msg = "Error:" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg = "Info:" + msg;
                    Debug.Log(msg);
                    break;
            }
        });
    }

    private void Update()
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
                    phone = "12345678901"
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
                    phone = "12345678901"
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
                    phone = "12345678901"
                },

            });
        }
    }
}
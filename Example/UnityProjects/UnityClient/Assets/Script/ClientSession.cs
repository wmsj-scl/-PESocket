using PENet;
using Protocol;
using Protocol.S2C;
using UnityEngine;

public class ClientSession : PESession<NetMsg>
{
    protected override void OnConnected()
    {
    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        MsgCpu.OnReciveMsg(msg);
    }

    protected override void OnDisConnected()
    {
    }
}

public static class MsgCpu
{
    public static void OnReciveMsg(NetMsg msg)
    {
        switch (msg.msgType)
        {
            case MsgType.RegisterAccount:
                var refisterData = msg as S2CBase;
                if (refisterData.errorCode != ErrorCode.Succeed)
                {
                    string message = "注册账号失败:" + refisterData.errorCode.ToString();
                    PETool.LogMsg(message, LogLevel.Error);
                }
                else
                {
                    PETool.LogMsg("RegisterAccount: " + refisterData.errorCode.ToString());
                }
                break;
            case MsgType.GetAccountData:
                var acountData = msg as S2CGetAccountData;
                if (acountData.errorCode == ErrorCode.Succeed)
                    Debug.Log(msg.msgType.ToString() + acountData.comData.account + acountData.comData.password + acountData.comData.name + acountData.comData.phone);
                else
                    PETool.LogMsg(acountData.errorCode.ToString(), LogLevel.Error);
                break;

        }
    }
}
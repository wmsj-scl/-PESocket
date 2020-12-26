using PENet;
using Protocol;
using Protocol.S2C;
using UnityEngine;

public class MsgCpu : SingleBase<MsgCpu>
{
    public void OnReciveMsg(NetMsg msg)
    {
        switch (msg.msgType)
        {
            case MsgType.LoginAccount:
                LoginAccount(msg);
                break;
            case MsgType.RegisterAccount:
                RegisterAccount(msg);
                break;
            case MsgType.GetAccountData:
                GetAccountData(msg);
                break;

        }
    }

    private void LoginAccount(NetMsg msg)
    {
        var data = msg as S2CLoginAccount;
        if (data.errorCode == ErrorCode.Succeed)
        {
            GameManager.Single.PushTextDlg.ShowText("欢迎{0}登陆");
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(data.errorCode));
        }
    }

    private void RegisterAccount(NetMsg msg)
    {
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
    }

    private void GetAccountData(NetMsg msg)
    {
        var acountData = msg as S2CGetAccountData;
        if (acountData.errorCode == ErrorCode.Succeed)
            Debug.Log(msg.msgType.ToString() + acountData.comData.account + acountData.comData.password + acountData.comData.name + acountData.comData.phone);
        else
            PETool.LogMsg(acountData.errorCode.ToString(), LogLevel.Error);
    }
}

using DBHelper;
using PENet;
using Protocol;
using Protocol.C2S;
using Protocol.S2C;
using System.IO;

public class ServerSession : PESession<NetMsg> {
    protected override void OnConnected() {
        PETool.LogMsg("Client OnLine.");
        SendMsg(new S2CBase {
            errorCode = ErrorCode.Succeed,
        });
    }

    protected override void OnReciveMsg(NetMsg msg) {
        PETool.LogMsg("Client Request:" + msg.msgType.ToString());
        SendMsg(MsgCPU.OnReciveMsg(msg));
    }

    protected override void OnDisConnected() {
        PETool.LogMsg("Client OffLine.");
    }
}

public static class MsgCPU
{
    public static NetMsg OnReciveMsg(NetMsg msg)
    {
        PETool.LogMsg("Client Request MsgType:" + msg.msgType.ToString());
        switch (msg.msgType)
        {
            case MsgType.RegisterAccount:
                return RegisterAccount(msg);
            case MsgType.GetAccountData:
                return GetAccountData(msg);
        }
        return new S2CBase(msg.msgType) { errorCode = ErrorCode.NoExecution };
    }

    private static NetMsg GetAccountData(NetMsg msg)
    {
        var dataC2SGetAccountData = msg as C2SGetAccountData;
        ErrorCode errorCode;
        var redByte = TxtHelp.Read(FileType.AccountSingle, dataC2SGetAccountData.account, out errorCode);
        if (errorCode == ErrorCode.Succeed)
        {
            var readC2SRegisterAccount = PETool.DeSerialize<C2SRegisterAccount>(redByte);
            return new S2CGetAccountData() { errorCode = errorCode, comData = readC2SRegisterAccount.comData };
        }
        else
        {
            return new S2CGetAccountData() { errorCode = errorCode };
        }
    }

    private static NetMsg RegisterAccount(NetMsg msg)
    {
        var dataC2SRegisterAccount = msg as C2SRegisterAccount;
        PETool.LogMsg(msg.msgType.ToString() + dataC2SRegisterAccount.comData.account + dataC2SRegisterAccount.comData.password + dataC2SRegisterAccount.comData.name + dataC2SRegisterAccount.comData.phone);

        if (File.Exists(TxtHelp.GetPath(FileType.AccountSingle, dataC2SRegisterAccount.comData.account)))
        {
            return new S2CBase(msg.msgType) { errorCode = ErrorCode.AccountExists };
        }
        else
        {
            TxtHelp.Write(FileType.AccountSingle, dataC2SRegisterAccount.comData.account, PETool.Serialize(dataC2SRegisterAccount));
            return new S2CBase(msg.msgType) { errorCode = ErrorCode.Succeed };
        }
    }
}

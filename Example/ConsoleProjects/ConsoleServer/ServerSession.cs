using ConsoleServer;
using DBHelper;
using PENet;
using PESocket;
using Protocol;
using Protocol.C2S;
using Protocol.CommonData;
using Protocol.S2C;
using System;
using System.Collections.Generic;
using System.IO;

public class ServerSession : PESession<NetMsg>
{
    protected override void OnConnected()
    {
        PETool.LogMsg("Client OnLine.");
        SendMsg(new S2CBase
        {
            errorCode = ErrorCode.Succeed,
        });
    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        var data = MsgCPU.OnReciveMsg(msg);
        PETool.LogMsg(string.Format("Send Client:{0}", data.ToString()));
        SendMsg(data);
    }

    protected override void OnDisConnected()
    {
        PETool.LogMsg("Client OffLine.");
    }
}

public static class MsgCPU
{


    public static NetMsg OnReciveMsg(NetMsg msg)
    {
        PETool.LogMsg("Client Request:" + msg.ToString());
        switch (msg.msgType)
        {
            case MsgType.GetBorrowRecord:
                return GetBorrowRecord(msg);
            case MsgType.BorrowMoney:
                return BorrowMoney(msg);
            case MsgType.RegisterAccount:
                return RegisterAccount(msg);
            case MsgType.GetAccountData:
                return GetAccountData(msg);
            case MsgType.LoginAccount:
                return LoginAccount(msg);
            case MsgType.GetAppData:
                return GetAppData(msg);
            case MsgType.SetAppData:
                return SetAppData(msg);
            case MsgType.GetAllAccountList:
                return GetAllAccountList(msg);
            case MsgType.SetAccountData:
                return SetAccountData(msg);
            case MsgType.GMDisposeBorrow:
                return GMDisposeBorrow(msg);
            case MsgType.GMRepay:
                return GMRepay(msg);
        }
        return new S2CBase(msg.msgType) { errorCode = ErrorCode.NoExecution };
    }

    private static NetMsg GMRepay(NetMsg msg)
    {
        var data = msg as C2SGMRepay;
        ErrorCode code = TxtHelp.CheckAccountPower(data.account, new AccountPower[] { AccountPower.Gm, AccountPower.NoneGm });
        if (code == ErrorCode.Succeed)
        {
            var bytes = TxtHelp.Read(FileType.MoneyInfo, data.otherAccount, out code, 1024 * 1024);
            BorrowInformatioSave seve;
            if (code == ErrorCode.Succeed)
            {
                seve = PETool.DeSerialize<BorrowInformatioSave>(bytes);
                for (int i = 0; i < seve.borrows.Count; i++)
                {
                    if (seve.borrows[i].id == data.borrowInfoId)
                    {
                        data.PaymentInfo.id = seve.borrows[i].paymentInfos.Count + 1;
                        data.PaymentInfo.dateTime = GetCurTime();
                        seve.borrows[i].paymentInfos.Add(data.PaymentInfo);
                        TxtHelp.Write(FileType.MoneyInfo, data.otherAccount, PETool.Serialize(seve));
                        return new S2CGMRepay() { errorCode = ErrorCode.Succeed };
                    }
                }
            }
        }

        return new S2CGMRepay() { errorCode = code };
    }

    private static NetMsg GMDisposeBorrow(NetMsg msg)
    {
        var data = msg as C2SGMDisposeBorrow;
        ErrorCode errorCode = TxtHelp.CheckAccountPower(data.account, new AccountPower[] { AccountPower.Gm, AccountPower.NoneGm });
        if (errorCode == ErrorCode.Succeed)
        {
            var bytes = TxtHelp.Read(FileType.MoneyInfo, data.dispAccount, out errorCode, 1024 * 1024);
            BorrowInformatioSave seve;
            if (errorCode == ErrorCode.Succeed)
            {
                seve = PETool.DeSerialize<BorrowInformatioSave>(bytes);
                for (int i = 0; i < seve.borrows.Count; i++)
                {
                    if (seve.borrows[i].id == data.data.id)
                    {
                        seve.borrows[i] = data.data;
                        TxtHelp.Write(FileType.MoneyInfo, data.dispAccount, PETool.Serialize(seve));
                        return new S2CGMDisposeBorrow() { errorCode = ErrorCode.Succeed, dipAccount = data.dispAccount, borrow = data.data };
                    }
                }
            }
        }
        return new S2CGMDisposeBorrow() { errorCode = errorCode };
    }

    private static NetMsg GetBorrowRecord(NetMsg msg)
    {
        var data = msg as C2SGetBorrowRecord;
        ErrorCode errorCode;
        var bytes = TxtHelp.Read(FileType.MoneyInfo, data.getAccount, out errorCode, 1024 * 1024);
        BorrowInformatioSave seve;
        if (errorCode == ErrorCode.Succeed)
            seve = PETool.DeSerialize<BorrowInformatioSave>(bytes);
        else
            seve = new BorrowInformatioSave();
        return new S2CGetBorrowRecord() { errorCode = errorCode, list = seve.borrows };
    }

    private static NetMsg BorrowMoney(NetMsg msg)
    {
        var data = msg as C2SBorrowMoney;
        var errorCode = ErrorCode.Succeed;
        var C2SGetAppData = GetTxtAppData(out errorCode);
        if (errorCode == ErrorCode.Succeed)
        {
            data.borrowInformatio.account = data.account;
            if (data.borrowInformatio.allMoney > C2SGetAppData.appData.limitOfMoney)
            {
                return new S2CBorrowMoney() { errorCode = ErrorCode.OutLimitOfMoney };
            }

            data.borrowInformatio.rateInterest = C2SGetAppData.appData.interests;
            data.borrowInformatio.dateTime = GetCurTime();
            var bytes = TxtHelp.Read(FileType.MoneyInfo, data.account, out errorCode, 1024 * 1024);
            BorrowInformatioSave seve;
            if (errorCode == ErrorCode.Succeed)
                seve = PETool.DeSerialize<BorrowInformatioSave>(bytes);
            else
                seve = new BorrowInformatioSave();
            data.borrowInformatio.id = seve.borrows.Count + 1;
            seve.borrows.Add(data.borrowInformatio);
            TxtHelp.Write(FileType.MoneyInfo, data.account, PETool.Serialize(seve));

            return new S2CBorrowMoney()
            {
                errorCode = ErrorCode.Succeed,
                count = seve.borrows.Count
            };
        }
        else if (errorCode == ErrorCode.FailedFileNotExists)
        {
            errorCode = ErrorCode.AppCfgNotSet;
        }

        return new S2CBorrowMoney() { errorCode = errorCode };
    }

    private static NetMsg SetAccountData(NetMsg msg)
    {
        var data = msg as C2SSetAccountData;
        ErrorCode errorCode = TxtHelp.CheckAccountPower(data.account, new AccountPower[] { AccountPower.Gm, AccountPower.NoneGm });
        if (errorCode == ErrorCode.Succeed)
        {
            WriteOrCoverAccount(new C2SRegisterAccount() { comData = data.data });
            return new S2CSetAccountData() { data = data.data };
        }

        return new S2CSetAccountData() { errorCode = errorCode };
    }

    private static NetMsg GetAllAccountList(NetMsg msg)
    {
        var data = msg as C2SGetAllAccountList;
        ErrorCode errorCode;
        errorCode = TxtHelp.CheckAccountPower(data.account, new AccountPower[] { AccountPower.Gm });
        if (errorCode == ErrorCode.Succeed)
        {
            var all = TxtHelp.GetFileList(FileType.AccountSingle);
            List<CommonAccountData> list = new List<CommonAccountData>();
            byte[] byteData;
            for (int i = 0; i < all.Length; i++)
            {
                byteData = TxtHelp.ReadByPath(all[i], out errorCode);
                if (errorCode == ErrorCode.Succeed)
                {
                    var accountData = PETool.DeSerialize<C2SRegisterAccount>(byteData);
                    list.Add(accountData.comData);
                }
            }

            return new S2CGetAllAccountList() { accountDatas = list, errorCode = ErrorCode.Succeed };
        }
        return new S2CLoginAccount() { errorCode = errorCode };
    }

    private static NetMsg SetAppData(NetMsg msg)
    {
        var data = msg as C2SSetAppData;
        TxtHelp.Write(FileType.AppData, FileType.AppData.ToString(), PETool.Serialize(data));
        ServerStart.SendAll(new S2ACAppDataChanged() { appData = data.appData });
        return new S2CSetAppData() { errorCode = ErrorCode.Succeed, appData = data.appData };
    }

    private static NetMsg GetAppData(NetMsg msg)
    {
        var errorCode = ErrorCode.Succeed;
        var C2SGetAppData = GetTxtAppData(out errorCode);

        if (ErrorCode.Succeed == errorCode)
        {
            return new S2CGetAppData() { errorCode = errorCode, appData = C2SGetAppData.appData };
        }

        return new S2CGetAppData() { errorCode = errorCode };
    }

    private static NetMsg LoginAccount(NetMsg msg)
    {
        var data = msg as C2SLoginAccount;
        ErrorCode errorCode;
        var redByte = TxtHelp.Read(FileType.AccountSingle, data.account, out errorCode);
        if (errorCode == ErrorCode.Succeed)
        {
            var readC2SRegisterAccount = PETool.DeSerialize<C2SRegisterAccount>(redByte);
            if (readC2SRegisterAccount.comData.password != data.password)
            {
                return new S2CLoginAccount() { errorCode = ErrorCode.PasswordError };
            }
            else
            {
                return new S2CLoginAccount()
                {
                    errorCode = ErrorCode.Succeed,
                    data = readC2SRegisterAccount.comData,
                };
            }
        }
        else if (errorCode == ErrorCode.FailedFileNotExists)
        {
            return new S2CLoginAccount() { errorCode = ErrorCode.AccountNotExists };
        }
        return new S2CLoginAccount() { errorCode = errorCode };
    }

    private static NetMsg GetAccountData(NetMsg msg)
    {
        var dataC2SGetAccountData = msg as C2SGetAccountData;
        ErrorCode errorCode;
        var redByte = TxtHelp.Read(FileType.AccountSingle, dataC2SGetAccountData.account, out errorCode);
        if (errorCode == ErrorCode.Succeed)
        {
            var readC2SRegisterAccount = PETool.DeSerialize<C2SRegisterAccount>(redByte);
            List<BorrowInformatio> informatio;
            redByte = TxtHelp.Read(FileType.MoneyInfo, dataC2SGetAccountData.account, out errorCode);
            if (errorCode == ErrorCode.Succeed)
            {
                informatio = PETool.DeSerialize<BorrowInformatioSave>(redByte).borrows;
            }
            else
            {
                informatio = new List<BorrowInformatio>();
            }
            return new S2CGetAccountData() { errorCode = errorCode, comData = readC2SRegisterAccount.comData, informatio = informatio };
        }
        else
        {
            return new S2CGetAccountData() { errorCode = errorCode };
        }
    }

    private static NetMsg RegisterAccount(NetMsg msg)
    {
        var dataC2SRegisterAccount = msg as C2SRegisterAccount;

        ErrorCode errorCode = CommonCheckMsg.CheckRegisterAccount(dataC2SRegisterAccount);
        if (errorCode == ErrorCode.Succeed)
        {
            PETool.LogMsg(msg.msgType.ToString() + dataC2SRegisterAccount.comData.account + dataC2SRegisterAccount.comData.password + dataC2SRegisterAccount.comData.name + dataC2SRegisterAccount.comData.phone);

            if (File.Exists(TxtHelp.GetPath(FileType.AccountSingle, dataC2SRegisterAccount.comData.account)))
            {
                return new S2CBase(msg.msgType) { errorCode = ErrorCode.AccountExists };
            }
            else
            {
                WriteOrCoverAccount(dataC2SRegisterAccount);
                return new S2CRegisterAccount() { errorCode = ErrorCode.Succeed, data = dataC2SRegisterAccount.comData };
            }
        }
        else
        {
            return new S2CBase(msg.msgType) { errorCode = errorCode };
        }
    }

    private static C2SSetAppData GetTxtAppData(out ErrorCode errorCode)
    {
        var data = TxtHelp.Read(FileType.AppData, FileType.AppData.ToString(), out errorCode);

        if (ErrorCode.Succeed == errorCode)
        {
            return PETool.DeSerialize<C2SSetAppData>(data); ;
        }
        return new C2SSetAppData();
    }

    private static void WriteOrCoverAccount(C2SRegisterAccount data)
    {
        if (data.comData.account.Equals(AppCost.GmAccount))
        {
            data.comData.accountPower = AccountPower.Gm;
        }
        TxtHelp.Write(FileType.AccountSingle, data.comData.account, PETool.Serialize(data));
    }

    public static int GetCurTime()
    {

        return TimeHelper.GetTimeStamp(DateTime.Now);
    }

}

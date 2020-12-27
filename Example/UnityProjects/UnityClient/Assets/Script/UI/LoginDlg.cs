using Protocol;
using Protocol.C2S;
using Protocol.S2C;
using UnityEngine;
using UnityEngine.UI;

public class LoginDlg : SingleBase<LoginDlg>
{
    public Button btnLogin;
    public Button btnRegister;

    public InputField inputFieldAccount;
    public InputField inputFieldPassword;

    private void Start()
    {
        NotifyManager.AddNotify(MsgType.AppDataChanged, AppDataChanged);
        NotifyManager.AddNotify(MsgType.LoginAccount, LoginAccount);
        NotifyManager.AddNotify(MsgType.GetAppData, GetAppData);
        btnLogin.onClick.AddListener(Login);
        btnRegister.onClick.AddListener(Register);
    }

    private void AppDataChanged(NetMsg msg)
    {
        var data = msg as S2ACAppDataChanged;
        GameManager.Single.appData = data.appData;
        if (GameManager.Single.MainDlg.GetDlg.gameObject.activeSelf)
        {
            GameManager.Single.MainDlg.GetDlg.UpdateUIData();
        }
    }

    private void GetAppData(NetMsg msg)
    {
        var data = msg as S2CGetAppData;
        if (data.errorCode == ErrorCode.Succeed)
        {
            GameManager.Single.appData = data.appData;
            GameManager.Single.PushTextDlg.ShowText("欢迎{0}登陆", new string[] { GameManager.Single.userData.name });
            GameManager.Single.MainDlg.Show();
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(data.errorCode));
        }

    }

    private static void LoginAccount(NetMsg msg)
    {
        var data = msg as S2CLoginAccount;
        if (data.errorCode == ErrorCode.Succeed)
        {
            GameManager.Single.userData = data.data;
            GameManager.Single.GameStart.SendMsg(new C2SGetAppData() { });
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(data.errorCode));
        }
    }

    private void Login()
    {
        var errCode = CommonCheckMsg.CheckAccount(inputFieldAccount.text);
        if (errCode  != ErrorCode.Succeed)
        {
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(errCode));
            return;
        }
        errCode = CommonCheckMsg.CheckPassword(inputFieldPassword.text);
        if (errCode != ErrorCode.Succeed)
        {
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(errCode));
            return;
        }
        GameStart.Single.SendMsg(new C2SLoginAccount()
        {
            account = inputFieldAccount.text,
            password = inputFieldPassword.text,
        });
    }

    private void Register()
    {
        GameManager.Single.registerAccountDlg.gameObject.SetActive(true);
    }
}

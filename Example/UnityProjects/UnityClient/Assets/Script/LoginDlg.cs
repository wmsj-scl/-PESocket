using Protocol;
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
        NotifyManager.AddNotify(MsgType.LoginAccount, LoginAccount);
        btnLogin.onClick.AddListener(Login);
        btnRegister.onClick.AddListener(Register);
    }

    private static void LoginAccount(NetMsg msg)
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

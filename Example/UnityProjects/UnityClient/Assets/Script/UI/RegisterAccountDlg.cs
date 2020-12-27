using PENet;
using Protocol;
using Protocol.C2S;
using Protocol.S2C;
using UnityEngine;
using UnityEngine.UI;

public class RegisterAccountDlg : MonoBehaviour
{
    public Button btnClose;
    public Button btnRegister;

    public InputField inputFieldAccount;
    public InputField inputFieldPassword;
    public InputField inputFieldName;
    public InputField inputFieldPhone;
    public InputField inputFieldID;

    private void Start()
    {
        NotifyManager.AddNotify(MsgType.RegisterAccount, OnRegisterAccount);
        btnClose.onClick.AddListener(() => { gameObject.SetActive(false); });
        btnRegister.onClick.AddListener(() => {
            var err = CommonCheckMsg.CheckAccount(inputFieldAccount.text);
            if (err != ErrorCode.Succeed)
            {
                PETool.LogMsg(ErrorStr.GetErrorStr(err), LogLevel.Error);
            }
            err = CommonCheckMsg.CheckPassword(inputFieldPassword.text);
            if (err != ErrorCode.Succeed)
            {
                PETool.LogMsg(ErrorStr.GetErrorStr(err), LogLevel.Error);
            }
            err = CommonCheckMsg.CheckPhone(inputFieldPhone.text);
            if (err != ErrorCode.Succeed)
            {
                PETool.LogMsg(ErrorStr.GetErrorStr(err), LogLevel.Error);
            }
            err = CommonCheckMsg.CheckID(inputFieldID.text);
            if (err != ErrorCode.Succeed)
            {
                PETool.LogMsg(ErrorStr.GetErrorStr(err), LogLevel.Error);
            }

            var data = new C2SRegisterAccount()
            {
                comData = {
                    account = inputFieldAccount.text,
                    name = inputFieldName.text,
                    password = inputFieldPassword.text,
                    phone = inputFieldPhone.text,
                    id = inputFieldID.text
                },
            };

           

            GameStart.Single.SendMsg(data);
        });
    }

    private void OnRegisterAccount(NetMsg netMsg)
    {
        if (netMsg.errorCode == ErrorCode.Succeed)
        {
            var data = (netMsg as S2CRegisterAccount);
            GameManager.Single.userData = data.data;
            GameManager.Single.GameStart.SendMsg(new C2SGetAppData() { });           
        }
        else
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(netMsg.errorCode));
    }

}

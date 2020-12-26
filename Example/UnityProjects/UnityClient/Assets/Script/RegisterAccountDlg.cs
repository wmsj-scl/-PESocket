using PENet;
using Protocol;
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

            var data = new Protocol.C2S.C2SRegisterAccount()
            {
                comData = {
                    account = inputFieldAccount.text,
                    name = inputFieldName.text,
                    password = inputFieldPassword.text,
                    phone = inputFieldPhone.text,
                    id = inputFieldID.text
                },
            };

            NotifyManager.AddNotify(data.msgType, OnRegisterAccount);

            GameStart.Single.SendMsg(data);
        });
    }

    private void OnRegisterAccount(NetMsg netMsg)
    {

    }

}

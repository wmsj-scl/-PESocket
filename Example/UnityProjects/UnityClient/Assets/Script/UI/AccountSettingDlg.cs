using Protocol;
using Protocol.CommonData;
using Protocol.S2C;
using UnityEngine;
using UnityEngine.UI;

public class AccountSettingDlg : MonoBehaviour
{
    public InputField inputFieldAccount;
    public InputField inputFieldPassword;
    public InputField inputFieldName;
    public InputField inputFieldPhone;
    public InputField inputFieldID;

    public Button btnSetting;

    public CommonAccountData curData { get; private set; }

    void Start()
    {
        NotifyManager.AddNotify(MsgType.SetAccountData, OnSetAccountData);
        btnSetting.onClick.AddListener(SetAccountData);
    }

    private void OnSetAccountData(NetMsg msg)
    {
        if (msg.errorCode == ErrorCode.Succeed)
        {
            var data = msg as S2CSetAccountData;
            Show(data.data);
            GameManager.Single.PushTextDlg.ShowText("设置成功！");
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(msg.errorCode));
        }
    }

    public void Show(CommonAccountData data)
    {
        curData = data;
        gameObject.SetActive(true);
        inputFieldAccount.text = data.account.ToString();
        inputFieldPassword.text = data.password.ToString();
        inputFieldName.text = data.name.ToString();
        inputFieldPhone.text = data.phone.ToString();
        inputFieldID.text = data.id.ToString();
    }

    private void SetAccountData()
    {
        var data = new CommonAccountData()
        {
            account = curData.account,
            accountPower = curData.accountPower,
            password = inputFieldPassword.text,
            phone = inputFieldPhone.text,
            id = inputFieldID.text,
            name = inputFieldName.text
        };

        ErrorCode errorCode = CommonCheckMsg.CheckRegisterAccount(data);
        if (ErrorCode.Succeed == errorCode)
        {
            GameManager.Single.GameStart.SendMsg(new Protocol.C2S.C2SSetAccountData()
            {
                account = GameManager.Single.userData.account,
                data = data,
            });
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText(errorCode);
        }
    }
}

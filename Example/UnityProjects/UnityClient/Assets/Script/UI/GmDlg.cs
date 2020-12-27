using Protocol;
using Protocol.C2S;
using Protocol.CommonData;
using Protocol.S2C;
using UnityEngine;
using UnityEngine.UI;

public class GmDlg : MonoBehaviour
{
    public InputField InputFieldLimitOfMoney;
    public InputField InputIinterests;
    public InputField InputFieldMumberStages;
    public InputField InputFieldBorrowTitle;
    public InputField InputFieldBorrowContent;

    public Button btnSetting;
    public Button btnReturn;

    void Start()
    {
        NotifyManager.AddNotify(MsgType.SetAppData, OnSetAppData);
        btnSetting.onClick.AddListener(Setting);
        btnReturn.onClick.AddListener(() => { gameObject.SetActive(false); });
    }

    private void OnSetAppData(NetMsg msg)
    {
        var data = msg as S2CSetAppData;
        GameManager.Single.appData = data.appData;
        GameManager.Single.PushTextDlg.ShowText("设置成功！");
    }

    private void Setting()
    {
        var data = new AppData();
        float flo = 0f;
        int i = 0;
        if (float.TryParse(InputFieldLimitOfMoney.text, out flo))
        {
            data.limitOfMoney = flo;
        }
        if (float.TryParse(InputIinterests.text, out flo))
        {
            data.interests = flo;
        }
        if (int.TryParse(InputFieldMumberStages.text, out i))
        {
            data.numberStages = i;
        }
        data.borrowTitle = InputFieldBorrowTitle.text;
        data.borrowContent = InputFieldBorrowContent.text;

        var err = CommonCheckMsg.CheckAppData(data);
        if (err == ErrorCode.Succeed)
            GameManager.Single.GameStart.SendMsg(new C2SSetAppData() { appData = data });
        else
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(err));
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
}

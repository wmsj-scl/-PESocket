using System;
using Protocol;
using Protocol.C2S;
using Protocol.S2C;
using UnityEngine;
using UnityEngine.UI;

public class BorrowDlg : MonoBehaviour
{
    public Toggle toggle;
    public Text textProtocolTitle;
    public Button btnBorrowMoney;
    public Button btnClose;
    public Transform protocolContent;
    public Text textProtocol;

    public InputField inputFieldMoney;
    public InputField inputFieldStages;

    public Button btnAgree;
    public Button btnCancel;

    void Start()
    {
        NotifyManager.AddNotify(MsgType.BorrowMoney, OnBorrowMoney);
        textProtocolTitle.text = GameManager.Single.appData.borrowTitle;
        protocolContent.gameObject.SetActive(false);
        textProtocol.text = GameManager.Single.appData.borrowContent;
        btnClose.onClick.AddListener(() => { gameObject.SetActive(false); toggle.isOn = false; });
        btnBorrowMoney.onClick.AddListener(BorromMoney);
        toggle.onValueChanged.AddListener(OnToggleChanged);
        btnAgree.onClick.AddListener(() => { protocolContent.gameObject.SetActive(false); });
        btnCancel.onClick.AddListener(() => { protocolContent.gameObject.SetActive(false); toggle.isOn = false; });
    }

    private void OnBorrowMoney(NetMsg s)
    {
        if (s.errorCode == ErrorCode.Succeed)
        {
            var data = s as S2CBorrowMoney;
            GameManager.Single.PushTextDlg.ShowText(string.Format("借款成功，当前是第{0}笔借款",data.count));

            inputFieldMoney.text = "";
            inputFieldStages.text = "";
            toggle.isOn = false;

        }
        else
            GameManager.Single.PushTextDlg.ShowText(s.errorCode);
    }

    private void BorromMoney()
    {
        int money = 0;
        int stages = 0;
        if (!toggle.isOn)
        {
            GameManager.Single.PushTextDlg.ShowText("请先阅读并同意借款协议！");
            return;
        }
        if (!int.TryParse(inputFieldMoney.text, out money))
        {
            GameManager.Single.PushTextDlg.ShowText("借款不可为空！");
            return;
        }

        if (!int.TryParse(inputFieldStages.text, out stages))
        {
            GameManager.Single.PushTextDlg.ShowText("分期数量不对！");
            return;
        }

        if (money > GameManager.Single.appData.limitOfMoney)
        {
            GameManager.Single.PushTextDlg.ShowText("借款金额超出上限！ 上限：" + GameManager.Single.appData.limitOfMoney);
            return;
        }
        var err = CommonCheckMsg.CheckStages(stages);
        if (err != ErrorCode.Succeed)
        {
            GameManager.Single.PushTextDlg.ShowText(err);
            return;
        }

        var data = new C2SBorrowMoney() { borrowInformatio = new Protocol.CommonData.BorrowInformatio() {
            allMoney = money,
            stagesNumber = stages,
        } };
        GameManager.Single.GameStart.SendMsg(data);
    }

    private void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            protocolContent.gameObject.SetActive(true);
        }
    }
}

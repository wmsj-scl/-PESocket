using System;
using Protocol;
using Protocol.S2C;
using Protocol.CommonData;
using UnityEngine;
using UnityEngine.UI;

public class GmAccountSetBorrowDlg : MonoBehaviour
{
    public Text TextInfo;

    public Transform CheckPanel;
    public Transform Content;

    public Button BtnCancel;
    public Button BtnPass;

    public InputField AllMoney;
    public InputField Principal;
    public InputField Interest;

    public Button BtnRepay;
    public Button BtnClosey;

    private bool isStart = false;

    private CommonAccountData account;

    private BorrowInformatio informatio;

    private void Start()
    {
        NotifyManager.AddNotify(Protocol.MsgType.GMDisposeBorrow, GMDisposeBorrow);

        try
        {
            BtnCancel.onClick.AddListener(OnCancel);
            BtnPass.onClick.AddListener(OnPass);
            BtnRepay.onClick.AddListener(OnRepay);
            BtnClosey.onClick.AddListener(() => { gameObject.SetActive(false); });
            gameObject.SetActive(false);
            if (!isStart)
            {
                isStart = true;
                Show(account, informatio, "");
            }

        }
        catch { }
    }

    private void GMDisposeBorrow(NetMsg s)
    {
        var data = s as S2CGMDisposeBorrow;
        if (data.errorCode == ErrorCode.Succeed)
        {
            informatio.borrowState = data.borrow.borrowState;
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText(data.errorCode);
        }
    }

    public void OnRepay()
    {
        Debug.Log(AllMoney.text);
    }

    public void OnCancel()
    {
        informatio.borrowState = BorrowState.Refuse;
        GameManager.Single.GameStart.SendMsg(new Protocol.C2S.C2SGMDisposeBorrow() {dispAccount = account.account,data = informatio });
    }

    public void OnPass()
    {
        informatio.borrowState = BorrowState.Approved;
        GameManager.Single.GameStart.SendMsg(new Protocol.C2S.C2SGMDisposeBorrow() { dispAccount = account.account, data = informatio });
    }

    public void Show(CommonAccountData account, BorrowInformatio informatio, string text)
    {
        this.account = account;
        this.informatio = informatio;
        if (!isStart)
        {
            Start();
            return;
        }

        gameObject.SetActive(true);
        if (!string.IsNullOrEmpty(text))
            TextInfo.text = text;
        Content.gameObject.SetActive(false);
        Content.gameObject.SetActive(true);
    }
}

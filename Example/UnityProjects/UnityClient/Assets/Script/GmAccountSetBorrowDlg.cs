using System;
using Protocol;
using Protocol.S2C;
using Protocol.CommonData;
using UnityEngine;
using UnityEngine.UI;
using Protocol.C2S;

public class GmAccountSetBorrowDlg : MonoBehaviour
{
    public Text TextInfo;

    public Transform CheckPanel;
    public Transform SetBorrowContent;
    public Transform Content;

    public Button BtnCancel;
    public Button BtnPass;

    public InputField AllMoney;
    public InputField Principal;
    public InputField Interest;

    public Button BtnRepay;
    public Button BtnClosey;

    private bool isStart = false;

    private int repayTimer;

    private CommonAccountData account;

    private BorrowInformatio informatio;

    private void Start()
    {
        NotifyManager.AddNotify(MsgType.GMDisposeBorrow, GMDisposeBorrow);


        try
        {
            AllMoney.onValueChanged.AddListener(onValueChanged);
            BtnCancel.onClick.AddListener(OnCancel);
            BtnPass.onClick.AddListener(OnPass);
            BtnRepay.onClick.AddListener(OnRepay);
            BtnClosey.onClick.AddListener(() => { gameObject.SetActive(false); });
            gameObject.SetActive(false);
            if (!isStart)
            {
                isStart = true;
                Show(account, informatio);
            }

        }
        catch { }
    }

    private void GMDisposeBorrow(NetMsg s)
    {
        var data = s as S2CGMDisposeBorrow;
        if (data.errorCode == ErrorCode.Succeed)
        {
            informatio = data.borrow;
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText(data.errorCode);
        }
    }

    private void onValueChanged(string arg0)
    {
        if (string.IsNullOrEmpty(arg0))
            return;
        float repayedCount = 0;
        for (int i = 0; i < informatio.paymentInfos.Count; i++)
        {
            repayedCount += informatio.paymentInfos[i].principal;
        }

        var allMoney = float.Parse(AllMoney.text);
        var interestMoney = (informatio.allMoney - repayedCount) * informatio.rateInterest;
        var principal =allMoney - interestMoney;
        Interest.text = interestMoney.ToString();
        Principal.text = principal.ToString();
    }

    public void OnRepay()
    {
        repayTimer += 1;
        if (repayTimer % 2 == 0)
        {
            var allMoney = float.Parse(AllMoney.text);
            var principal = float.Parse(Principal.text);
            var interestMoney = float.Parse(Interest.text);
            GameManager.Single.GameStart.SendMsg(new C2SGMRepay()
            {
                otherAccount = informatio.account,
                borrowInfoId = informatio.id,
                PaymentInfo = new PaymentInfo()
                {
                    allMoney = allMoney,
                    interestMoney = interestMoney,
                    principal = principal,
                }
            });
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText("按第二次 确定还款！ 请确认金额后再次点击！");
        }
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

    public void Show(CommonAccountData account, BorrowInformatio informatio)
    {
        this.account = account;
        this.informatio = informatio;
        if (!isStart)
        {
            Start();
            return;
        }

        gameObject.SetActive(true);

        TextInfo.text = BorrowRecordItem.GetBorrowInformatioStr(informatio);

        Content.gameObject.SetActive(false);
        Content.gameObject.SetActive(true);
        SetBorrowContent.gameObject.SetActive(informatio.borrowState == BorrowState.Approved);
        CheckPanel.gameObject.SetActive(informatio.borrowState == BorrowState.WaitApproval);
    }
}

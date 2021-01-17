using PESocket;
using Protocol.C2S;
using Protocol.CommonData;
using UnityEngine;
using UnityEngine.UI;

public class GMSetRepaymentDlg : MonoBehaviour
{
    private string format = "{0}日:借款{1},利率:{2}%,共分:{3}期,剩余待还本金{4}";

    private CommonAccountData account;

    private BorrowInformatio informatio;

    public Text Info;

    public InputField RepayNumber;
    public InputField Interest;
    public InputField Principal;

    public Button BtnRepay;
    public Button BtnClose;

    private int repayTimer;

    public void Start()
    {
        BtnRepay.onClick.AddListener(onRepayClick);
        BtnClose.onClick.AddListener(onCloseClick);
        RepayNumber.onValueChanged.AddListener(onValueChanged);
    }

    private void onValueChanged(string arg0)
    {
        var allMoney = float.Parse(RepayNumber.text);
        var principal = allMoney / (1 + informatio.rateInterest) * informatio.rateInterest;
        var interestMoney = allMoney / (1 + informatio.rateInterest);
        Interest.text = Interest.ToString();
        Principal.text = Principal.ToString();
    }

    private void onCloseClick()
    {
        gameObject.SetActive(false);
    }

    private void onRepayClick()
    {
        repayTimer += 1;
        if (repayTimer % 2 == 0)
        {
            var allMoney = float.Parse(RepayNumber.text);
            var principal = allMoney / (1 + informatio.rateInterest) * informatio.rateInterest;
            var interestMoney = allMoney / (1 + informatio.rateInterest);
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

    public void SetErpayment(CommonAccountData account, BorrowInformatio data)
    {
        gameObject.SetActive(true);
        repayTimer = 0;
        this.account = account;
        this.informatio = data;
        Info.text = string.Format(format, TimeHelper.GetDateTime(data.dateTime).ToShortDateString(), data.allMoney, data.rateInterest * 100, data.stagesNumber, data.account);
    }





}

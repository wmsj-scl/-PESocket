using PESocket;
using Protocol.CommonData;
using UnityEngine;
using UnityEngine.UI;

public enum BorrowRecordType
{
    SELF = 0,
    GM = 2,
}

public static class BorrowRecordStr
{
    public static string format = "{0}日:借款{1},利率:{2}%,共分:{3}期,已还本金{4},已还利息{5}，已还期数{6}，剩余本金{7}， {8}";
    public static string formatWait = "{0}日:借款{1},利率:{2}%,共分:{3}期,<color=green>{4}</color>";
    public static string formatRepay = "本期应还:{0},其中本金：{1},利息：{2},手续费：{3}";
}

public class BorrowRecordItem : MonoBehaviour
{
    public BorrowRecordType recordType;

    private Button btnItemClick;
    private Text text;
  
    private BorrowInformatio data;
    private CommonAccountData accountData;
    void Start()
    {
        btnItemClick = GetComponent<Button>();
        text = GetComponent<Text>();
        btnItemClick.onClick.AddListener(() =>
        { // todo
            switch (recordType)
            {
                case BorrowRecordType.SELF:
                    GameManager.Single.MainDlg.MyDlg.RepayContent.gameObject.SetActive(true);
                    GameManager.Single.MainDlg.MyDlg.TextNumber.text = string.Format(BorrowRecordStr.formatRepay,
                        data.rateInterest * data.allMoney + data.allMoney / data.stagesNumber,
                        data.allMoney / data.stagesNumber,
                        data.rateInterest * data.allMoney,
                        0);
                    break;
                case BorrowRecordType.GM:
                    if (data.borrowState == BorrowState.WaitApproval)
                    {
                        GameManager.Single.gmAccountSetBorrowDlg.Show(accountData, data);
                    }
                    else if (data.borrowState == BorrowState.Approved)
                    {
                        GameManager.Single.gmAccountSetBorrowDlg.Show(accountData, data);
                    }
                    break;
                default:
                    break;
            }

        });
        SetText();
    }

    public void SetData(BorrowInformatio data)
    {
        this.data = data;
        SetText();
    }

    public void SetData(BorrowInformatio data, CommonAccountData accountData)
    {
        this.data = data;
        this.accountData = accountData;
        SetText();
    }

    private void SetText()
    {
        if (text && data != null)
        {
            text.text = GetBorrowInformatioStr(data);
        }
    }

    public static string GetBorrowInformatioStr(BorrowInformatio data)
    {
        string text;
        if (data.borrowState == BorrowState.Approved)
        {
            float repayCount = 0;
            float interestMoney = 0;
            float principal = 0;
            for (int i = 0; i < data.paymentInfos.Count; i++)
            {
                repayCount += data.paymentInfos[i].allMoney;
                interestMoney += data.paymentInfos[i].interestMoney;
                principal += data.paymentInfos[i].principal;
            }
            // "{0}日:借款{1},利率:{2}%,共分:{3}期,已还本金{4},已还利息{5}，已还期数{6}，剩余本金{7}，\n <color=red>——点我还款</color>";
            text = string.Format(BorrowRecordStr.format, TimeHelper.GetDateTime(data.dateTime).ToShortDateString(),
                data.allMoney,
                data.rateInterest * 100,
                data.stagesNumber,
                principal,
                interestMoney,
                data.paymentInfos.Count,
                data.allMoney - principal,
                GetBorrowStateStr(data.borrowState)
                );
        }
        else
            text = string.Format(BorrowRecordStr.formatWait, TimeHelper.GetDateTime(data.dateTime).ToShortDateString(), data.allMoney, data.rateInterest * 100, data.stagesNumber, GetBorrowStateStr(data.borrowState));
        return text;
    }

    public static float GetPrincipal(BorrowInformatio data)
    {
        float principal = 0;
        for (int i = 0; i < data.paymentInfos.Count; i++)
        {
            principal += data.paymentInfos[i].principal;
        }
        return principal;
    }

    private static string GetBorrowStateStr(BorrowState borrowState)
    {
        switch (borrowState)
        {
            case BorrowState.WaitApproval:
                return "\n<color=green>——等待管理员审核</color>";
            case BorrowState.Approved:
                return "\n<color=red>——点我还款</color>";
            case BorrowState.Refuse:
                return "\n<color=red>——已拒绝</color>";
            case BorrowState.Cancel:
                return "\n<color=yellow>——已取消</color>";
            case BorrowState.Settle:
                return "\n<color=yellow>——还款完毕！</color>";
            default:
                return "\n等待管理员审核";
        }
    }
}

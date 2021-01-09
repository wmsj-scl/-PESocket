using PESocket;
using Protocol.CommonData;
using UnityEngine;
using UnityEngine.UI;

public class BorrowRecordItem : MonoBehaviour
{
    private Button btnItemClick;
    private Text text;

    private string format = "{0}日:借款{1},利率:{2}%,共分:{3}期,剩余待还本金{4}，\n <color=red>——点我还款</color>";
    private string formatWait = "{0}日:借款{1},利率:{2}%,共分:{3}期,<color=green>等待管理员审核</color>";
    private string formatRepay = "本期应还:{0},其中本金：{1},利息：{2},手续费：{3}";
    private BorrowInformatio data;
    void Start()
    {
        btnItemClick = GetComponent<Button>();
        text = GetComponent<Text>();
        btnItemClick.onClick.AddListener(() =>
        { // todo
            GameManager.Single.MainDlg.MyDlg.RepayContent.gameObject.SetActive(true);
            GameManager.Single.MainDlg.MyDlg.TextNumber.text = string.Format(formatRepay,
                data.rateInterest * data.allMoney + data.allMoney / data.stagesNumber,
                data.allMoney / data.stagesNumber,
                data.rateInterest * data.allMoney,
                0);
        });
        SetText();
    }

    public void SetData(BorrowInformatio data)
    {
        this.data = data;
        SetText();
    }

    private void SetText()
    {
        if (text && data != null)
        {
            if (data.borrowState == BorrowState.Approved)
                text.text = string.Format(format, TimeHelper.GetDateTime(data.dateTime).ToShortDateString(), data.allMoney, data.rateInterest * 100, data.stagesNumber, data.account);
            else
                text.text = string.Format(formatWait, TimeHelper.GetDateTime(data.dateTime).ToShortDateString(), data.allMoney, data.rateInterest * 100, data.stagesNumber);
        }

    }
}

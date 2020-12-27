
using Protocol;
using Protocol.C2S;
using Protocol.CommonData;
using Protocol.S2C;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HGmDlg : MonoBehaviour
{
    public Button btnSetting;
    public Button btnReturn;

    public AccountSettingDlg settingDlg;

    public Transform Content;
    public GameObject tempAccount;
    private List<AccountItem> accountItems = new List<AccountItem>();
    private List<CommonAccountData> commonAccountDatas = new List<CommonAccountData>();

    void Start()
    {
        NotifyManager.AddNotify(MsgType.GetAllAccountList, GetAllAccountList);
        GameManager.Single.GameStart.SendMsg(new C2SGetAllAccountList() { account = GameManager.Single.userData.account });
        btnSetting.onClick.AddListener(Setting);
        btnReturn.onClick.AddListener(() => { gameObject.SetActive(false); });
        settingDlg.gameObject.SetActive(false);
    }

    public void GetAllAccountList(NetMsg msg)
    {
        if (msg.errorCode == ErrorCode.Succeed)
        {
            var data = msg as S2CGetAllAccountList;
            commonAccountDatas = data.accountDatas;
            UpdateAccountList();
        }
        else
        {
            GameManager.Single.PushTextDlg.ShowText(ErrorStr.GetErrorStr(msg.errorCode));
        }
    }

    public void UpdateAccountList()
    {
        for (int i = 0; i < commonAccountDatas.Count; i++)
        {
            if (i >= accountItems.Count)
            {
                accountItems.Add(Instantiate(tempAccount, Content).AddComponent<AccountItem>());
            }
            accountItems[i].SetData(commonAccountDatas[i]);
        }

        for (int i = commonAccountDatas.Count; i < accountItems.Count; i++)
        {
            accountItems[i].gameObject.SetActive(false);
        }
       
    }

    private void Setting()
    {

    }
}

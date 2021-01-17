using UnityEngine;
using Protocol;
using System.Collections.Generic;
using Protocol.S2C;
using UnityEngine.UI;

public class MyDlg : MonoBehaviour
{
    public Transform ContentBorrowRecord;
    public Transform ItemBorrowRecord;
    private List<BorrowRecordItem> itemList = new List<BorrowRecordItem>();
    public Transform RepayContent;
    public Text TextNumber;

    public Button btnCloseRepayContent;

    private void Start()
    {
        btnCloseRepayContent.onClick.AddListener(() => {
            RepayContent.gameObject.SetActive(false);
        });
        NotifyManager.AddNotify(MsgType.GetBorrowRecord, OnGetBorrowRecord);
        ItemBorrowRecord.gameObject.SetActive(false);
        RepayContent.gameObject.SetActive(false);
    }

    public void Show()
    {
        if (gameObject.activeSelf)
            return;
        GameManager.Single.GameStart.SendMsg(new Protocol.C2S.C2SGetBorrowRecord() { getAccount = GameManager.Single.userData.account });
        gameObject.SetActive(true);
    }

    private void OnGetBorrowRecord(NetMsg s)
    {
        var data = s as S2CGetBorrowRecord;
        for (int i = 0; i < data.list.Count; i++)
        {
            if (i >= itemList.Count)
            {
                itemList.Add(Instantiate(ItemBorrowRecord.gameObject, ContentBorrowRecord).GetComponent<BorrowRecordItem>());
            }
            itemList[i].gameObject.SetActive(true);
            itemList[i].SetData(data.list[i]);
        }
        for (int i = data.list.Count; i < itemList.Count; i++)
        {
            itemList[i].gameObject.SetActive(false);
        }
    }
}
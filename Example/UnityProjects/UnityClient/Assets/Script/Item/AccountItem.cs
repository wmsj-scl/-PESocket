using System;
using System.Collections;
using System.Collections.Generic;
using Protocol.CommonData;
using UnityEngine;
using UnityEngine.UI;

public class AccountItem : MonoBehaviour
{
    public Text Info;
    public Button btnSetAccount;
    private bool isAddEvent = false;
    private CommonAccountData data;

    public void Start()
    {
        if (Info)
            UpdateUIDate();
    }

    public void SetData(CommonAccountData data)
    {
        this.data = data;
        gameObject.SetActive(true);
        if (Info)
            UpdateUIDate();

    }

    public void UpdateUIDate()
    {
        Info.text = String.Format("姓名：{0},身份证：{1}，手机号：{2}，账号：{3}，密码：{4}，权限：{5}", data.name, data.id, data.phone, data.account, data.password, data.accountPower);
        if (!isAddEvent)
            btnSetAccount.onClick.AddListener(OnItemClick);
    }

    private void OnItemClick()
    {
        GameManager.Single.HGmDlg.settingDlg.Show(data);
    }
}

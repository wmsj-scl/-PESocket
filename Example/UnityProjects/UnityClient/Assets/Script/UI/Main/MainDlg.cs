using UnityEngine;
using UnityEngine.UI;

public class MainDlg : MonoBehaviour
{
    public Text text;

    public Toggle toggleGet;
    public Toggle toggleMy;
    public Button btnNoneGm;
    public Button btnGm;

    public Button btnCloseSetting;

    private HGmDlg HGmDlg;
    private GmDlg GmDlg;
    public GetDlg GetDlg;
    public MyDlg MyDlg;

    void Start()
    {
        HGmDlg = GameManager.Single.HGmDlg;
        GmDlg = GameManager.Single.GmDlg;
        toggleGet.onValueChanged.AddListener(OnToggleGet);
        toggleMy.onValueChanged.AddListener(OnToggleMy);
        btnCloseSetting.onClick.AddListener(() => {  });
        btnCloseSetting.gameObject.SetActive(false);
        btnNoneGm.onClick.AddListener(OnBtnNoneGm);
        btnGm.onClick.AddListener(OnBtnHightGm);

        btnNoneGm.gameObject.SetActive(GameManager.Single.userData.accountPower >= Protocol.AccountPower.NoneGm);
        btnGm.gameObject.SetActive(GameManager.Single.userData.accountPower >= Protocol.AccountPower.Gm);
        OnToggleGet(true);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        text.text = string.Format("Hi~,{0}", GameManager.Single.userData.name);
     
    }

    private void OnSettingClick()
    {
        var isShow = !btnCloseSetting.gameObject.activeSelf;
        btnCloseSetting.gameObject.SetActive(isShow);
    }

    private void OnBtnHightGm()
    {
        GmDlg.gameObject.SetActive(false);
        HGmDlg.gameObject.SetActive(true);
    }

    private void OnBtnNoneGm()
    {
        GmDlg.gameObject.SetActive(true);
        HGmDlg.gameObject.SetActive(false);
    }

    private void OnToggleGet(bool isOn)
    {
        if (isOn)
        {
            GetDlg.Show();
            MyDlg.gameObject.SetActive(false);
            HGmDlg.gameObject.SetActive(false);
            GmDlg.gameObject.SetActive(false);
        }

    }

    private void OnToggleMy(bool isOn)
    {
        if (isOn)
        {
            MyDlg.Show();
            GetDlg.gameObject.SetActive(false);
            HGmDlg.gameObject.SetActive(false);
            GameManager.Single.GmDlg.gameObject.SetActive(false);
        }
      
    }
}

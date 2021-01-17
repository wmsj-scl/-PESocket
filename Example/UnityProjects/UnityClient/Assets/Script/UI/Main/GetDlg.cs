
using UnityEngine;
using UnityEngine.UI;

public class GetDlg : MonoBehaviour
{
    public Button btnGet;

    public BorrowDlg borrowDlg;

    public Text Limit;
    public Text TipsInterests;

    private bool inited = false;

    void Start()
    {
        borrowDlg.gameObject.SetActive(false);
        btnGet.onClick.AddListener(()=> { borrowDlg.gameObject.SetActive(true); });
        UpdateUIData();
        inited = true;
    }

    public void UpdateUIData()
    {
        var appData = GameManager.Single.appData;
        Limit.text = Mathf.FloorToInt(appData.limitOfMoney).ToString();
        TipsInterests.text = string.Format("月利率：{0}%", (appData.interests * 100f).ToString("0.00"));
    }

    void Update()
    {
        
    }

    internal void Show()
    {
        if (gameObject.activeSelf)
            return;
        gameObject.SetActive(true);
        if (inited)
            UpdateUIData();
    }
}

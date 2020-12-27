
using UnityEngine;
using UnityEngine.UI;

public class HGmDlg : MonoBehaviour
{
    public Button btnSetting;
    public Button btnReturn;

    void Start()
    {
        btnSetting.onClick.AddListener(Setting);
        btnReturn.onClick.AddListener(() => { gameObject.SetActive(false); });
    }
    private void Setting()
    {

    }
}

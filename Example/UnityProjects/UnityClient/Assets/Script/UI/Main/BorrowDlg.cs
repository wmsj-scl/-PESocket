using UnityEngine;
using UnityEngine.UI;

public class BorrowDlg : MonoBehaviour
{
    public Toggle toggle;
    public Text textProtocolTitle;
    public Button btnBorrowMoney;
    public Button btnClose;
    public Transform protocolContent;
    public Text textProtocol;

    void Start()
    {
        textProtocolTitle.text = GameManager.Single.appData.borrowTitle;
        protocolContent.gameObject.SetActive(false);
        textProtocol.text = GameManager.Single.appData.borrowContent;
        btnClose.onClick.AddListener(() => { gameObject.SetActive(false); });
        btnBorrowMoney.onClick.AddListener(BorromMoney);
    }

    private void BorromMoney()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

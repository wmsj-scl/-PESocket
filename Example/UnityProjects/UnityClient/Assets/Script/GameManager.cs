using Protocol.CommonData;

public class GameManager : SingleBase<GameManager>
{
    public PushTextDlg PushTextDlg;
    public LoginDlg LoginDlg;
    public RegisterAccountDlg registerAccountDlg;
    public GameStart GameStart;
    public MainDlg MainDlg;
    public GmDlg GmDlg;
    public HGmDlg HGmDlg;
    public GmAccountSetBorrowDlg gmAccountSetBorrowDlg;


    public CommonAccountData userData;
    public AppData appData;

    void Start()
    {
        DontDestroyOnLoad(this);
        PushTextDlg.gameObject.SetActive(true);
        PushTextDlg.gameObject.SetActive(false);
        //registerAccountDlg.gameObject.SetActive(false);
        //GameStart.gameObject.SetActive(false);
        //MainDlg.gameObject.SetActive(false);
        //GmDlg.gameObject.SetActive(false);
        //HGmDlg.gameObject.SetActive(false);
        LoginDlg.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        NotifyManager.Update();
    }
}

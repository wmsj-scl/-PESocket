using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleBase<GameManager>
{
    public PushTextDlg PushTextDlg;
    public LoginDlg LoginDlg;
    public RegisterAccountDlg registerAccountDlg;
    public GameStart GameStart;

    void Start()
    {
        DontDestroyOnLoad(this);
        PushTextDlg.gameObject.SetActive(true);
        PushTextDlg.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        NotifyManager.Update();
    }
}

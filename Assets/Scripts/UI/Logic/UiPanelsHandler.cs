using UnityEngine;

public class UiPanelsHandler : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _finishPanel;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _leaderBoard;

    private const string Trigger = "Show";

    private GameObject[] _startPanelElements;

    private void OnEnable()
    {
        Scales.OnScalesBroke += ShowLosePanel;
        FindAndSetStartUIElements();
        _finishPanel.SetActive(false);
        ShowStartUI();
    }

    private void OnDestroy()
    {
        Scales.OnScalesBroke -= ShowLosePanel;
        SdkAndJavascriptHandler.OnAuthorizedAndPersonalProfileDataGot -= ShowLeaderBoard;
    }

    public void ShowWinPanel()
    {
        ShowFinishPanel();
        _finishPanel.GetComponent<FinishPanel>().ShowWinControls();
    }

    public void ShowLosePanel()
    {
        ShowFinishPanel();
        _finishPanel.GetComponent<FinishPanel>().ShowLoseControls();
    }

    public void ShowShopPanel()
    {
        HideStartUI();
        _shopPanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        HideStartUI();
        _settingsPanel.SetActive(true);
    }

    public void ShowLeaderBoard()
    {
        SdkAndJavascriptHandler.OnAuthorizedAndPersonalProfileDataGot -= ShowLeaderBoard;
        HideStartUI();
        _leaderBoard.SetActive(true);
    }

    public void ShowStartUI()
    {
        for (int i = 0; i < _startPanelElements.Length; i++)
            _startPanelElements[i].SetActive(true);

        _shopPanel.SetActive(false);
        _settingsPanel.SetActive(false);
        _leaderBoard.SetActive(false);
    }

    public void TryShowLeaderboard()
    {
        SdkAndJavascriptHandler.OnAuthorizedAndPersonalProfileDataGot += ShowLeaderBoard;
        SdkAndJavascriptHandler.TryAuthorize();
    }

    private void HideStartUI()
    {
        for (int i = 0; i < _startPanelElements.Length; i++)
            _startPanelElements[i].SetActive(false);
    }

    private void ShowFinishPanel()
    {
        _finishPanel.SetActive(true);
        _finishPanel.GetComponent<Animator>().SetTrigger(Trigger);
    }

    private void FindAndSetStartUIElements()
    {
        StartPanelElement[] elementList = _startPanel.GetComponentsInChildren<StartPanelElement>();

        _startPanelElements = new GameObject[elementList.Length];

        for (int i = 0; i < elementList.Length; i++)
            _startPanelElements[i] = elementList[i].gameObject;
    }
}
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private RectTransform _tutorialParent;
    [SerializeField] private TutorialComponentsHandler _tutorialPanelPrefab;

    private static Tutorial _instance;

    private TutorialComponentsHandler _componentsHandler;

    private void OnEnable()
    {
        _instance = this;
    }

    private void Start()
    {
        if (Game.IsTutorialFinished == false)
            StartTutorial();
    }

    private void OnDisable()
    {
        TutorialComponentsHandler.OnTutorialFinished -= EndTutorial;
    }

    public static void StartTutorial()
    {
        _instance.CreateTutorialWindow();
    }

    public static void Skip()
    {
        if (_instance != null && _instance._componentsHandler != null)
            _instance._componentsHandler.SwitchToNextKeyString();
    }

    private void CreateTutorialWindow()
    {
        _componentsHandler = Instantiate(_tutorialPanelPrefab, _tutorialParent);
        TutorialComponentsHandler.OnTutorialFinished += EndTutorial;
    }

    private void EndTutorial()
    {
        Destroy(_componentsHandler.gameObject);
        Destroy(this);
    }
}
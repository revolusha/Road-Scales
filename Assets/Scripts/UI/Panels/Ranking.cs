using Agava.YandexGames;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private RankingLine _template;
    [SerializeField] private UiPanelsHandler _uiPanelsHandler;

    private const string LeaderboardName = "ranking";

    private RankingLine[] rankingLines;

    private void OnEnable()
    {
        if (YandexGamesSdk.IsInitialized)
            ActualizeLeaderboard();
    }

    public void SaveLeaderboardScore()
    {
        Leaderboard.GetPlayerEntry(LeaderboardName, onSuccessCallback: (result) =>
        {
            if (result == null || Game.Money.Score > result.score)
                Leaderboard.SetScore(LeaderboardName, Game.Money.Score);
        });
    }

    public void LoadLeaderboard()
    {
        if (PlayerAccount.IsAuthorized == false)
            PlayerAccount.Authorize();

        Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: HandleResponse, onErrorCallback: HandleError);
    }

    public void ActualizeLeaderboard()
    {
        const int DelayAfterSave = 100;

        if (PlayerAccount.IsAuthorized == false || YandexGamesSdk.IsInitialized == false)
            return;

        SaveLeaderboardScore();
        System.Threading.Thread.Sleep(DelayAfterSave);
        LoadLeaderboard();
    }

    private void HandleError(string message)
    {
        Debug.Log(message);
    }

    private void HandleResponse(LeaderboardGetEntriesResponse result)
    {
        if (rankingLines == null)
            rankingLines = new RankingLine[result.entries.Length];

        for (int i = 0; i < rankingLines.Length; i++)
        {
            if (rankingLines[i] == null)
                rankingLines[i] = Instantiate(_template, _content.transform);

            string name = result.entries[i].player.publicName;
            if (string.IsNullOrEmpty(name))
                name = "Anonymous";

            rankingLines[i].SetTexts(name, result.entries[i].formattedScore, result.entries[i].rank);
        }
    }
}
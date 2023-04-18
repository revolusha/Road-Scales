using Agava.YandexGames;
using System.Collections;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private RankingLine _template;

    private RankingLine[] rankingLines;

    private const string LeaderboardName = "ranking";

    private void OnEnable()
    {
        StartCoroutine(Initialize());
    }

    public static void SetLeaderboardScore()
    {
        if (YandexGamesSdk.IsInitialized)
        {
            Leaderboard.GetPlayerEntry(LeaderboardName, onSuccessCallback: (result) =>
            {
                if (result == null || Game.Money.Score > result.score)
                    Leaderboard.SetScore(LeaderboardName, Game.Money.Score);
            });
        }
    }

    public void Authorize()
    {
        PlayerAccount.RequestPersonalProfileDataPermission();

        if (!PlayerAccount.IsAuthorized)
            PlayerAccount.Authorize();
    }

    private void LoadLeaderboard()
    {
        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            if (rankingLines == null)
                rankingLines = new RankingLine[result.entries.Length];

            for (int i = 0; i < rankingLines.Length; i++)
            {
                if (rankingLines[i] == null)
                    rankingLines[i] = Instantiate(_template);

                rankingLines[i].SetTexts(
                    result.entries[i].player.publicName, 
                    result.entries[i].formattedScore, 
                    result.entries[i].rank);

                //string name = result.entries[i].player.publicName;
                //if (string.IsNullOrEmpty(name))
                //    name = "Anonimus";

                //_leaderNames[i].text = name;
                //_scoreList[i].text = result.entries[i].formattedScore;
                //_ranks[i].text = result.entries[i].rank.ToString();
            }
        });
    }

    private IEnumerator Initialize()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        yield return YandexGamesSdk.Initialize();

        Authorize();
        LoadLeaderboard();
    }
}
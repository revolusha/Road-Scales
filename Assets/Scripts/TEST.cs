using UnityEngine;

public class TEST : MonoBehaviour
{
    private const int TestIndex = 14;

    public static void LoadLevel(int index)
    {
        LevelReloader.SwitchToLevel(index);
        LevelReloader.ReloadBaseLevel();
    }

    public static void LoadTestLevel()
    {
        LoadLevel(TestIndex);
    }
}
using UnityEngine;

public class TEST : MonoBehaviour
{
    public static void LoadLevel(int index)
    {
        LevelReloader.SwitchToLevel(index);
        LevelReloader.ReloadBaseLevel();
    }
}
using UnityEngine;

public class CrownBadge : MonoBehaviour
{
    private void Start()
    {
        if (Game.IsLastLevelFinished == false)
            gameObject.SetActive(false);
    }
}
using System.Collections;
using UnityEngine;

public class RewardButton : MonoBehaviour
{    
    private void OnEnable()
    {
        if (Game.Advertisement.IsReadyToShowRewardAd == false)
            gameObject.SetActive(false);
    }

    public void CheckShowPermission()
    {
        StartCoroutine(WaitBeforeCheck());
    }

    private IEnumerator WaitBeforeCheck()
    {
        const float Delay = .1f;

        yield return new WaitForSeconds(Delay);

        OnEnable();
    }
}
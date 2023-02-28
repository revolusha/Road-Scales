using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _controlsForLose;
    [SerializeField] private GameObject[] _controlsForWin;

    public void ShowWinControls()
    {
        HideGameObjects(_controlsForLose);
        ShowGameObjects(_controlsForWin);
    }

    public void ShowLoseControls()
    {
        HideGameObjects(_controlsForWin);
        ShowGameObjects(_controlsForLose);
    }

    private void HideGameObjects(GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
            array[i].SetActive(false);
    }

    private void ShowGameObjects(GameObject[] array)
    {
        for (int i = 0; i < array.Length; i++)
            array[i].SetActive(true);
    }
}

using UnityEngine;

public abstract class DialogWindowBase : MonoBehaviour
{
    [SerializeField] protected GameObject[] _elementsToHide;

    protected void OnEnable()
    {
        HidePanel();
    }

    public void HidePanel()
    {
        foreach (GameObject element in _elementsToHide)
            element.SetActive(false);
    }

    public void ShowPanel()
    {
        foreach (GameObject element in _elementsToHide)
            element.SetActive(true);
    }
}
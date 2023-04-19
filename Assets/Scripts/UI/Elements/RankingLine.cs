using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]

public class RankingLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _rank;
    [SerializeField] private RankBadge _badge;

    private Image _image;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
    }

    public void SetTexts(string name, string score, int rank, bool isPlayer = false)
    {
        _name.text = name;
        _score.text = score;
        _rank.text = rank.ToString();
    }
}
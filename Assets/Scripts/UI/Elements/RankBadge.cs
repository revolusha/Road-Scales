using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]

public class RankBadge : MonoBehaviour
{
    [SerializeField] private Sprite _firstPlace;
    [SerializeField] private Sprite _secondPlace;
    [SerializeField] private Sprite _thirdPlace;

    private Image _image;

    private void OnEnable()
    {
        _image = GetComponent<Image>();
    }

    public void SetBadge(int rank)
    {
        _image.color = Color.white;

        switch (rank)
        {
            case 1:
                _image.sprite = _firstPlace;
                break;
            case 2:
                _image.sprite = _secondPlace;
                break;
            case 3:
                _image.sprite = _thirdPlace;
                break;

            default:
                _image.color = new (0, 0, 0, 0);
                break;
        }
    }
}
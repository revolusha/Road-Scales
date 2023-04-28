using UnityEngine;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private LocalizationStringWithDynamicValue _dynamicLocalizationString;

    private const int IndexOffset = 1;

    private void OnEnable()
    {
        _dynamicLocalizationString.ChangeValue((Game.LevelHandler.CurrentLevelIndex + IndexOffset).ToString());
    }
}
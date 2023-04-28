using UnityEngine;

[CreateAssetMenu(fileName = "Localization", menuName = "Road Scale Game/Localization String Value")]

public class LocalizationStringValue : ScriptableObject
{
    [SerializeField, TextArea(1, 4)] private string _ruString;
    [SerializeField, TextArea(1, 4)] private string _enString;
    [SerializeField, TextArea(1, 4)] private string _trString;

    public string RuString => _ruString;
    public string EnString => _enString;
    public string TrString => _trString;
}
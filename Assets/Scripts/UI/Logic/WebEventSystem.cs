using UnityEngine.EventSystems;

namespace Agava.YandexGames.Samples
{
    public class WebEventSystem : EventSystem
    {
        protected override void OnApplicationFocus(bool hasFocus) => base.OnApplicationFocus(true);
    }
}

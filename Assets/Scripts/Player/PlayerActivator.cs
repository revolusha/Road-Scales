using UnityEngine;

[RequireComponent(typeof(PlayerSpawner))]

public class PlayerActivator : MonoBehaviour
{
    [SerializeField] private Behaviour[] _componentsToActivateOnStart;

    private PlayerSkinComponentHandler _componentHandler;

    public void StartMoving()
    {
        for (int i = 0; i < _componentsToActivateOnStart.Length; i++)
            _componentsToActivateOnStart[i].enabled = true;

        _componentHandler = GetComponent<PlayerSpawner>().Player.GetComponent<PlayerSkinComponentHandler>();
        _componentHandler.StartMoving();
    }

    public void Finish()
    {
        _componentHandler.Finish();
    }
}

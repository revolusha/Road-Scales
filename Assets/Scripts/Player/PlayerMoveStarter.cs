using UnityEngine;

public class PlayerMoveStarter : MonoBehaviour
{
    [SerializeField] private MonoBehaviour[] _componentsToActivateAfterStart;

    public void StartMoving()
    {
        for (int i = 0; i < _componentsToActivateAfterStart.Length; i++)
        {
            _componentsToActivateAfterStart[i].enabled = true;
        }

        this.enabled = false;
    }
}

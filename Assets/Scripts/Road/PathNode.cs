using UnityEngine;

public class PathNode: MonoBehaviour 
{
    [SerializeField] private Vector3 _localPosition;

    private void OnEnable()
    {
        transform.localPosition = _localPosition;
    }

    private void OnDrawGizmos()
    {
        const float Radius = .2f;

        Gizmos.color = new(1, 0, 0, .3f);
        Gizmos.DrawSphere(transform.position, Radius);
    }
<<<<<<< Updated upstream

    private void SetNextNode(PathNode node)
    {
        Next = node;
    }
=======
>>>>>>> Stashed changes
}

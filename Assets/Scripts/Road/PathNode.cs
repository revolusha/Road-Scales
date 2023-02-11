using UnityEngine;

public class PathNode: MonoBehaviour 
{
    [SerializeField] private Vector3 _localPosition;

    public PathNode Next { get; private set; }

    private void OnEnable()
    {
        transform.localPosition = _localPosition;
    }

    private void OnDrawGizmos()
    {
        const float Radius = .5f;

        Gizmos.color = new(1, 0, 0, .3f);
        Gizmos.DrawSphere(transform.position, Radius);
    }

    private void SetNextNode(PathNode node)
    {
        Next = node;
    }
}

using UnityEngine;

public class RoadSegment : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _roadMeshRenderers;

    private RoadJoint _nextJoint;
    
    public Path Path { get; private set; }
    public RoadSegment Next { get; private set; }
    public Vector3 NextJointPosition => _nextJoint.transform.position;
    public Quaternion NextJointRotation => _nextJoint.transform.rotation;

    private void OnEnable()
    {
        _nextJoint = GetComponentInChildren<RoadJoint>();
        Path = GetComponentInChildren<Path>();
    }

    public RoadSegment PutBehindSegment(RoadSegment previousSegment)
    {
        transform.SetPositionAndRotation(previousSegment.NextJointPosition, previousSegment.NextJointRotation);
        previousSegment.SetNextSegment(this);

        return this;
    }

    public void SetNextSegment(RoadSegment next)
    {
        Next = next;
    }

    public void SetMaterial(Material material)
    {
        for (int i = 0; i < _roadMeshRenderers.Length; i++)
            _roadMeshRenderers[i].material = material;
    }
}

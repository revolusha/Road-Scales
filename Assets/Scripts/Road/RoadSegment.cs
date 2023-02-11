using UnityEngine;

public class RoadSegment : MonoBehaviour
{
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
       MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = material;
        }
    }
}

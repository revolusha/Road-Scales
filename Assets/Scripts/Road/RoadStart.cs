using UnityEngine;

public class RoadStart : MonoBehaviour
{
    public static RoadSegment Segment { get; private set; }

    private void OnEnable()
    {
        Segment = GetComponent<RoadSegment>();
    }
}
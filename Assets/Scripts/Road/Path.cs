using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private GameObject _pathNodeTemplate;
    [SerializeField] private Vector3[] _localPathNodesPositions = new Vector3[] { new Vector3(0, 0, 10) };

    private PathNode[] _pathNodes;

    public PathNode[] PathNodes => _pathNodes;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDrawGizmos()
    {
        const int MinimalRequiredNodesToDraw = 2;

        if (_localPathNodesPositions.Length < MinimalRequiredNodesToDraw || _pathNodes != null)
            return;

        Gizmos.color = new(1, 0, 0, .3f);

        for (int i = _pathNodes.Length; i > 1;  i--)
        {
            Gizmos.DrawLine(_pathNodes[i].transform.position, _pathNodes[i - 1].transform.position);
        }
    }

    private void Initialize()
    {
        _pathNodes = new PathNode[_localPathNodesPositions.Length];

        for (int i = 0; i < _localPathNodesPositions.Length; i++)
        {
            GameObject pathNode = Instantiate(_pathNodeTemplate, transform);

            pathNode.transform.localPosition = _localPathNodesPositions[i];
            _pathNodes[i] = pathNode.GetComponent<PathNode>();
        }
    }
}

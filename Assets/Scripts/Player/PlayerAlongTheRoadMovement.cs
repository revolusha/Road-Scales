using System.Collections.Generic;
using UnityEngine;

public class PlayerAlongTheRoadMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private float _minimalDistanseToPathNode = 0.2f;

    private const float MoveFactor = 3;

    private bool _canMove = false;
    private PathNode _currentNode;
    private Queue<PathNode> _path;

    private void OnEnable()
    {
        RoadBuilder.OnRoadReady += StartMoving;
    }

    private void OnDisable()
    {
        RoadBuilder.OnRoadReady -= StartMoving;
    }

    private void Update()
    {
        if (_canMove)
        {
            ÀctualizeNode();
            CorrrectDirection();
            Move();
        }
    }

    private void OnDrawGizmosSelected()
    {
        const float Radius = 0.4f;

        if (_currentNode == null)
            return;

        Gizmos.color = new Color(0,0,1,0.2f);
        Gizmos.DrawSphere(_currentNode.transform.position, Radius);
    }

    private void StartMoving()
    {
        _path = GetPath(RoadStart.Segment);
        _currentNode = _path.Dequeue();
        _canMove = true;
        RoadBuilder.OnRoadReady -= StartMoving;
    }

    private void CorrrectDirection()
    {
        const float AlignmentSpeed = .01f;

        Vector3 targetDirection = (_currentNode.transform.position - transform.position).normalized;

        if (transform.forward == targetDirection)
            return;

        if (CheckAlignment(targetDirection))
            transform.LookAt(_currentNode.transform.position);
        else
        {
            Vector3 oldLerpedDirection = transform.forward * _currentNode.transform.position.magnitude;
            Vector3 lerpedDirection = Vector3.Lerp(oldLerpedDirection, _currentNode.transform.position, AlignmentSpeed * Time.deltaTime);

            transform.LookAt(lerpedDirection);
        }
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            transform.position + transform.forward * MoveFactor,
            _speed * Time.deltaTime);
    }

    private Queue<PathNode> GetPath(RoadSegment firstSegment)
    {
        Queue<PathNode> path = new Queue<PathNode>();

        RoadSegment current = firstSegment;

        while (current != null)
        {
            for (int i = 0; i < current.Path.PathNodes.Length; i++)
            {
                path.Enqueue(current.Path.PathNodes[i]);
            }

            current = current.Next;
        } 

        return path;
    }

    private void ÀctualizeNode()
    {
        if (Vector3.Distance(transform.position, _currentNode.transform.position)
            < _minimalDistanseToPathNode)
        {
            _currentNode = _path.Dequeue();
        }
    }

    private bool CheckAlignment(Vector3 targetDirection)
    {
        const float AlighnmentThreshold = .01f;

        Vector3 difference = new(
            targetDirection.x - transform.forward.x,
            targetDirection.y - transform.forward.y,
            targetDirection.z - transform.forward.z);

        if (difference.x > AlighnmentThreshold 
            || difference.y > AlighnmentThreshold 
            || difference.z > AlighnmentThreshold)
            return false;

        return true;
    }
}

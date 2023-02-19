using System.Collections.Generic;
using UnityEngine;

public class PlayerAlongTheRoadMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 0.2f;
<<<<<<< Updated upstream
    [SerializeField] private float _minimalDistanseToPathNode = 0.2f;
=======
    [SerializeField] private float _alignmentSpeed = .1f;
>>>>>>> Stashed changes

    private const float MoveFactor = 3;

    private bool _canMove = false;
<<<<<<< Updated upstream
=======
    private bool _isOnNode = false;
>>>>>>> Stashed changes
    private PathNode _currentNode;
    private Queue<PathNode> _path;

    private void OnEnable()
    {
        if (RoadBuilder.IsReady)
        {
            StartMoving();
            return;
        }
        
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
<<<<<<< Updated upstream
        const float AlignmentSpeed = .01f;

        Vector3 targetDirection = (_currentNode.transform.position - transform.position).normalized;

        if (transform.forward == targetDirection)
            return;

        if (CheckAlignment(targetDirection))
=======
        if (_isOnNode == false && CheckAlignment())
        {
>>>>>>> Stashed changes
            transform.LookAt(_currentNode.transform.position);
        else
        {
<<<<<<< Updated upstream
            Vector3 oldLerpedDirection = transform.forward * _currentNode.transform.position.magnitude;
            Vector3 lerpedDirection = Vector3.Lerp(oldLerpedDirection, _currentNode.transform.position, AlignmentSpeed * Time.deltaTime);
=======
            float destinationDistance = Vector3.Distance(transform.position, _currentNode.transform.position);
            Vector3 transformGlobalForward = transform.position + transform.forward;
            Vector3 lerpedDestination = Vector3.Lerp(
                transformGlobalForward, 
                _currentNode.transform.position, 
                _alignmentSpeed * Time.deltaTime / destinationDistance / 2);
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
        if (Vector3.Distance(transform.position, _currentNode.transform.position)
            < _minimalDistanseToPathNode)
        {
            _currentNode = _path.Dequeue();
        }
=======
        if (_path.Count <= 0)
        {
            _canMove = false;
            return;
        }

        _currentNode = _path.Dequeue();
>>>>>>> Stashed changes
    }

    private bool CheckAlignment(Vector3 targetDirection)
    {
<<<<<<< Updated upstream
        const float AlighnmentThreshold = .01f;
=======
        const float AlighnmentThresholdAngle = .25f;
>>>>>>> Stashed changes

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

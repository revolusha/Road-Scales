using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlongTheRoadMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private float _alignmentSpeed = .1f;

    private const float MoveFactor = 3;

    private bool _canMove = false;
    private bool _isOnNode = false;
    private PathNode _currentNode;
    private Queue<PathNode> _path;

    public Action OnFinished;

    private void OnEnable()
    {
        Scales.OnScalesBroke += StopMoving;

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
        Scales.OnScalesBroke -= StopMoving;
    }

    private void Update()
    {
        if (_canMove)
        {
            CorrectDirection();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PathNode>() == _currentNode)
        {
            _isOnNode = true;
            SkipNode();
        }
        else if (other.TryGetComponent(out Finish finish))
        {
            _canMove = false;
            finish.TriggerFinishEvent();
            Debug.Log("finish");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PathNode>())
        {
            _isOnNode = false;
        }
    }

    private void SkipNode()
    {
        if (_path == null || _path.Count == 0)
        {
            _canMove = false;
            return;
        }

        _currentNode = _path.Dequeue();
    }

    private void StartMoving()
    {
        _path = GetPath(RoadStart.Segment);
        _currentNode = _path.Dequeue();
        _canMove = true;
        RoadBuilder.OnRoadReady -= StartMoving;
    }

    private void StopMoving()
    {
        _canMove = false;
        Scales.OnScalesBroke += StopMoving;
    }


    private void CorrectDirection()
    {
        if (_isOnNode == false && CheckAlignment())
        {
            transform.LookAt(_currentNode.transform.position);
        }
        else
        {
            float destinationDistance = Vector3.Distance(transform.position, _currentNode.transform.position);
            Vector3 transformGlobalForward = transform.position + transform.forward;
            Vector3 lerpedDestination = Vector3.Lerp(
                transformGlobalForward,
                _currentNode.transform.position,
                _alignmentSpeed * Time.deltaTime / destinationDistance);

            transform.LookAt(lerpedDestination);
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
        Queue<PathNode> path = new();

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

    private bool CheckAlignment()
    {
        const float AlighnmentThresholdAngle = .1f;

        float angle = Vector3.Angle(
            transform.forward, 
            _currentNode.transform.position - transform.position);

        if (angle < AlighnmentThresholdAngle)
            return true;

        return false;
    }
}

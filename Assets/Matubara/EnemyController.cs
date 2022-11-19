using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("����n�_��transform��o�^����")] Transform[] _waypoints = default;
    [SerializeField, Header("Ray�̎n�_")] Transform _rayStartpoint = default;
    [SerializeField, Header("Ray�̒���")] float _maxDistance;
    NavMeshAgent _navMeshAgent = default;
    int _currentWaypointIndex = 0;
    Vector3 _raycastHitPosition;
    void Start()
    {
        _navMeshAgent= GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_waypoints[0].position);
    }
    void Update()
    {
        if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            _navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
        }
        SearchingforPlayer();
    }

    void SearchingforPlayer()
    {
        Ray ray = new Ray(_rayStartpoint.position, transform.forward);
        _raycastHitPosition = _rayStartpoint.position + transform.forward * _maxDistance;
        
        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance) && hit.collider.CompareTag("Player"))
        {
            _raycastHitPosition = hit.point;
            Debug.Log("Ray��Player�Ƀq�b�g���܂���");
            fire();
        }

        Debug.DrawLine(ray.origin, _raycastHitPosition, Color.red);
    }

    void fire()
    {

    }
}

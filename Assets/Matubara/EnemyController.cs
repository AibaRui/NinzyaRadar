using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("巡回地点のtransformを登録する")] Transform[] _waypoints = default;
    [SerializeField, Header("Rayの始点")] Transform _rayStartpoint = default;
    [SerializeField, Header("Rayの長さ")] float _maxDistance;
    [SerializeField, Header("Bulletのプレハブ")] GameObject _bulletPurefab;
    [SerializeField, Header("弾を発射する地点")] Transform _muzzle;
    [SerializeField, Header("弾丸の速度")] float _forcepower;
    [SerializeField, Header("EnemyのHP")] int _hp = 2;
    bool _isP = false;
    float _saveSpeed;
    NavMeshAgent _navMeshAgent = default;
    int _currentWaypointIndex = 0;
    Vector3 _raycastHitPosition;
    void Start()
    {
        _navMeshAgent= GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_waypoints[0].position);
        _saveSpeed = _navMeshAgent.speed;
    }
    void Update()
    {
        SearchingforPlayer();
    }

    void SearchingforPlayer()
    {
        Ray ray = new Ray(_rayStartpoint.position, transform.forward);
        _raycastHitPosition = _rayStartpoint.position + transform.forward * _maxDistance;

        if (Physics.Raycast(ray, out RaycastHit hit, _maxDistance) && hit.collider.CompareTag("Player"))
        {
            _isP = true;
        }
        else
        {
            _isP = false;
        }

        if (_isP)
        {
            _navMeshAgent.speed = 0;
            _raycastHitPosition = hit.point;
            Debug.Log("RayがPlayerにヒットしました");
            Fire();
        }
        else
        {
            _navMeshAgent.speed = _saveSpeed;
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                _navMeshAgent.SetDestination(_waypoints[_currentWaypointIndex].position);
            }
        }

        Debug.DrawLine(_rayStartpoint.position, _raycastHitPosition, Color.red);
    }

    void Fire()
    {
        GameObject go = Instantiate(_bulletPurefab, _muzzle.position, transform.rotation);
        go.GetComponent<Rigidbody>().AddForce(this.transform.forward * _forcepower, ForceMode.VelocityChange);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("") || CompareTag(""))
        {
            _hp -= 1;
        }
    }
}

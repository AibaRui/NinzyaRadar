using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class EnemyController : MonoBehaviour
{
    [SerializeField, Header("����n�_��transform��o�^����")] Transform[] _waypoints = default;
    [SerializeField, Header("Ray�̎n�_")] Transform[] _rayStartpoints = default;
    [SerializeField, Header("Ray�̒���")] float _maxDistance;
    [SerializeField, Header("Bullet�̃v���n�u")] GameObject _bulletPurefab;
    [SerializeField, Header("�e�𔭎˂���n�_")] Transform _muzzle;
    [SerializeField, Header("�e�ۂ̑��x")] float _forcepower;
    [SerializeField, Header("�e�̘A�ˑ��x")] float _firerate;
    [SerializeField, Header("Enemy��HP")] int _hp = 2;
    [SerializeField, TagField, Header("Player�̍U���̃^�O�̖��O(2�܂�)")] string[] _playerAttackTagName = new string[2];
    float _timer;
    float _saveSpeed;
    NavMeshAgent _navMeshAgent = default;
    int _currentWaypointIndex = 0;
    Vector3 _raycastHitPosition;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.SetDestination(_waypoints[0].position);
        _saveSpeed = _navMeshAgent.speed;
        _timer = _firerate;
    }
    void Update()
    {
        Ray sightray = new Ray(_rayStartpoints[0].position, transform.forward);
        Ray sightray2 = new Ray(_rayStartpoints[1].position, transform.forward);

        _raycastHitPosition = _rayStartpoints[0].position + transform.forward * _maxDistance;

        if (Physics.Raycast(sightray, out RaycastHit hit, _maxDistance) && hit.collider.CompareTag("Player") || Physics.Raycast(sightray2, out hit, _maxDistance) && hit.collider.CompareTag("Player"))
        {
            _raycastHitPosition = hit.point;
            sighting(true);
        }
        else
        {
            sighting(false);
        }

        Debug.DrawLine(_rayStartpoints[0].position, _raycastHitPosition, Color.red);
        Debug.DrawLine(_rayStartpoints[1].position, _raycastHitPosition, Color.red);
    }

    void sighting(bool b)
    {
        if (b)
        {
            Transform playerpos = GameObject.FindGameObjectWithTag("Player").transform;
            transform.forward = playerpos.position - this.transform.position;
            _navMeshAgent.speed = 0;
            Shoot();
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
    }

    void Shoot()
    {
        _timer += Time.deltaTime;
        if (_timer > _firerate)
        {
            GameObject go = Instantiate(_bulletPurefab, _muzzle.position, transform.rotation);
            go.GetComponent<Rigidbody>().AddForce(this.transform.forward * _forcepower, ForceMode.VelocityChange);
            _timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _playerAttackTagName[0] || other.gameObject.tag == _playerAttackTagName[1])
        {
            _hp -= 1;
            Debug.Log(_hp);
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

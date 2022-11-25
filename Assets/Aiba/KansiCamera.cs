using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KansiCamera : MonoBehaviour
{

    [SerializeField] float _maxAngle = 90;
    [SerializeField] float _minAngle = -90;
    [SerializeField] float _rotateSpeed = 0.1f;

    [SerializeField] GameObject _moveKansiCamera;

    [SerializeField] Transform _muzzle;

    [SerializeField] GameObject _gun;
    [SerializeField] GameObject _dir;

    [SerializeField] float _lineDir = 2;
    [SerializeField] LayerMask _playerLayre;

    [SerializeField] GameObject _bullet;
    [SerializeField] float _bulletSpeed = 5;

    RaycastHit _hit;
    bool _isRotateRight = false;

    [SerializeField] float _coolTime = 5;
    float _countTime = 0;

    bool _isAttack = true;
    bool _isLock = false;
    bool _isLockOn = false;

    float _time = 5;
    float count = 0;


    [SerializeField] AudioSource _aram;
    [SerializeField] AudioSource _gunAudio;

    Vector3 _startAngle;
    void Start()
    {
        // transformを取得
        Transform cameraTransform = _moveKansiCamera.transform;
        // ローカル座標を基準に、回転を取得
        Vector3 localAngle = cameraTransform.localEulerAngles;

        _aram = _aram.GetComponent<AudioSource>();
        _gunAudio = _gunAudio.GetComponent<AudioSource>();

        if (localAngle.y >= 0)
        {
            _isRotateRight = true;
        }
    }

    void Update()
    {
        if (!_isLockOn)
        {
            Check();
        }

        Gun();
        CoolTime();
        LockOnPlayer();
    }

    /// <summary>Rayを飛ばしてる</summary>
    private void Check()
    {
        Vector3 start = _gun.transform.position;
        Vector3 dir = _dir.transform.position - _gun.transform.position;
        bool fowardHit = Physics.Raycast(start, dir, out _hit, _lineDir);
        if (_hit.collider != null && _hit.collider.gameObject.tag == "Player")
        {
            _isLockOn = true;
            _isLock = true;
            _aram.Play();
        }
    }
    void OnDrawGizmos()
    {
        Vector3 start2 = _gun.transform.position;
        Vector3 dir = _dir.transform.position - _gun.transform.position;
        Vector3 end2 = start2 + dir * _lineDir;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(start2, end2);
    }

    void Gun()
    {
        if (_isLockOn && _isAttack)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            Vector3 dir = player.transform.position - transform.position;
            var go = Instantiate(_bullet);
            go.transform.position = _muzzle.position;
            go.transform.forward = dir;
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * _bulletSpeed;
            _isAttack = false;
            _gunAudio.Play();
        }
        else if (_isLock && _isAttack)
        {
            Vector3 dir = _dir.transform.position - _gun.transform.position;

            var go = Instantiate(_bullet);
            go.transform.position = _muzzle.position;
            go.transform.forward = dir;
            go.GetComponent<Rigidbody>().velocity = go.transform.forward * _bulletSpeed;
            _isAttack = false;
            _isLock = false;
            _gunAudio.Play();
        }

    }


    void LockOnPlayer()
    {
        if (_isLockOn)
        {

            Debug.Log("ddd");

            var go = GameObject.FindGameObjectWithTag("Player");

            Vector3 p = go.transform.position - _moveKansiCamera.transform.position;

            _moveKansiCamera.transform.right = -p;

            bool rayForPlayer = Physics.Raycast(_muzzle.position, go.transform.position, _lineDir, _playerLayre);

            count += Time.deltaTime;

            if (count >= _time)
            {
                _aram.Stop();
                count = 0;
                Transform cameraTransform = _moveKansiCamera.transform;
                // ローカル座標を基準に、回転を取得
                Vector3 localAngle = new Vector3(0, 0, 0);
                _isLockOn = false;
                cameraTransform.localEulerAngles = localAngle; // 回転角度を設定
            }
        }
    }



    void CoolTime()
    {
        if (!_isAttack)
        {
            _countTime += Time.deltaTime;

            if (_countTime >= _coolTime)
            {
                _countTime = 0;
                _isAttack = true;
            }
        }
    }



    private void FixedUpdate()
    {
        if (!_isLockOn)
        {
            //  Debug.Log(_isRotateRight);

            // transformを取得
            Transform cameraTransform = _moveKansiCamera.transform;
            // ローカル座標を基準に、回転を取得
            Vector3 localAngle = cameraTransform.localEulerAngles;


            if (_isRotateRight)
            {
                localAngle.y += _rotateSpeed; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
                if (localAngle.y >= _maxAngle && localAngle.y < _minAngle)
                {
                    _isRotateRight = false;
                }
            }
            else if (!_isRotateRight)
            {
                localAngle.y -= _rotateSpeed; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
                if (localAngle.y <= _minAngle && localAngle.y > _maxAngle)
                {
                    _isRotateRight = true;
                }
            }

            cameraTransform.localEulerAngles = localAngle; // 回転角度を設定
        }
    }
}

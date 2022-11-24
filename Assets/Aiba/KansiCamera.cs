using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KansiCamera : MonoBehaviour
{
    [SerializeField] float _maxAngle = 90;
    [SerializeField] float _minAngle = -90;
    [SerializeField] float _rotateSpeed = 0.1f;

    [SerializeField] GameObject _moveKansiCamera;

    bool _isRotateRight = false;
    void Start()
    {
        // transformを取得
        Transform cameraTransform = _moveKansiCamera.transform;
        // ローカル座標を基準に、回転を取得
        Vector3 localAngle = cameraTransform.localEulerAngles;

        if(localAngle.y>=0)
        {
            _isRotateRight = true;
        }
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
      //  Debug.Log(_isRotateRight);

        // transformを取得
        Transform cameraTransform = _moveKansiCamera.transform;
        // ローカル座標を基準に、回転を取得
        Vector3 localAngle = cameraTransform.localEulerAngles;


        if (_isRotateRight)
        {
            localAngle.y += _rotateSpeed; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
            Debug.Log(localAngle.y);
            if (localAngle.y >= _maxAngle && localAngle.y < _minAngle)
            {
                _isRotateRight = false;
            }
        }
        else if (!_isRotateRight)
        {
            localAngle.y -= _rotateSpeed; // ローカル座標を基準に、y軸を軸にした回転を10度に変更
            Debug.Log(localAngle.y);
            if (localAngle.y <= _minAngle &&localAngle.y>_maxAngle)
            {
                _isRotateRight = true;
            }
        }

        cameraTransform.localEulerAngles = localAngle; // 回転角度を設定
    }
}

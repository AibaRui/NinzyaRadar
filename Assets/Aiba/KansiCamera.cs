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
        // transform���擾
        Transform cameraTransform = _moveKansiCamera.transform;
        // ���[�J�����W����ɁA��]���擾
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

        // transform���擾
        Transform cameraTransform = _moveKansiCamera.transform;
        // ���[�J�����W����ɁA��]���擾
        Vector3 localAngle = cameraTransform.localEulerAngles;


        if (_isRotateRight)
        {
            localAngle.y += _rotateSpeed; // ���[�J�����W����ɁAy�������ɂ�����]��10�x�ɕύX
            Debug.Log(localAngle.y);
            if (localAngle.y >= _maxAngle && localAngle.y < _minAngle)
            {
                _isRotateRight = false;
            }
        }
        else if (!_isRotateRight)
        {
            localAngle.y -= _rotateSpeed; // ���[�J�����W����ɁAy�������ɂ�����]��10�x�ɕύX
            Debug.Log(localAngle.y);
            if (localAngle.y <= _minAngle &&localAngle.y>_maxAngle)
            {
                _isRotateRight = true;
            }
        }

        cameraTransform.localEulerAngles = localAngle; // ��]�p�x��ݒ�
    }
}

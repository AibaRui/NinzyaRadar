using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KansiCamera : MonoBehaviour
{
    [SerializeField] float _maxAngle = 90;
    [SerializeField] float _minAngle = -90;
    [SerializeField] float _rotateSpeed = 0.1f;

    [SerializeField] GameObject _moveKansiCamera;

    bool _isRotateRight = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

        // transform���擾
        Transform cameraTransform = _moveKansiCamera.transform;
        // ���[�J�����W����ɁA��]���擾
        Vector3 localAngle = cameraTransform.localEulerAngles;


        if(_isRotateRight)
        {
            localAngle.y += _rotateSpeed; // ���[�J�����W����ɁAy�������ɂ�����]��10�x�ɕύX

            if(localAngle.y>=_maxAngle)
            {
                _isRotateRight = false;
            }
        }
      else  if (!_isRotateRight)
        {
            localAngle.y -= _rotateSpeed; // ���[�J�����W����ɁAy�������ɂ�����]��10�x�ɕύX

            if (localAngle.y <= _minAngle)
            {
                _isRotateRight = true;
            }
        }

        cameraTransform.localEulerAngles = localAngle; // ��]�p�x��ݒ�
    }
}

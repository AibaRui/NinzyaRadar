using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoiMove : MonoBehaviour
{
    [Header("�̗�")]
    [Tooltip("�̗�")] [SerializeField] float _lifeTime = 5;

    [Header("�X�s�[�h")]
    [Tooltip("�X�s�[�h")] [SerializeField] float _moveSpeed = 5;
    /// <summary>�J�E���g�p</summary>
    private float _countTime = 0;

    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        _rb.velocity = transform.forward*_moveSpeed;
        _rb.velocity = new Vector3(_rb.velocity.x, -1, _rb.velocity.z);
        _countTime += Time.deltaTime;
        if (_countTime > _lifeTime)
        {
            Destroy(gameObject);
        }
    }
}

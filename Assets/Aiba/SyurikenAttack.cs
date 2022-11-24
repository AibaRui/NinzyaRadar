using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyurikenAttack : MonoBehaviour
{
    [Header("�藠���̃v���n�u")]
    [Tooltip("�藠���̃v���n�u")] [SerializeField] GameObject _syurikenPrefab;

    [Header("�藠���̑��x")]
    [Tooltip("�藠���̑��x")] [SerializeField] float _speed = 5f;

    [Header("�}�Y���̈ʒu")]
    [Tooltip("�}�Y���̈ʒu")] [SerializeField] GameObject _muzzle;


    public void Attack()
    {
        var go = Instantiate(_syurikenPrefab);
        go.transform.position = _muzzle.transform.position;

        var rb = go.GetComponent<Rigidbody>();

        Vector3 velo = Camera.main.transform.forward;

        rb.velocity = velo.normalized * _speed;
    }
}

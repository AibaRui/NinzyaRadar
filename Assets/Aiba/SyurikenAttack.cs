using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyurikenAttack : MonoBehaviour
{
    [Header("手裏剣のプレハブ")]
    [Tooltip("手裏剣のプレハブ")] [SerializeField] GameObject _syurikenPrefab;

    [Header("手裏剣の速度")]
    [Tooltip("手裏剣の速度")] [SerializeField] float _speed = 5f;

    [Header("マズルの位置")]
    [Tooltip("マズルの位置")] [SerializeField] GameObject _muzzle;


    public void Attack()
    {
        var go = Instantiate(_syurikenPrefab);
        go.transform.position = _muzzle.transform.position;

        var rb = go.GetComponent<Rigidbody>();

        Vector3 velo = Camera.main.transform.forward;

        rb.velocity = velo.normalized * _speed;
    }
}

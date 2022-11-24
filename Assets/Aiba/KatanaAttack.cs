using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaAttack : MonoBehaviour
{
    [Header("�U���̓����蔻��")]
    [Tooltip("�U���̓����蔻��")] [SerializeField] GameObject _attackColider;

    [Header("�U���̓����蔻��̗L������")]
    [Tooltip("�U���̓����蔻��̗L������")]
    [SerializeField] float _attackTime = 0.1f;
    void Start()
    {
        
    }

    public IEnumerator AttackCollider()
    {
        _attackColider.SetActive(true);
        yield return new WaitForSeconds(_attackTime);
        _attackColider.SetActive(false);
    }



    private void OnDisable()
    {
        _attackColider.SetActive(false);
    }
}

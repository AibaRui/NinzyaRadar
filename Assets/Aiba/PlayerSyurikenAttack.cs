using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSyurikenAttack : MonoBehaviour
{
    [Header("Mianカメラにつけている武器")]
    [Tooltip("Mianカメラにつけている武器")] [SerializeField] GameObject _syuriken;

    [Header("クールタイム")]
    [Tooltip("クールタイム")]
    [SerializeField] float _coolTime = 1f;

    bool _isAttack = true;

    P_Control _control;

    [SerializeField] Animator _animSyuriken;
    Rigidbody m_rb;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        _control = FindObjectOfType<P_Control>();
        _animSyuriken = _animSyuriken.GetComponent<Animator>();
    }


    void Update()
    {
        if (_syuriken.activeSelf)
        {
            Attack();
        }
    }


    void Attack()
    {
        //手裏剣を装備している
        if (_syuriken.activeSelf)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_isAttack)
                {
                    return;
                }
                Debug.Log("2a");
                _isAttack = false;
                _animSyuriken.Play("SyurikenAttack");
                StartCoroutine(CoolTime());
            }

        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(_coolTime);
              _isAttack = true;
    }
}

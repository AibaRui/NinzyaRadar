using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKatanaAttack : MonoBehaviour
{

    [Header("Mianカメラにつけている武器")]
    [Tooltip("Mianカメラにつけている武器")] [SerializeField] GameObject _katana;

    [Header("攻撃の当たり判定")]
    [Tooltip("攻撃の当たり判定")] [SerializeField] GameObject _attackColider;

    [Header("攻撃の当たり判定の有効時間")]
    [Tooltip("攻撃の当たり判定の有効時間")]
    [SerializeField] float _attackTime = 0.1f;

    [SerializeField] int _maxCount = 4;

    bool _okAttack = false;
    bool _nowAttacking = false;

    private int count = 0;

    public int Count { get => count; set => count = value; }

    P_Control _control;

    [SerializeField] Animator _animKatana;

    Rigidbody m_rb;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        _control = FindObjectOfType<P_Control>();
        _animKatana = _animKatana.GetComponent<Animator>();

    }

    void Update()
    {
            Attack();
    }

    void Attack()
    {
        //刀を装備している
        if (_katana.activeSelf)
        {
            if (Input.GetMouseButton(0))
            {
                if (count < _maxCount)
                {
                    if (count == 0)
                    {
                        _animKatana.Play("katanaAttack");
                    }
                    else
                    {
                        _animKatana.SetTrigger("Attack");
                    }
                }
            }
        }
    }

}

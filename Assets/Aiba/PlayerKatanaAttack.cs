using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKatanaAttack : MonoBehaviour
{

    [Header("MianƒJƒƒ‰‚É‚Â‚¯‚Ä‚¢‚é•Ší")]
    [Tooltip("MianƒJƒƒ‰‚É‚Â‚¯‚Ä‚¢‚é•Ší")] [SerializeField] GameObject _katana;

    [Header("UŒ‚‚Ì“–‚½‚è”»’è")]
    [Tooltip("UŒ‚‚Ì“–‚½‚è”»’è")] [SerializeField] GameObject _attackColider;

    [Header("UŒ‚‚Ì“–‚½‚è”»’è‚Ì—LŒøŠÔ")]
    [Tooltip("UŒ‚‚Ì“–‚½‚è”»’è‚Ì—LŒøŠÔ")]
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
        //“‚ğ‘•”õ‚µ‚Ä‚¢‚é
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

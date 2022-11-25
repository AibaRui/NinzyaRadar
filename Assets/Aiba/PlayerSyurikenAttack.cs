using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSyurikenAttack : MonoBehaviour
{
    [Header("Mian�J�����ɂ��Ă��镐��")]
    [Tooltip("Mian�J�����ɂ��Ă��镐��")] [SerializeField] GameObject _syuriken;

    [Header("�N�[���^�C��")]
    [Tooltip("�N�[���^�C��")]
    [SerializeField] float _coolTime = 1f;

    bool _isAttack = true;

    P_Control _control;

    [SerializeField] Animator _animSyuriken;

    [SerializeField] AudioSource _aud;


    Rigidbody m_rb;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        _control = FindObjectOfType<P_Control>();
        _animSyuriken = _animSyuriken.GetComponent<Animator>();
        _aud = _aud.GetComponent<AudioSource>();
    }


    void Update()
    {
           // Attack();
    }


   public void Attack()
    {
        //�藠���𑕔����Ă���
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
                _aud.Play();
            }

        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(_coolTime);
              _isAttack = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKatanaAttack : MonoBehaviour
{

    [SerializeField] GameObject _katana;

    bool _okAttack = false;
    bool _nowAttacking = false;


    PlayerMove _playerMove;
    [SerializeField] Animator m_anim;

    P_Control _control;

    [SerializeField] Animator _animKatana;

    Rigidbody m_rb;
    void Start()
    {
        _playerMove = FindObjectOfType<PlayerMove>();
        m_rb = GetComponent<Rigidbody>();
        if (m_anim)
        {
            m_anim = m_anim.gameObject.GetComponent<Animator>();
        }
        _control = FindObjectOfType<P_Control>();
        _animKatana = _animKatana.GetComponent<Animator>();

    }


    void Update()
    {
        if (_katana.activeSelf)
        {
            Attack();
            ScondAvirity();
        }
    }

    void ScondAvirity()
    {
        // �����̓��͂��擾���A���������߂�
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // ���͕����̃x�N�g����g�ݗ��Ă�
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        // �J��������ɓ��͂��㉺=��/��O, ���E=���E�ɃL�����N�^�[��������
        dir = Camera.main.transform.TransformDirection(dir);    // ���C���J��������ɓ��͕����̃x�N�g����ϊ�����
        dir.y = 0;  // y �������̓[���ɂ��Đ��������̃x�N�g���ɂ���

        if (Input.GetMouseButtonDown(1) && !_control._isAvirity)
        {
            _animKatana.Play("KatanaDash");
            _control._isAvirity = true;
            Debug.Log("!!!");
            m_rb.velocity = Vector3.zero;
            m_rb.AddForce(dir.normalized * 50, ForceMode.Impulse);
            StartCoroutine(a());
        }

        if (Input.GetMouseButtonUp(1))
        {

        }
    }

    IEnumerator a()
    {
        yield return new WaitForSeconds(0.3f);
        _control._isAvirity = false;
    }

    void Attack()
    {
        //���𑕔����Ă���
        if (_katana.activeSelf)
        {
            if (Input.GetMouseButton(0))
            {
                if (_nowAttacking)
                {
                    return;
                }
                _nowAttacking = true;
                _animKatana.Play("katanaAttack1");

            }
          StartCoroutine(CoolTime());
        }
    }
    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(0.5f);
        _nowAttacking = false;

        //var a = _animKatana.GetCurrentAnimatorStateInfo(0).normalizedTime;

        //if (a >= 1)
        //{
        //    _nowAttacking = false;
        //}

    }


}

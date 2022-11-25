using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKatanaAttack : MonoBehaviour
{

    [Header("Mian�J�����ɂ��Ă��镐��")]
    [Tooltip("Mian�J�����ɂ��Ă��镐��")] [SerializeField] GameObject _katana;

    [Header("�U���̓����蔻��")]
    [Tooltip("�U���̓����蔻��")] [SerializeField] GameObject _attackColider;

    [Header("�U���̓����蔻��̗L������")]
    [Tooltip("�U���̓����蔻��̗L������")]
    [SerializeField] float _attackTime = 0.1f;

    [SerializeField] int _maxCount = 4;

    bool _okAttack = false;
    bool _nowAttacking = false;

    private int count = 0;

    public int Count { get => count; set => count = value; }

    P_Control _control;

    [SerializeField] Animator _animKatana;

    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();

    [SerializeField] AudioSource _aud;

    public bool _isAttack = true;

    Rigidbody m_rb;
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        _control = FindObjectOfType<P_Control>();
        _animKatana = _animKatana.GetComponent<Animator>();
        _aud = _aud.GetComponent<AudioSource>();
    }

    void Update()
    {
        // Attack();
    }

    public void Attack()
    {
        //���𑕔����Ă���
        if (_katana.activeSelf)
        {
            var r = Random.Range(0, audioClips.Count);
            if (Input.GetMouseButtonDown(0) && _isAttack)
            {
                _isAttack = false;
                if (count < _maxCount)
                {

                    Debug.Log(count);
                    if (count == 0)
                    {
                        _animKatana.Play("katanaAttack");

                    }
                    else
                    {
                        _animKatana.SetTrigger("Attack");
                        // _aud.PlayOneShot(audioClips[r]);
                    }
                }
                    _aud.PlayOneShot(audioClips[r]);
            }


        }
    }

}

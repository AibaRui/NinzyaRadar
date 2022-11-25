using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Control : MonoBehaviour
{

    [SerializeField] Animator _animKatana;
    [SerializeField] Animator _animSyuriken;


    [Header("�ڒn����̍ہA���S (Pivot) ����ǂꂭ�炢�̋������u�ڒn���Ă���v�Ɣ��肷�邩�̒���")]
    [Tooltip("�ڒn����̍ہA���S (Pivot) ����ǂꂭ�炢�̋������u�ڒn���Ă���v�Ɣ��肷�邩�̒���")]
    [SerializeField] float _isGroundedLength = 1.1f;


    [Header("�n��ł̑��x����")]
    [Tooltip("�n��ł̑��x����")] [SerializeField] float _groundMove = 12;

    [Header("�X���C�f�B���O�̑��x����")]
    [Tooltip("�X���C�f�B���O�̑��x����")] [SerializeField] float _slidingMove;

    [Header("�󒆂ł̑��x����")]
    [Tooltip("�󒆂ł̑��x����")] [SerializeField] float _airMove;

    [Header("�W�����v���̑��x����")]
    [Tooltip("�W�����v���̑��x����")] [SerializeField] float _jumpMove;

    public PlayerAction playerAction = PlayerAction.OnGround;

    /// <summary>�Q�[�������ۂ�</summary>
    bool _isStartGame = false;

    public bool _isSliding;
    public bool _isSquat;
    public bool _isGround;
    public bool _isJump;


    private float _limitSpeedX;
    private float _limitSpeedZ;


    [SerializeField] PlayerMove _playerMove;
    [SerializeField] PlayerKatanaAttack _playerKatanaAttack;
    [SerializeField] PlayerSyurikenAttack _playerSyurikenAttack;
    [SerializeField] Sliding _sliding;
    [SerializeField] WeaponChange _weaponChange;
    [SerializeField] PlayerHideAvirity _hideAvirity;
    [SerializeField] PlayerDecoiAvirity _decoiAvirity;

    Rigidbody m_rb;
    void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _playerKatanaAttack = _playerKatanaAttack.GetComponent<PlayerKatanaAttack>();
        _playerSyurikenAttack = _playerSyurikenAttack.GetComponent<PlayerSyurikenAttack>();
        _sliding = GetComponent<Sliding>();
        _weaponChange = _weaponChange.GetComponent<WeaponChange>();
        _animKatana = _animKatana.GetComponent<Animator>();
        _animSyuriken = _animSyuriken.GetComponent<Animator>();
        _hideAvirity = _hideAvirity.GetComponent<PlayerHideAvirity>();
        _decoiAvirity = _decoiAvirity.GetComponent<PlayerDecoiAvirity>();
        m_rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (_isStartGame)
        {
            if (!_hideAvirity._isHide)
            {
                //PlayerMove�̏���
                _playerMove.Dir();
                _playerMove.Jump();
                if (playerAction == PlayerAction.OnGround)
                {
                    _playerMove.Move();
                    _playerMove.DownSpeed();
                }

                //Sliding�̏���
                _sliding.SlidingGo();

                //�U������
                _weaponChange.Chenge();
                _playerKatanaAttack.Attack();
                _playerSyurikenAttack.Attack();

                Check();
                SpeedLimit();
                _isGround = IsGrounded();

                _decoiAvirity.CoolTimeDecoiAvirity();
                _decoiAvirity.Decoi();
            }
            _hideAvirity.Hide();
        }
    }

    private void FixedUpdate()
    {
        if (_isStartGame)
        {
            //PlayerMove�̏���
            _playerMove.MoveAir(_playerMove._airVelo);
        }
    }


    private void LateUpdate()
    {
        _animKatana.SetBool("Ground", IsGrounded());
    }


    /// <summary>���x����</summary>
    void SpeedLimit()
    {

        if (m_rb.velocity.x >= _limitSpeedX)
        {
            m_rb.velocity = new Vector3(_limitSpeedX, m_rb.velocity.y, m_rb.velocity.z);
        }
        if (m_rb.velocity.x < -_limitSpeedX)
        {
            m_rb.velocity = new Vector3(-_limitSpeedX, m_rb.velocity.y, m_rb.velocity.z);
        }

        if (m_rb.velocity.z >= _limitSpeedZ)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, _limitSpeedZ);
        }
        if (m_rb.velocity.z < -_limitSpeedZ)
        {
            m_rb.velocity = new Vector3(m_rb.velocity.x, m_rb.velocity.y, -_limitSpeedZ);
        }

    }


    void Check()
    {
        if (_isSliding)
        {
            playerAction = PlayerAction.Sliding;

            _limitSpeedX = _slidingMove;
            _limitSpeedZ = _slidingMove;
            return;
        }

        if (IsGrounded())
        {
            playerAction = PlayerAction.OnGround;
            _limitSpeedX = _groundMove;
            _limitSpeedZ = _groundMove;
            return;
        }

        if (!IsGrounded())
        {
            playerAction = PlayerAction.Air;

            _limitSpeedX = _airMove;
            _limitSpeedZ = _airMove;
            return;
        }
    }

    /// <summary>�ݒu����</summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        //  Debug.Log("kk");
        // Physics.Linecast() ���g���đ���������𒣂�A�����ɉ������Փ˂��Ă����� true �Ƃ���
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center + new Vector3(0, 0, -0.3f);   // start: �̂̒��S
        Vector3 end = start + Vector3.down * _isGroundedLength;  // end: start ����^���̒n�_
        Debug.DrawLine(start, end, Color.green); // ����m�F�p�� Scene �E�B���h�E��Ő���\������
        bool isGrounded = Physics.Linecast(start, end); // ���������C���ɉ������Ԃ����Ă����� true �Ƃ���
        return isGrounded;
    }

    public enum PlayerAction
    {
        OnGround,
        Air,
        Sliding,
        WallRun,
        Slow,
        Climb,
    }

    public void OnStart()
    {
        _isStartGame = true;
    }

    public void OnEndGame()
    {
        _isStartGame = false;
        _animKatana.speed = 0;
        _animSyuriken.speed = 0;
    }

}

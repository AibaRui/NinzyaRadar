using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Control : MonoBehaviour
{

    [SerializeField] Animator _animKatana;
    [SerializeField] Animator _animSyuriken;


    [Header("接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ")]
    [Tooltip("接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ")]
    [SerializeField] float _isGroundedLength = 1.1f;


    [Header("地上での速度制限")]
    [Tooltip("地上での速度制限")] [SerializeField] float _groundMove = 12;

    [Header("スライディングの速度制限")]
    [Tooltip("スライディングの速度制限")] [SerializeField] float _slidingMove;

    [Header("空中での速度制限")]
    [Tooltip("空中での速度制限")] [SerializeField] float _airMove;

    [Header("ジャンプ時の速度制限")]
    [Tooltip("ジャンプ時の速度制限")] [SerializeField] float _jumpMove;

    public PlayerAction playerAction = PlayerAction.OnGround;

    /// <summary>ゲーム中か否か</summary>
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
                //PlayerMoveの処理
                _playerMove.Dir();
                _playerMove.Jump();
                if (playerAction == PlayerAction.OnGround)
                {
                    _playerMove.Move();
                    _playerMove.DownSpeed();
                }

                //Slidingの処理
                _sliding.SlidingGo();

                //攻撃処理
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
            //PlayerMoveの処理
            _playerMove.MoveAir(_playerMove._airVelo);
        }
    }


    private void LateUpdate()
    {
        _animKatana.SetBool("Ground", IsGrounded());
    }


    /// <summary>速度制限</summary>
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

    /// <summary>設置判定</summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        //  Debug.Log("kk");
        // Physics.Linecast() を使って足元から線を張り、そこに何かが衝突していたら true とする
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center + new Vector3(0, 0, -0.3f);   // start: 体の中心
        Vector3 end = start + Vector3.down * _isGroundedLength;  // end: start から真下の地点
        Debug.DrawLine(start, end, Color.green); // 動作確認用に Scene ウィンドウ上で線を表示する
        bool isGrounded = Physics.Linecast(start, end); // 引いたラインに何かがぶつかっていたら true とする
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

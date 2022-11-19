using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_Control : MonoBehaviour
{

    [SerializeField] Animator _animKatana;

    public PlayerAction playerAction = PlayerAction.OnGround;

    [Header("接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ")]
    [Tooltip("接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ")]
    [SerializeField] float _isGroundedLength = 1.1f;



    [SerializeField] float _groundMove = 12;
    [SerializeField] float _slidingMove;
    [SerializeField] float _airMove;
    [SerializeField] float _jumpMove;
    [SerializeField] float _gravity = 3;

    public bool _isWapon = false;


    public bool _isSliding;

    public bool _isAvirity =false;

    public bool _isSquat;
    public bool _isGround;
    public bool _isJump;

    private float _limitSpeedX;
    private float _limitSpeedZ;


    Rigidbody m_rb;
    void Start()
    {
        _animKatana = _animKatana.GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        Check();
        SpeedLimit();
        //T();

        _isGround = IsGrounded();
    }

    private void LateUpdate()
    {
        _animKatana.SetBool("Ground", IsGrounded());
    }


    //void T()
    //{
    //    _xSpeed.text = m_rb.velocity.x.ToString("00.0");
    //    _xSpeedLimit.text = _limitSpeedX.ToString();

    //    _zSpeed.text = m_rb.velocity.x.ToString("00.0");
    //    _zSpeedLimit.text = _limitSpeedZ.ToString();

    //    _Action.text = playerAction.ToString();
    //}



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

        //if (_isJump)
        //{
        //    playerAction = PlayerAction.JumpAir;

        //    _limitSpeedX = _jumpMove;
        //    _limitSpeedZ = _jumpMove;
        //    return;
        //}
        

        if (_isGround)
        {
            playerAction = PlayerAction.OnGround;
            _limitSpeedX = _groundMove;
            _limitSpeedZ = _groundMove;
            return;
        }

        if (!_isGround)
        {
            playerAction = PlayerAction.Air;

            _limitSpeedX = _airMove;
            _limitSpeedZ = _airMove;
            return;
        }



    }

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
        JumpAir,
        Air,
        Sliding,
        WallRun,
        Slow,
        Avirity,
        Climb,
    }
}

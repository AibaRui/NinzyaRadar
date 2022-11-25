using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpControl : MonoBehaviour
{
    [Header("体力")]
    [Tooltip("体力")] [SerializeField] int _hp = 5;
    private int _nowHp = 0;

    public int Hp() { return _nowHp; }

    [Header("敵の攻撃のタグの名前")]
    [Tooltip("敵の攻撃のタグの名前")] [SerializeField] string _damageTagName = "";

    [SerializeField] GameManager _gm;

    private void Awake()
    {
        _gm = _gm.GetComponent<GameManager>();
        _nowHp = _hp;
    }



    /// <summary>回復する</summary>
    public void lifeUp(int num)
    {
        _nowHp += num;
    }

    /// <summary>ダメージ処理</summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == _damageTagName)
        {
            _nowHp--;
            if (_nowHp < 0)
            {
                _gm.GameOver();
            }
        }
    }
}

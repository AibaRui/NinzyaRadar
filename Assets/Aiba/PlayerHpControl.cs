using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpControl : MonoBehaviour
{
    [Header("体力")]
    [Tooltip("体力")] [SerializeField] int _hp = 5;

    [Header("敵の攻撃のタグの名前")]
    [Tooltip("敵の攻撃のタグの名前")][SerializeField] string _damageTagName = "";

    [SerializeField] GameManager _gm;
    void Start()
    {
        _gm = GetComponent<GameManager>();
    }

    /// <summary>回復する</summary>
    public void lifeUp(int num)
    {
        _hp += num;
    }

    /// <summary>ダメージ処理</summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == _damageTagName)
        {
            _hp--;
            if(_hp<0)
            {
                _gm.GameOver();
            }
        }
    }
}

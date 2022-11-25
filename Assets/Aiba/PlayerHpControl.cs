using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpControl : MonoBehaviour
{
    [Header("�̗�")]
    [Tooltip("�̗�")] [SerializeField] int _hp = 5;
    private int _nowHp = 0;

    public int Hp() { return _nowHp; }

    [Header("�G�̍U���̃^�O�̖��O")]
    [Tooltip("�G�̍U���̃^�O�̖��O")] [SerializeField] string _damageTagName = "";

    [SerializeField] GameManager _gm;

    private void Awake()
    {
        _gm = _gm.GetComponent<GameManager>();
        _nowHp = _hp;
    }



    /// <summary>�񕜂���</summary>
    public void lifeUp(int num)
    {
        _nowHp += num;
    }

    /// <summary>�_���[�W����</summary>
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

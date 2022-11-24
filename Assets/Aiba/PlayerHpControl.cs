using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpControl : MonoBehaviour
{
    [Header("�̗�")]
    [Tooltip("�̗�")] [SerializeField] int _hp = 5;

    [Header("�G�̍U���̃^�O�̖��O")]
    [Tooltip("�G�̍U���̃^�O�̖��O")][SerializeField] string _damageTagName = "";

    [SerializeField] GameManager _gm;
    void Start()
    {
        _gm = GetComponent<GameManager>();
    }

    /// <summary>�񕜂���</summary>
    public void lifeUp(int num)
    {
        _hp += num;
    }

    /// <summary>�_���[�W����</summary>
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

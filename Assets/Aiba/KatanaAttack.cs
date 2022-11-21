using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaAttack : MonoBehaviour
{
    [Header("UŒ‚‚Ì“–‚½‚è”»’è")]
    [Tooltip("UŒ‚‚Ì“–‚½‚è”»’è")] [SerializeField] GameObject _attackColider;

    [Header("UŒ‚‚Ì“–‚½‚è”»’è‚Ì—LŒøŠÔ")]
    [Tooltip("UŒ‚‚Ì“–‚½‚è”»’è‚Ì—LŒøŠÔ")]
    [SerializeField] float _attackTime = 0.1f;
    void Start()
    {
        
    }

    public IEnumerator AttackCollider()
    {
        _attackColider.SetActive(true);
        yield return new WaitForSeconds(_attackTime);
        _attackColider.SetActive(false);
    }



    private void OnDisable()
    {
        _attackColider.SetActive(false);
    }
}

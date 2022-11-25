using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [Header("‰ñ•œ‚·‚é—Ê")]
    [Tooltip("‰ñ•œ‚·‚é—Ê")] [SerializeField] int _healHp = 1;

    [SerializeField] GameObject _aud;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           var go = Instantiate(_aud);
            go.transform.position = transform.position;
            GameObject.FindObjectOfType<PlayerHpControl>().lifeUp(_healHp);
            Destroy(gameObject);
        }
    }
}

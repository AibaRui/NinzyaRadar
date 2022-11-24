using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    [Header("�񕜂����")]
    [Tooltip("�񕜂����")] [SerializeField] int _healHp = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindObjectOfType<PlayerHpControl>().lifeUp(_healHp);
            Destroy(gameObject);
        }
    }
}

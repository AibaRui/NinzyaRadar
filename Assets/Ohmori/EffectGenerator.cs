using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGenerator : MonoBehaviour
{
    [Tooltip("生成するエフェクト"), SerializeField]
    GameObject _effect;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ぶつかった");
        if (other.gameObject.tag == "Enemy")
        {
            GameObject _tempObj = Instantiate(_effect);
            _tempObj.transform.position = other.transform.position;
            _tempObj.transform.LookAt(this.transform.position);
        }

    }
}

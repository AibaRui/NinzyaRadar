using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLife : MonoBehaviour
{
    [SerializeField] float _coolTime = 3;
    float _time = 0;

    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _coolTime)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

    }
}

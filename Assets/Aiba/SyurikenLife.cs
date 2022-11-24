using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyurikenLife : MonoBehaviour
{
    [SerializeField] float _coolTime = 3;
    float _time = 0;

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;

        if (_time >= _coolTime)
        {
            Destroy(gameObject);
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "")
    //    {

    //    }
    //    Destroy(gameObject);
    //}

}

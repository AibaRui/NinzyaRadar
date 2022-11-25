using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InfraredController : MonoBehaviour
{
    [Tooltip("赤いライン"), SerializeField]
    GameObject[] _redLines;
    [Tooltip("赤いラインに触れた際の処理"), SerializeField]
    UnityEvent _onTouchRedLine;
    [Tooltip("赤いラインが点滅するディスタンス"), SerializeField]
    float _distance = 10f;

    Collider _collider;
    float _timer;
    bool _setActive = true;

    private void Start()
    {
        _collider = (Collider) GetComponent<BoxCollider>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _distance)
        {
            _setActive = !_setActive;
            _collider.enabled = _setActive;

            foreach (var n in _redLines)
            {
                n.SetActive(_setActive);
            }

            _timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _onTouchRedLine.Invoke();
        }
    }
}

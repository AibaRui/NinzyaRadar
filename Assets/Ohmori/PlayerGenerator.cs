using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField, Tooltip("プレイヤーのスポーン地点")]
    Transform[] _generateTransform;

    [SerializeField, Tooltip("プレイヤーのプレハブ")]
    GameObject _playerPrefab;

    public void GeneratePlayer()
    {
        int randomNum = Random.Range(0, _generateTransform.Length);
        GameObject player = Instantiate(_playerPrefab);
        player.transform.position = _generateTransform[randomNum].position;
    }
}

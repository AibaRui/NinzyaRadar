using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] _goals;

    private void Start()
    {
        var temp = Random.Range(0, _goals.Length);

        _goals[temp].SetActive(true);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("HPスライダー")] Slider _hpSlider;
    [SerializeField, Header("タイマーテキスト")] Text _timerText;
    [SerializeField, Header("操作説明のパネル")] GameObject _instructionsPanel;
    [SerializeField] int _playerHP;
    float _gameTime;
    // Start is called before the first frame update
    void Start()
    {
        //_gameTime = 
        //_playerHP = 
        _hpSlider.maxValue = _playerHP;
        _hpSlider.value = _playerHP;
    }

    // Update is called once per frame
    void Update()
    {
        //_playerHP = 
        _hpSlider.value = _playerHP;
        if(Input.GetKeyDown(KeyCode.Tab) && _instructionsPanel.active == false)
        {
            _instructionsPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            _instructionsPanel.SetActive(false);
        }
    }
}

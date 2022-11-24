using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("HP�X���C�_�[")] Slider _hpSlider;
    [SerializeField, Header("��������̃p�l��")] GameObject _instructionsPanel;
    [SerializeField, Header("�Q�[���I�[�o�[�̃p�l��")] GameObject _gameoverPanel;
    [SerializeField, Header("�Q�[���N���A�̃p�l��")] GameObject _gameclearPanel;
    float _playerHP;
    // Start is called before the first frame update
    void Start()
    {
        _gameoverPanel.SetActive(false);
        _instructionsPanel.SetActive(false);
        _gameclearPanel.SetActive(false);
        _playerHP = FindObjectOfType<PlayerHpControl>().Hp();
        _hpSlider.maxValue = _playerHP;
        _hpSlider.value = _playerHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerHpControl>())
        {
            _playerHP = _playerHP = FindObjectOfType<PlayerHpControl>().Hp();
        }
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

    void GameOver()
    {
        _gameoverPanel.SetActive(true);
    }

    void GameClear()
    {
        _gameclearPanel.SetActive(true);
    }
}

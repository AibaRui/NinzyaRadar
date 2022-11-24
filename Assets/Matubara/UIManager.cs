using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("HP�X���C�_�[")] Slider _hpSlider;
    [SerializeField, Header("�^�C�}�[�e�L�X�g")] Text _timerText;
    [SerializeField, Header("��������̃p�l��")] GameObject _instructionsPanel;
    [SerializeField] float _playerHP;
    float _gameTime;
    // Start is called before the first frame update
    void Start()
    {
        _instructionsPanel.SetActive(false);
        _playerHP = FindObjectOfType<TMPPlayerScript>().HP;
        _hpSlider.maxValue = _playerHP;
        _hpSlider.value = _hpSlider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        _playerHP = FindObjectOfType<TMPPlayerScript>().HP;
        if (Input.GetKeyDown(KeyCode.Tab) && _instructionsPanel.active == false)
        {
            _instructionsPanel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            _instructionsPanel.SetActive(false);
        }
        ChangeSlider();
    }
    void ChangeSlider()
    {
        _hpSlider.value = _playerHP;
    }
}

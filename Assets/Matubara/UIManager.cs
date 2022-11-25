using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("HPスライダー")] Slider _hpSlider;
    [SerializeField, Header("操作説明のパネル")] GameObject _instructionsPanel;
    [SerializeField, Header("ゲームオーバーのパネル")] GameObject _gameoverPanel;
    [SerializeField, Header("ゲームクリアのパネル")] GameObject _gameclearPanel;
    int _playerHP;
    // Start is called before the first frame update
    void Start()
    {
        _gameoverPanel.SetActive(false);
        _instructionsPanel.SetActive(false);
        _gameclearPanel.SetActive(false);
        _playerHP =GameObject.FindObjectOfType<PlayerHpControl>().Hp();
        _hpSlider = _hpSlider.GetComponent<Slider>();
        _hpSlider.maxValue = _playerHP;
       
        Debug.Log(_hpSlider.maxValue);
        _hpSlider.minValue = 0;
        _hpSlider.value = _playerHP;
    }

    // Update is called once per frame
    void Update()
    {

        if (FindObjectOfType<PlayerHpControl>())
        {
            _playerHP = FindObjectOfType<PlayerHpControl>().Hp();
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

  public  void GameOver()
    {
        _gameoverPanel.SetActive(true);
    }

  public  void GameClear()
    {
        _gameclearPanel.SetActive(true);
    }
}

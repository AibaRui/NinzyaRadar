using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MouseSencivity : MonoBehaviour
{
    [SerializeField] Slider _sensitivitySlider;
    CinemachinePOV _camera;
    [SerializeField] CinemachineVirtualCamera _ca;
    [SerializeField] float _maxSensitivity = 300;
    [SerializeField] float _mixSensitivity = 50;

    [SerializeField] GameObject _panel;

    bool _isPause = false;
    void Start()
    {
        _camera = _ca.GetCinemachineComponent<CinemachinePOV>();
        _sensitivitySlider = _sensitivitySlider.GetComponent<Slider>();

        //スライダーの最大値の設定
        _sensitivitySlider.maxValue = _maxSensitivity;

        //スライダーの現在値の設定
        _sensitivitySlider.minValue = _mixSensitivity;

        _panel.SetActive(_isPause);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;
            _panel.SetActive(_isPause);

            if(_isPause)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    public void ChangeSensitivity(float value)
    {
        _camera.m_HorizontalAxis.m_MaxSpeed = value;
        _camera.m_VerticalAxis.m_MaxSpeed = value;
    }


}

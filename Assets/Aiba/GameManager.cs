using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using System.Collections;
public class GameManager : MonoBehaviour
{
    [Header("�Q�[���̂��ׂĂ̓G")]
    [Tooltip("�Q�[���̂��ׂĂ̓G")]
    List<GameObject> _enemies = new List<GameObject>();

    [Header("�X�^�[�g�J�E���g��Text")]
    [Tooltip("�X�^�[�g�J�E���g��Text")] [SerializeField] Text _startCountText;

    [Header("�^�C���J�E���g��Text")]
    [Tooltip("�^�C���J�E���g��Text")] [SerializeField] Text _timeCountText;

    [SerializeField] float _timeLimit = 180;
    float _countTime;

    [Header("�J�n���ɌĂяo������")]
    [Tooltip("�J�n���ɌĂяo������")] [SerializeField] UnityEvent _onGameStart;

    [Header("GameClear���ɌĂяo������")]
    [Tooltip("GameClear���ɌĂяo������")] [SerializeField] UnityEvent _onGameClear;

    [Header("GameOver���ɌĂяo������")]
    [Tooltip("GameOver���ɌĂяo������")] [SerializeField] UnityEvent _onGameOver;

    bool _isStart = false;

    [SerializeField] StartSituation _startSituation = StartSituation.WaitSecound3;
    int count = 0;
    void Start()
    {
        if (_startSituation == StartSituation.WaitSecound3)
        {
            StartCoroutine(StartCount());
        }
    }


    void Update()
    {
        ClickStart();

        if (_isStart)
        {
            CountTime();
        }

    }

    /// <summary>�N���b�N������Q�[���J�n</summary>
    void ClickStart()
    {
        if (count > 1 || _startSituation == StartSituation.WaitSecound3)
        {
            return;
        }

        if (Input.anyKey)
        {
            count++;
            _enemies.ForEach(enemy => enemy.gameObject.SetActive(true));
            _onGameStart.Invoke();
            _isStart = true;
        }
    }

    /// <summary>�X�^�[�g�J�E���g�̃e�L�X�g���o���ēG���o��</summary>
    /// <returns></returns>
    IEnumerator StartCount()
    {
        _startCountText.text = "3";
        yield return new WaitForSeconds(1);
        _startCountText.text = "2";
        yield return new WaitForSeconds(1);
        _startCountText.text = "1";
        yield return new WaitForSeconds(1);
        _startCountText.text = "Start";
        _enemies.ForEach(enemy => enemy.gameObject.SetActive(true));
        _isStart = true;
        _onGameStart.Invoke();
        yield return new WaitForSeconds(1);
        _startCountText.text = "";
    }

    void CountTime()
    {
        _countTime += Time.deltaTime;
        if (_countTime >= 1)
        {
            _countTime = 0;
            _timeLimit--;
            _timeCountText.text = _timeLimit.ToString();
        }
    }

    public void GameOver()
    {
        _enemies.ForEach(enemy => enemy.gameObject.SetActive(false));
        _onGameOver.Invoke();
    }

    public void GameClear()
    {
        _enemies.ForEach(enemy => enemy.gameObject.SetActive(false));
        _onGameClear.Invoke();
    }




    enum StartSituation
    {
        WaitSecound3,
        ClickStart
    }
}

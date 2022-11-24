using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using System.Collections;
public class GameManager : MonoBehaviour
{
    [Header("ゲームのすべての敵")]
    [Tooltip("ゲームのすべての敵")]
    List<GameObject> _enemies = new List<GameObject>();

    [Header("スタートカウントのText")]
    [Tooltip("スタートカウントのText")] [SerializeField] Text _startCountText;

    [Header("タイムカウントのText")]
    [Tooltip("タイムカウントのText")] [SerializeField] Text _timeCountText;

    [SerializeField] float _timeLimit = 180;
    float _countTime;

    [Header("開始時に呼び出す処理")]
    [Tooltip("開始時に呼び出す処理")] [SerializeField] UnityEvent _onGameStart;

    [Header("GameClear時に呼び出す処理")]
    [Tooltip("GameClear時に呼び出す処理")] [SerializeField] UnityEvent _onGameClear;

    [Header("GameOver時に呼び出す処理")]
    [Tooltip("GameOver時に呼び出す処理")] [SerializeField] UnityEvent _onGameOver;

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

    /// <summary>クリックしたらゲーム開始</summary>
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

    /// <summary>スタートカウントのテキストを出して敵を出す</summary>
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

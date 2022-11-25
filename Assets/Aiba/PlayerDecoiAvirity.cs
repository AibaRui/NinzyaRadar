using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDecoiAvirity : MonoBehaviour
{
    [SerializeField] GameObject _decoi;

    [SerializeField] float _boxCastX = 1;
    [SerializeField] float _boxCastY = 1;
    [SerializeField] float _boxCastZ = 1;
    [SerializeField] Vector3 posAdd;
    [SerializeField] float _downLong;

    [SerializeField] float _upLayerLong;

    [SerializeField] RaycastHit _hit;

    [SerializeField] LayerMask _wallLayre;

    /// <summary>‘O•û‚É•Ç‚ª‚ ‚é‚©‚Ç‚¤‚©</summary>
    bool _fowardWall;

    [SerializeField] float _lineDir = 3;

    [SerializeField] float _coolTime = 5;
    private float _countTime = 0;

    [SerializeField] AudioSource _aud;
    bool _isCoolDown;

    [SerializeField] Text _ctText;
    [SerializeField] GameObject _panel;

    private void Start()
    {
                    _aud = _aud.GetComponent<AudioSource>();
    }

    public void Decoi()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(_isCoolDown || CheckFowardWall())
            {
                return;
            }
            _aud.Play();
            _isCoolDown = true;
            var go = Instantiate(_decoi);
            go.transform.position = transform.position+transform.forward*1.5f;
            go.transform.forward = transform.forward;
            _countTime = _coolTime;
            _panel.SetActive(true);
        }
    }

    public void CoolTimeDecoiAvirity()
    {
        if (_isCoolDown)
        {

            _countTime -= Time.deltaTime;
            _ctText.text = _countTime.ToString("0");

            if (_countTime <=0)
            {
                _isCoolDown = false;
                _ctText.text = "Q";
                _panel.SetActive(false);
            }
        }
    }

    private bool CheckFowardWall()
    {
        _fowardWall = Physics.BoxCast(transform.position + transform.forward + posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ), transform.forward, Quaternion.identity, 1.0f, _wallLayre);

        return _fowardWall;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + transform.forward + posAdd, new Vector3(_boxCastX, _boxCastY, _boxCastZ));    
    }
}

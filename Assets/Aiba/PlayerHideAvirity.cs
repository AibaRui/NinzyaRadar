using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class PlayerHideAvirity : MonoBehaviour
{
    [SerializeField] GameObject _hideWallObject;
    [SerializeField] GameObject _hideGroundObject;

    [SerializeField] List<GameObject> _weapon = new List<GameObject>();

    [SerializeField] float _lineDir = 2;

    [SerializeField] LayerMask _wallLayre;

    [SerializeField] CinemachineVirtualCamera _hideCamera;

    [SerializeField] GameObject _baseCamera;

    Vector3 _pos;

    [SerializeField] GameObject _playerBody;

    /// <summary>変わり身のインスタンス</summary>
    GameObject _go;

    RaycastHit _hit;

    //隠れているか否かの判断
    public bool _isHide = false;
    Rigidbody _rb;

    void Start()
    {
        _hideCamera = _hideCamera.GetComponent<CinemachineVirtualCamera>();
        _rb = GetComponent<Rigidbody>();
    }

    private bool CheckWall()
    {
        bool fowardHit = Physics.Raycast(transform.position, transform.forward, out _hit, _lineDir, _wallLayre);
        return fowardHit;
    }


    public void Hide()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!_isHide)
            {
                if (CheckWall())
                {
                    _isHide = true;
                    _pos = transform.position;
                    _rb.isKinematic = true;

                    var go = HideObject();
                    _hideCamera.Priority = 100;
                    _hideCamera.Follow = go.transform;
                    _hideCamera.LookAt = go.transform;

                    _playerBody.GetComponent<Collider>().enabled = false;
                    _playerBody.GetComponent<MeshRenderer>().enabled = false;

                    _weapon.ForEach(i => i.SetActive(false));
                }
            }
            else
            {
                _isHide = false;
                transform.position = _pos;
                _rb.isKinematic = false;

                Destroy(_go);
                _hideCamera.Priority = 1;
                _weapon[0].SetActive(true);

                _playerBody.GetComponent<Collider>().enabled = true;
                _playerBody.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }


    private GameObject HideObject()
    {
        _go = Instantiate(_hideWallObject);
        Vector3 p = transform.position - _hit.point;
        p = new Vector3(p.normalized.x * 0.2f, 0, p.normalized.z * 0.5f);
        _go.transform.position = _hit.point += p;
        _go.transform.LookAt(transform.position);
        return _go;
    }

}

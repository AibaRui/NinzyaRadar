using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : MonoBehaviour
{

    [Header("Mian�J�����ɂ��Ă��镐��")]
    [Tooltip("Mian�J�����ɂ��Ă��镐��")] [SerializeField] GameObject[] _weapons = new GameObject[1];

    int num = 1;
    void Start()
    {
        _weapons[num - 1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //Chenge();
    }

   public void Chenge()
    {
        float wh = Input.GetAxis("Mouse ScrollWheel");
        if (wh != 0)
        {
            if (wh > 0)
            {
                _weapons[num - 1].SetActive(false);
                num -= 1;
                if (num == 0)
                {
                    num = _weapons.Length;
                }
                _weapons[num - 1].SetActive(true);
                Debug.Log(num);
            }
            else if (wh < 0)
            {
                _weapons[num - 1].SetActive(false);

                num += 1;
                if (num > _weapons.Length)
                {
                    num = 1;
                }
                Debug.Log(num);

            }
            _weapons[num - 1].SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMPPlayerScript : MonoBehaviour
{
    public float HP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        HP -= 1;
    }
}

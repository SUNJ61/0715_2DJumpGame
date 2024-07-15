using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObj : MonoBehaviour
{
    private Transform tr;
    private GameObject PlatformObj;

    private float Speed = 10.0f;
    private string PlatformStr = "Platform";
    void Start()
    {
        tr = GetComponent<Transform>();
        PlatformObj = GetComponent<GameObject>();
    }
    void Update()
    {
        if(GameManager.instance.isGameOver == false)
            tr.Translate(Vector3.left * Speed * Time.deltaTime);
    }
}

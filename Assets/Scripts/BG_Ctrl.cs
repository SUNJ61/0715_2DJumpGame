using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Ctrl : MonoBehaviour
{
    private float width;
    void Start()
    {
        BoxCollider2D box = GetComponent<BoxCollider2D>();
        width = box.size.x;
        StartCoroutine(BackGroundMove());
    }

    private void RePosition()
    {
        Vector2 offset = new Vector2(width * 2, 0);
        transform.position = (Vector2)transform.position + offset;
    }

    IEnumerator BackGroundMove()
    {
        while(!GameManager.instance.isGameOver)
        {
            yield return new WaitForSeconds(0.02f);
            if(transform.position.x <= -width)
            {
                RePosition();
            }
        }
    }
}

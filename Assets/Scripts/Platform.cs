using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; //장애물 오브젝트

    private string PlayerStr = "Player";
    private bool stepped = false; //플레이어 캐릭터가 밟았는지 확인하는 변수

    void OnEnable() //Awake -> OnEnable -> Start 함수 순으로 호출된다.
    {//컴퍼넌트가 활성화 될 때마다  매번 실행 되는 매소드이다.  여기서 발판을 리셋 처리를 한다.
        stepped = false; //스크립트가 다시 실행될 때 이전 값을 가지고 있을 수도 있어 false로 초기화.
        for(int i = 0; i < obstacles.Length; i++) //장애물 수만큼 loop
        {
            if(Random.Range(0,3) == 0)
                obstacles[i].SetActive(true); //3분의 1확률로 가시가 꺼졌다 켜졌다 한다. (0이 나온 가시들은 오브젝트가 꺼진다.)
            else
                obstacles[i].SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {//플레이어 캐릭터가 발판(자신)을 밟으면 점수를 올린다.
        if (collision.collider.CompareTag(PlayerStr) && !stepped) //밟은 상태가 아니고, 밟은 오브젝트가 플레이어라면
        {
            stepped = true; //밟았음을 알려줌.
            GameManager.instance.AddScore(1);
        }
    }
}

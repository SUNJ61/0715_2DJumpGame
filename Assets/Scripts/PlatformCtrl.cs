using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformCtrl : MonoBehaviour
{
    public GameObject platformPrefab;
    public int count = 3;

    public float SpawnMin = 1.25f;
    public float SpawnMax = 2.25f;
    private float timeSpawn; //다음배치까지의 간격

    public float ymin = -3.5f;
    public float ymax = 1.5f;
    private float xPos = 20f;

    private GameObject[] Platforms; //미리 생성할 발판
    private int currentIndex = 0; //현재 순번의 발판 인덱스

    private Vector2 poolPosition = new Vector2(0, -25);
    private float lastSpawnTime; //마지막 배치 시간

    void Start() // 유니티 이벤트 함수중 제일 빨리 호출 됨 (start보다 빠르게 호출)
    {
        Platforms = new GameObject[count]; //count만큼의 공간을 가지는 배열 생성

        for(int i = 0; i < count; i++)
        { //가져온 프리팹을 원본으로 pool포지션 위치에 새 발판 생성, 생성된 발판을 플랫폼에 할당
            Platforms[i] = Instantiate(platformPrefab, poolPosition, Quaternion.identity);
        }

        lastSpawnTime = 0f;
        timeSpawn = 0f;
    }
    void Update()
    {
        if (GameManager.instance.isGameOver) return;
        if(Time.time >= lastSpawnTime + timeSpawn)
        {
            lastSpawnTime = Time.time;
            timeSpawn = Random.Range(SpawnMin, SpawnMax);
            float yPos = Random.Range(ymin, ymax);

            Platforms[currentIndex].SetActive(false); //현재 순번의 게임 오브젝트를 비활성화 하고 즉시 다시 활성화
            Platforms[currentIndex].SetActive(true); //이때 발판의 onEnable매소드가 실행됨.
            Platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            currentIndex++; //다음 인덱스로 증가
            if(currentIndex >= count)
            {
                currentIndex = 0; //인덱스 범위 밖으로 증가하면 초기화
            }
        }
    }
}

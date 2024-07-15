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
    private float timeSpawn; //������ġ������ ����

    public float ymin = -3.5f;
    public float ymax = 1.5f;
    private float xPos = 20f;

    private GameObject[] Platforms; //�̸� ������ ����
    private int currentIndex = 0; //���� ������ ���� �ε���

    private Vector2 poolPosition = new Vector2(0, -25);
    private float lastSpawnTime; //������ ��ġ �ð�

    void Start() // ����Ƽ �̺�Ʈ �Լ��� ���� ���� ȣ�� �� (start���� ������ ȣ��)
    {
        Platforms = new GameObject[count]; //count��ŭ�� ������ ������ �迭 ����

        for(int i = 0; i < count; i++)
        { //������ �������� �������� pool������ ��ġ�� �� ���� ����, ������ ������ �÷����� �Ҵ�
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

            Platforms[currentIndex].SetActive(false); //���� ������ ���� ������Ʈ�� ��Ȱ��ȭ �ϰ� ��� �ٽ� Ȱ��ȭ
            Platforms[currentIndex].SetActive(true); //�̶� ������ onEnable�żҵ尡 �����.
            Platforms[currentIndex].transform.position = new Vector2(xPos, yPos);

            currentIndex++; //���� �ε����� ����
            if(currentIndex >= count)
            {
                currentIndex = 0; //�ε��� ���� ������ �����ϸ� �ʱ�ȭ
            }
        }
    }
}

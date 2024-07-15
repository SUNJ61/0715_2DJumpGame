using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; //��ֹ� ������Ʈ

    private string PlayerStr = "Player";
    private bool stepped = false; //�÷��̾� ĳ���Ͱ� ��Ҵ��� Ȯ���ϴ� ����

    void OnEnable() //Awake -> OnEnable -> Start �Լ� ������ ȣ��ȴ�.
    {//���۳�Ʈ�� Ȱ��ȭ �� ������  �Ź� ���� �Ǵ� �żҵ��̴�.  ���⼭ ������ ���� ó���� �Ѵ�.
        stepped = false; //��ũ��Ʈ�� �ٽ� ����� �� ���� ���� ������ ���� ���� �־� false�� �ʱ�ȭ.
        for(int i = 0; i < obstacles.Length; i++) //��ֹ� ����ŭ loop
        {
            if(Random.Range(0,3) == 0)
                obstacles[i].SetActive(true); //3���� 1Ȯ���� ���ð� ������ ������ �Ѵ�. (0�� ���� ���õ��� ������Ʈ�� ������.)
            else
                obstacles[i].SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {//�÷��̾� ĳ���Ͱ� ����(�ڽ�)�� ������ ������ �ø���.
        if (collision.collider.CompareTag(PlayerStr) && !stepped) //���� ���°� �ƴϰ�, ���� ������Ʈ�� �÷��̾���
        {
            stepped = true; //������� �˷���.
            GameManager.instance.AddScore(1);
        }
    }
}

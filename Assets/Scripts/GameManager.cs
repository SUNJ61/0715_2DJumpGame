using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Text ScoreText;
    private GameObject gameUI;
    private Text GameScoreText;

    private string CanvasStr = "Canvas_UI";
    private string ScoreStr = "ScoreText";
    public bool isGameOver = false;
    public int Score = 0;

    void Awake() //�Ŵ��� Ŭ������ ���� ���� �ҷ��;��ϱ⶧���� awake���
    {
        if (instance == null) //�ν��Ͻ��� �������� �ʾ��� ��
            instance = this; //���� �Ҵ�
        else if (instance != this) //���� �ν��Ͻ��� �ڽŰ� ���� �ʴٸ�
            Destroy(gameObject); //������Ʈ ����

        //DontDestroyOnLoad(gameObject); //���� ������ �Ѿ���� ���� �Ŵ��� ������Ʈ�� �������� �ʰ� ����.
        ScoreText = GameObject.Find(CanvasStr).transform.GetChild(1).GetComponent<Text>();
        GameScoreText = GameObject.Find(ScoreStr).GetComponent<Text>();
        //gameOverObj = GameObject.Find("GameOverText").GetComponent<GameObject>(); //���ӿ�����Ʈ�� �����־ ã�� �� ����.
        gameUI = GameObject.Find(CanvasStr).transform.GetChild(1).GetComponent<GameObject>();
        isGameOver = false;
    }
    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0)) //���ӿ��� ���¿��� ���콺 ���� ��ư�� �����ٸ�
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //���� Ȱ��ȭ �� ���� ������Ѵ�.
        }
    }

    public void AddScore(int newScore)
    {
        Score += newScore;
        ScoreText.text = $"Score : {Score.ToString()}";
        GameScoreText.text = $"Score : {Score.ToString()}";
    }
    public void OnPlayerDead()
    {
        isGameOver = true;
        ScoreText.gameObject.SetActive(true);
        GameScoreText.gameObject.SetActive(false);
    }
}

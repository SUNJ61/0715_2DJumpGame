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

    void Awake() //매니저 클래스는 가장 먼저 불러와야하기때문에 awake사용
    {
        if (instance == null) //인스턴스가 생성되지 않았을 때
            instance = this; //동적 할당
        else if (instance != this) //현재 인스턴스가 자신과 같지 않다면
            Destroy(gameObject); //오브젝트 삭제

        //DontDestroyOnLoad(gameObject); //다음 씬으로 넘어가더라도 게임 매니저 오브젝트를 없어지지 않게 설정.
        ScoreText = GameObject.Find(CanvasStr).transform.GetChild(1).GetComponent<Text>();
        GameScoreText = GameObject.Find(ScoreStr).GetComponent<Text>();
        //gameOverObj = GameObject.Find("GameOverText").GetComponent<GameObject>(); //게임오브젝트가 꺼져있어서 찾을 수 없음.
        gameUI = GameObject.Find(CanvasStr).transform.GetChild(1).GetComponent<GameObject>();
        isGameOver = false;
    }
    void Update()
    {
        if (isGameOver && Input.GetMouseButtonDown(0)) //게임오버 상태에서 마우스 왼쪽 버튼을 누른다면
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //현재 활성화 된 씬을 재시작한다.
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

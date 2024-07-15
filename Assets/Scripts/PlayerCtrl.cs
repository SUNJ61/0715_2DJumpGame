using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]
    private AudioClip deathClip; //리소스 공간에 담아 스크립트로 가져온다.
    private AudioSource audioSource;
    private Rigidbody2D rbody2D;
    private Animator animator;

    private readonly string DieStr = "die";
    private readonly string IsGroundedStr = "IsGrounded";
    private readonly string DieTriggerStr = "DieTrigger";
    private readonly string PlayerStr = "Player";
    private readonly string DeadStr = "Dead";

    private bool IsGrounded = false;
    private bool IsDead = false;

    private float jumpForce = 600.0f;
    private int jumpCount = 0;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        deathClip = Resources.Load(DieStr) as AudioClip;
        //Resource폴더에 die파일을 오디오 클립으로 형변환 한다. 형변환 실패시 오류발생 x, 하지만 null값이 들어감.
        //deathClip = (AudioClip) Resources.Load(DieStr) as AudioClip; //이런식으로도 형변환 가능, 변환 실패시 오류 발생.
        IsDead = false;
    }
    void Update()
    {
        if (IsDead) return; //죽으면 업데이트 하지않기.
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++; //점프 횟수 증가, 점프 1회만 허용

            rbody2D.velocity = Vector3.zero; //점프 직전에 순간적으로 속도를 (0,0)으로 변경
            rbody2D.AddForce(new Vector2(0f, jumpForce)); //y축으로만 힘을 더하고 x축은 0인 상태로 유지
            audioSource.Play(); // 오디오 소스에 넣어둔 점프 클립 재생
        }
        else if (Input.GetMouseButtonUp(0) && rbody2D.velocity.y > 0) //마우스 왼쪽 버튼을 때고 y축 속도가 0보다 클때
        {
            rbody2D.velocity = rbody2D.velocity * 0.5f; //점프로 올라가는 속도가 점차 줄어든다.
        }
        animator.SetBool(IsGroundedStr, !IsGrounded); //애니메이션 파라미터 업데이트
    }
    private void OnTriggerEnter2D(Collider2D other)
    {// 트리거 콜라이더를 가진 장애물과 충돌을 감지
        if(other.CompareTag(DeadStr) && !IsDead)
        {
            Die();
            Destroy(other.gameObject);
            GameManager.instance.isGameOver = true;
        }
    }

    void Die()
    {
        animator.SetTrigger(DieTriggerStr);
        audioSource.clip = deathClip; //오디오 소스 내의 클립을 jump에서 die클립으로 변경 
        audioSource.Play(); // 오디오소스 플레이
        rbody2D.velocity = Vector2.zero; //죽으면 속도 없애기
        GameManager.instance.OnPlayerDead();
        IsDead = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {// 플레이어가 바닥에 닿았을 때 감지
        if (collision.contacts[0].normal.y > 0.7f) //어떤 콜라이더와 충돌했으며 충돌경사가 0.7보다 크면
        {//어떤 표면의 노말벡터의 y값이 1.0인 경우 해당 표면의 방향은 위쪽
            IsGrounded = true;
            jumpCount = 0;
        }//즉, 플레이어와 바닥이 닿은 지점의 기울기가 0.7보다 크다면 올라가는 판정을 받는다.
    }

    private void OnCollisionExit2D(Collision2D collision)
    {//바닥을 벗어난 것을 감지
        IsGrounded = false;
    }
}

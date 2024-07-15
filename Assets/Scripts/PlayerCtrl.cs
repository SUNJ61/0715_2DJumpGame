using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField]
    private AudioClip deathClip; //���ҽ� ������ ��� ��ũ��Ʈ�� �����´�.
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
        //Resource������ die������ ����� Ŭ������ ����ȯ �Ѵ�. ����ȯ ���н� �����߻� x, ������ null���� ��.
        //deathClip = (AudioClip) Resources.Load(DieStr) as AudioClip; //�̷������ε� ����ȯ ����, ��ȯ ���н� ���� �߻�.
        IsDead = false;
    }
    void Update()
    {
        if (IsDead) return; //������ ������Ʈ �����ʱ�.
        if (Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++; //���� Ƚ�� ����, ���� 1ȸ�� ���

            rbody2D.velocity = Vector3.zero; //���� ������ ���������� �ӵ��� (0,0)���� ����
            rbody2D.AddForce(new Vector2(0f, jumpForce)); //y�����θ� ���� ���ϰ� x���� 0�� ���·� ����
            audioSource.Play(); // ����� �ҽ��� �־�� ���� Ŭ�� ���
        }
        else if (Input.GetMouseButtonUp(0) && rbody2D.velocity.y > 0) //���콺 ���� ��ư�� ���� y�� �ӵ��� 0���� Ŭ��
        {
            rbody2D.velocity = rbody2D.velocity * 0.5f; //������ �ö󰡴� �ӵ��� ���� �پ���.
        }
        animator.SetBool(IsGroundedStr, !IsGrounded); //�ִϸ��̼� �Ķ���� ������Ʈ
    }
    private void OnTriggerEnter2D(Collider2D other)
    {// Ʈ���� �ݶ��̴��� ���� ��ֹ��� �浹�� ����
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
        audioSource.clip = deathClip; //����� �ҽ� ���� Ŭ���� jump���� dieŬ������ ���� 
        audioSource.Play(); // ������ҽ� �÷���
        rbody2D.velocity = Vector2.zero; //������ �ӵ� ���ֱ�
        GameManager.instance.OnPlayerDead();
        IsDead = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {// �÷��̾ �ٴڿ� ����� �� ����
        if (collision.contacts[0].normal.y > 0.7f) //� �ݶ��̴��� �浹������ �浹��簡 0.7���� ũ��
        {//� ǥ���� �븻������ y���� 1.0�� ��� �ش� ǥ���� ������ ����
            IsGrounded = true;
            jumpCount = 0;
        }//��, �÷��̾�� �ٴ��� ���� ������ ���Ⱑ 0.7���� ũ�ٸ� �ö󰡴� ������ �޴´�.
    }

    private void OnCollisionExit2D(Collision2D collision)
    {//�ٴ��� ��� ���� ����
        IsGrounded = false;
    }
}

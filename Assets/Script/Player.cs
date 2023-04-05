using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
/*�÷��̾� �̵�, �浹 ��� */
public class Player : MonoBehaviour
{
    public Vector2 inputVec; //Ű���� �Է°� ����
    public float speed; //�̵� �ӵ�
    public Scanner scaneer; //scanner.cs����
    public Hand[] hands; //Hand.cs ���� (�÷��̾� ����)
    public RuntimeAnimatorController[] animCon; //�÷��̾� ��ũ��Ʈ�� ���� �ִϸ����� ��Ʈ�ѷ��� ������ �迭 ���� ����
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriter;
    private static readonly int Speed = Animator.StringToHash("Speed");//Hash�� �̸� ĳ��

    private void Awake() //�ʱ�ȭ
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        scaneer = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void OnEnable()//Ȱ��ȭ �ɶ� ���ӸŴ����� id�� ���� ĳ���� �ִϸ��̼� ����
    {
        speed *= Character.Speed; //ĳ���� ���ý� ĳ���Ϳ� ���� ���ǵ带 ���Ѵ�.
        anim.runtimeAnimatorController = animCon[GameManager.Instance.playerId];
    }

    private void OnMove(InputValue value) //Input System���� value�� �޾Ƽ� inputVec�� �� ���� 
    {
        if (!GameManager.Instance.isLive)
            return;
        inputVec = value.Get<Vector2>();
    }
    private void FixedUpdate() //���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    {
        Vector2 nextVec = inputVec * (speed * Time.fixedDeltaTime);
        rigid.MovePosition(rigid.position + nextVec); //��ġ �̵� : ���� rigid ��ġ���� input ���� �����ִ� ����
    }
    private void LateUpdate() //�������� ����Ǳ� ������ ����Ǵ� �����ֱ� �Լ�
    {
        if (!GameManager.Instance.isLive)
            return;
        anim.SetFloat(Speed,inputVec.magnitude);
        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0; //inputVec.x �� 0���� ������ flipx�� true�� �ȴ� 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive) { return; } //isLive�� �ƴҶ� ����
        GameManager.Instance.health -= Time.deltaTime * 10; 
        if (GameManager.Instance.health <= 0) //�÷��̾ �׾����� 
        {
            for (int index = 2; index < transform.childCount; index++)//�÷��̾� �ڽĿ�����Ʈ �ι�° ���ķ� ��� ������Ʈ ��Ȱ��ȭ
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        } ;
    }
}

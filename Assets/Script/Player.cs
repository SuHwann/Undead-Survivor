using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Vector2 inputVec; //Ű���� �Է°� ����
    public float speed; //�̵� �ӵ�
    public Scanner scaneer; //scanner����
    public Hand[] hands;
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriter;
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        scaneer = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }    
    private void OnMove(InputValue value)
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
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive) { return; } //isLive�� �ƴҶ� ����
        GameManager.Instance.health -= Time.deltaTime * 10;
        if (GameManager.Instance.health <= 0)
        {
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        } ;
    }
}

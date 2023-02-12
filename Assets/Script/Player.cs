using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Vector2 inputVec; //Ű���� �Է°� ����
    public float speed; //�̵� �ӵ�
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter= GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
/*        ���� ���
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");*/
    }
    private void FixedUpdate() //���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec); //��ġ �̵� : ���� rigid ��ġ���� input ���� �����ִ� ����
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    private void LateUpdate() //�������� ����Ǳ� ������ ����Ǵ� �����ֱ� �Լ�
    {
        anim.SetFloat("Speed",inputVec.magnitude);//magitude : ������ inputVec�� ũ��
       if(inputVec.x != 0)//inputVec �� 0�� �ƴҶ� 
       {
            spriter.flipX = inputVec.x < 0; //inputVec�� 0���� ������ spriter.flipX �� ����
       }
    }

}

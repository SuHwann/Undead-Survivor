using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec; //Ű���� �Է°� ����
    public float speed; //�̵� �ӵ�
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate() //���� ���� �����Ӹ��� ȣ��Ǵ� �����ֱ� �Լ�
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + inputVec); //��ġ �̵� : ���� rigid ��ġ���� input ���� �����ִ� ����
    }
}

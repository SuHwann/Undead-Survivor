using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //�̵� �ӵ� ����
    public Rigidbody2D target; //�߰��� Ÿ�� ����

    bool isLive = true; //���� ���� ���� ����

    Rigidbody2D rigid; //������ ����
    SpriteRenderer spriter; //��Ʈ������ ����
    void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
        spriter= GetComponent<SpriteRenderer>();   
    }
    private void FixedUpdate()
    {
        if (!isLive) return; //���Ͱ� �������°� �ƴϸ� return
        Vector2 dirVec = target.position - rigid.position; //��ġ ���� = Ÿ�� ��ġ - ���� ��ġ
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //nextVec : �̵��� ������ ��� ���ؼ� �̵�
        rigid.MovePosition(rigid.position + nextVec); //���� ��ġ + ������ġ 
        rigid.velocity = Vector2.zero; // ������ �浹�� ���� �˹� ���� 
    }
    private void LateUpdate()
    {
        if(!isLive) return; //���Ͱ� �������°� �ƴϸ� return
        spriter.flipX = target.position.x < rigid.position.x; //�÷��̾ �ٶ�
    }
}

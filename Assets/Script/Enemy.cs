using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //�̵� �ӵ� ����
    public float health; //���� ü�� 
    public float maxHealth; // max ü��
    public RuntimeAnimatorController[] animCon; //RuntimeAnimatorController ����
    public Rigidbody2D target; //�߰��� Ÿ�� ����

    bool isLive; //���� ���� ���� ����

    Rigidbody2D rigid; //������ ����
    Animator anim; //�ִϸ����� ���� 
    SpriteRenderer spriter; //��Ʈ������ ����
    void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();    
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
    private void OnEnable() //������Ʈ�� Ȱ��ȭ �Ǹ� �ڵ����� Ÿ���� �÷��̾�� ����
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }
    public void Init(SpawnData data) //�ʱ� �Ӽ��� �����ϴ� �Լ� �߰�
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    private void OnTriggerEnter2D(Collider2D collision) //�浹 �̺�Ʈ �Լ�
    {
        if (!collision.CompareTag("Bullet")) return; //bullet�� �ƴҰ�� �����ϴ� ����
        health -= collision.GetComponent<Bullet>().damage;

        if(health > 0)
        {
            // .. Live , Hit Action
        }
        else
        {
            // .. Die\
            Dead();
        }
        void Dead()
        {
            gameObject.SetActive(false); //����� ���ӿ�����Ʈ ��Ȱ��ȭ
        }
    }
}

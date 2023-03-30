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
    Collider2D coll;  //�ݶ��̴� ���� 
    Animator anim; //�ִϸ����� ���� 
    SpriteRenderer spriter; //��Ʈ������ ����
    WaitForFixedUpdate wait; //�ڷ�ƾ wait ���� 
    void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();    
        spriter= GetComponent<SpriteRenderer>();   
        wait = new WaitForFixedUpdate();
    }
    private void FixedUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return; //���Ͱ� �������°� �ƴϸ� return
        Vector2 dirVec = target.position - rigid.position; //��ġ ���� = Ÿ�� ��ġ - ���� ��ġ
        Vector2 nextVec = dirVec.normalized * (speed * Time.fixedDeltaTime); //nextVec : �̵��� ������ ��� ���ؼ� �̵�
        rigid.MovePosition(rigid.position + nextVec); //���� ��ġ + ������ġ 
        rigid.velocity = Vector2.zero; // ������ �浹�� ���� �˹� ���� 
    }
    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        if(!isLive) return; //���Ͱ� �������°� �ƴϸ� return
        spriter.flipX = target.position.x < rigid.position.x; //�÷��̾ �ٶ�
    }
    private void OnEnable() //������Ʈ�� Ȱ��ȭ �Ǹ� �ڵ����� Ÿ���� �÷��̾�� ����
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2; //spriter renderrer �� Orderin Layer �� 1�� ��ȯ
        anim.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet") || !isLive) return; //bullet�� �ƴҰ�� �����ϴ� ����

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack()); //���ڿ��� ���ϴ� ������ �����ϱ� ���� �ϱ� ����

        if(health > 0)
        {
            // .. Live , Hit Action
            anim.SetTrigger("Hit");
        }
        else
        {
            // .. Die
            isLive = false;
            coll.enabled = false;
            rigid.simulated= false;
            spriter.sortingOrder = 1; //spriter renderrer �� Orderin Layer �� 1�� ��ȯ
            anim.SetBool("Dead", true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
        }
    }
    IEnumerator KnockBack() //�˹� �ڷ�ƾ �Լ�
    {
        yield return wait; //���� �ϳ��� ���� ������ ������ 
        Vector3 playerPos = GameManager.Instance.player.transform.position; //�÷��̾��� ��ġ ������
        Vector3 dirVec = transform.position - playerPos; //�÷��̾� ������ �ݴ� ���� : ���� ��ġ - �÷��̾� ��ġ 
        rigid.AddForce(dirVec.normalized * 3f, ForceMode2D.Impulse); //�÷��̾� ���� �ݴ� �������� �˹� 
    }
    void Dead() //���� ��� �Լ�
    {
        gameObject.SetActive(false); //����� ���ӿ�����Ʈ ��Ȱ��ȭ
    }
}

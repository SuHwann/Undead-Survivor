using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //이동 속도 변수
    public float health; //현재 체력 
    public float maxHealth; // max 체력
    public RuntimeAnimatorController[] animCon; //RuntimeAnimatorController 변수
    public Rigidbody2D target; //추격할 타겟 변수

    bool isLive; //몬스터 생존 여부 변수

    Rigidbody2D rigid; //리지드 변수
    Collider2D coll;  //콜라이더 변수 
    Animator anim; //애니메이터 변수 
    SpriteRenderer spriter; //스트라이프 변수
    WaitForFixedUpdate wait; //코루틴 wait 변수 
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
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return; //몬스터가 생존상태가 아니면 return
        Vector2 dirVec = target.position - rigid.position; //위치 차이 = 타켓 위치 - 나의 위치
        Vector2 nextVec = dirVec.normalized * (speed * Time.fixedDeltaTime); //nextVec : 이동할 방향을 계속 더해서 이동
        rigid.MovePosition(rigid.position + nextVec); //현재 위치 + 다음위치 
        rigid.velocity = Vector2.zero; // 물리적 충돌로 인한 넉백 방지 
    }
    private void LateUpdate()
    {
        if (!GameManager.Instance.isLive) return;
        if(!isLive) return; //몬스터가 생존상태가 아니면 return
        spriter.flipX = target.position.x < rigid.position.x; //플레이어를 바라봄
    }
    private void OnEnable() //오브젝트가 활성화 되면 자동으로 타겟을 플레이어로 지정
    {
        target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2; //spriter renderrer 에 Orderin Layer 를 1로 변환
        anim.SetBool("Dead", false);
        health = maxHealth;
    }
    public void Init(SpawnData data) //초기 속성을 적용하는 함수 추가
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    private void OnTriggerEnter2D(Collider2D collision) //충돌 이벤트 함수
    {
        if (!collision.CompareTag("Bullet") || !isLive) return; //bullet이 아닐경우 리턴하는 필터

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack()); //문자열로 안하는 이유는 추적하기 쉽게 하기 위함

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
            spriter.sortingOrder = 1; //spriter renderrer 에 Orderin Layer 를 1로 변환
            anim.SetBool("Dead", true);
            GameManager.Instance.kill++;
            GameManager.Instance.GetExp();
        }
    }
    IEnumerator KnockBack() //넉백 코루틴 함수
    {
        yield return wait; //다음 하나의 물리 프레임 딜레이 
        Vector3 playerPos = GameManager.Instance.player.transform.position; //플레이어의 위치 가져옴
        Vector3 dirVec = transform.position - playerPos; //플레이어 기준의 반대 방향 : 현재 위치 - 플레이어 위치 
        rigid.AddForce(dirVec.normalized * 3f, ForceMode2D.Impulse); //플레이어 기준 반대 방향으로 넉백 
    }
    void Dead() //몬스터 사망 함수
    {
        gameObject.SetActive(false); //사망시 게임오브젝트 비활성화
    }
}

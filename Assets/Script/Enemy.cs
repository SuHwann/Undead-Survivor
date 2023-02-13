using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed; //이동 속도 변수
    public Rigidbody2D target; //추격할 타겟 변수

    bool isLive = true; //몬스터 생존 여부 변수

    Rigidbody2D rigid; //리지드 변수
    SpriteRenderer spriter; //스트라이프 변수
    void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
        spriter= GetComponent<SpriteRenderer>();   
    }
    private void FixedUpdate()
    {
        if (!isLive) return; //몬스터가 생존상태가 아니면 return
        Vector2 dirVec = target.position - rigid.position; //위치 차이 = 타켓 위치 - 나의 위치
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime; //nextVec : 이동할 방향을 계속 더해서 이동
        rigid.MovePosition(rigid.position + nextVec); //현재 위치 + 다음위치 
        rigid.velocity = Vector2.zero; // 물리적 충돌로 인한 넉백 방지 
    }
    private void LateUpdate()
    {
        if(!isLive) return; //몬스터가 생존상태가 아니면 return
        spriter.flipX = target.position.x < rigid.position.x; //플레이어를 바라봄
    }
}

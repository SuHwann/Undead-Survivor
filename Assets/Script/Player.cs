using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public Vector2 inputVec; //키보드 입력값 변수
    public float speed; //이동 속도
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
/*        옛날 방식
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");*/
    }
    private void FixedUpdate() //물리 연산 프레임마다 호출되는 생명주기 함수
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec); //위치 이동 : 현재 rigid 위치에서 input 값을 더해주는 개념
    }
    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    private void LateUpdate() //프레임이 종료되기 직전에 실행되는 생명주기 함수
    {
        anim.SetFloat("Speed",inputVec.magnitude);//magitude : 순수한 inputVec의 크기
       if(inputVec.x != 0)//inputVec 가 0이 아닐때 
       {
            spriter.flipX = inputVec.x < 0; //inputVec가 0보다 작으면 spriter.flipX 에 들어간다
       }
    }

}

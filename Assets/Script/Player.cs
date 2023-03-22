using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public Vector2 inputVec; //키보드 입력값 변수
    public float speed; //이동 속도
    public Scanner scaneer; //scanner변수
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
    }
    private void FixedUpdate() //물리 연산 프레임마다 호출되는 생명주기 함수
    {
        Vector2 nextVec = inputVec * (speed * Time.fixedDeltaTime);
        rigid.MovePosition(rigid.position + nextVec); //위치 이동 : 현재 rigid 위치에서 input 값을 더해주는 개념
    }
    private void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }
    private void LateUpdate() //프레임이 종료되기 직전에 실행되는 생명주기 함수
    {
        anim.SetFloat(Speed,inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
/*플레이어 이동, 충돌 기능 */
public class Player : MonoBehaviour
{
    public Vector2 inputVec; //키보드 입력값 변수
    public float speed; //이동 속도
    public Scanner scaneer; //scanner.cs변수
    public Hand[] hands; //Hand.cs 변수 (플레이어 무기)
    public RuntimeAnimatorController[] animCon; //플레이어 스크립트에 여러 애니메이터 컨트롤러를 저장할 배열 변수 선언
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer spriter;
    private static readonly int Speed = Animator.StringToHash("Speed");//Hash로 미리 캐싱

    private void Awake() //초기화
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        scaneer = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }

    private void OnEnable()//활성화 될때 게임매니저의 id에 따라 캐릭터 애니메이션 실행
    {
        speed *= Character.Speed; //캐릭터 선택시 캐릭터에 따른 스피드를 곱한다.
        anim.runtimeAnimatorController = animCon[GameManager.Instance.playerId];
    }

    private void OnMove(InputValue value) //Input System에서 value를 받아서 inputVec에 값 저장 
    {
        if (!GameManager.Instance.isLive)
            return;
        inputVec = value.Get<Vector2>();
    }
    private void FixedUpdate() //물리 연산 프레임마다 호출되는 생명주기 함수
    {
        Vector2 nextVec = inputVec * (speed * Time.fixedDeltaTime);
        rigid.MovePosition(rigid.position + nextVec); //위치 이동 : 현재 rigid 위치에서 input 값을 더해주는 개념
    }
    private void LateUpdate() //프레임이 종료되기 직전에 실행되는 생명주기 함수
    {
        if (!GameManager.Instance.isLive)
            return;
        anim.SetFloat(Speed,inputVec.magnitude);
        if (inputVec.x != 0) {
            spriter.flipX = inputVec.x < 0; //inputVec.x 가 0보다 작으면 flipx는 true가 된다 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive) { return; } //isLive가 아닐땐 리턴
        GameManager.Instance.health -= Time.deltaTime * 10; 
        if (GameManager.Instance.health <= 0) //플레이어가 죽었을때 
        {
            for (int index = 2; index < transform.childCount; index++)//플레이어 자식오브젝트 두번째 이후로 모든 오브젝트 비활성화
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        } ;
    }
}

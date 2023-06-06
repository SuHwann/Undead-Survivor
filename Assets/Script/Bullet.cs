using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; //데미지
    public int per;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage , int per , Vector3 dir) //damage , per 초기화 함수
    {
        this.damage = damage;
        this.per = per;

        if(per >= 0) //관통이 -1(무한) 보다 큰 것에 대해서는 속도 적용
        {
            rigid.velocity = dir * 15f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100) return;

        per--;
        if(per < 0)//관통 값이 하나씩 줄어들면서 -1이 되면 비활성화 
        {
            rigid.velocity = Vector2.zero; //물리 속도 0
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100)
            return;
        gameObject.SetActive(false);
    }
}

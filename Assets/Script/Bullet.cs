using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage; //������
    public int per;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage , int per , Vector3 dir) //damage , per �ʱ�ȭ �Լ�
    {
        this.damage = damage;
        this.per = per;

        if(per >= 0) //������ -1(����) ���� ū �Ϳ� ���ؼ��� �ӵ� ����
        {
            rigid.velocity = dir * 15f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -100) return;

        per--;
        if(per < 0)//���� ���� �ϳ��� �پ��鼭 -1�� �Ǹ� ��Ȱ��ȭ 
        {
            rigid.velocity = Vector2.zero; //���� �ӵ� 0
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

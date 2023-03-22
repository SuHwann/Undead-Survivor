using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D coll;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))return; //�浹���� ����� Area�� �ƴϸ� ���� 
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffx = Mathf.Abs(playerPos.x - myPos.x); //�÷��̾� ��ġ - Ÿ�ϸ� ��ġ ������� �Ÿ� ���ϱ� 
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag) 
        {
            case "Ground":
                if(diffx > diffy)
                {
                    transform.Translate(Vector3.right * dirX * 40); //Translate : ������ �� ��ŭ ���� ��ġ���� �̵� 
                }
                else if( diffx < diffy) {
                    transform.Translate(Vector3.up * dirY * 40); //diffy�� diffx���� ũ�� ���� 40 ��ŭ �̵�
                }
                break;
            case "Enemy":
                if(coll.enabled)
                {
                    transform.Translate(playerDir * 20 + new Vector3(Random.Range(-3f,3f),Random.Range(-3f,3f),0f)); //�÷��̾��� �̵� ���⿡ ���� ���� ���� ������ ��ġ���� �����ϵ��� �̵� 
                }
                break;

        }
    }
}

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
       

        switch (transform.tag) 
        {
            case "Ground":
                float diffx = playerPos.x - myPos.x; //�÷��̾� ��ġ - Ÿ�ϸ� ��ġ ������� �Ÿ� ���ϱ� 
                float diffy = playerPos.y - myPos.y;
                float dirX = diffx < 0 ? -1 : 1;
                float dirY = diffy < 0 ? -1 : 1;
                diffx = Mathf.Abs(diffx);
                diffy = Mathf.Abs(diffy);

                if (diffx > diffy)
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
                    Vector3 dist = playerPos - myPos;
                    Vector3 ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3),0);
                    transform.Translate(ran + dist * 2); //�÷��̾��� �̵� ���⿡ ���� ���� ���� ������ ��ġ���� �����ϵ��� �̵� 
                }
                break;

        }
    }
}

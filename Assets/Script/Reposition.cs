using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 myPos = transform.position;
        float diffx = Mathf.Abs(playerPos.x - myPos.x); //플레이어 위치 - 타일맵 위치 계산으로 거리 구하기 
        float diffy = Mathf.Abs(playerPos.y - myPos.y);

        Vector3 playerDir = GameManager.Instance.player.inputVec;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if(diffx > diffy)
                {
                    transform.Translate(Vector3.right * dirX * 40); //Translate : 지정된 값 만큼 현재 위치에서 이동 
                }
                else if( diffx < diffy) {
                    transform.Translate(Vector3.up * dirY * 40); //diffy가 diffx보다 크면 위로 40 만큼 이동
                }
                break;
            case "Enemy":
                break;

        }
    }
}

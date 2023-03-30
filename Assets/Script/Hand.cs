using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft; //왼손인지 아닌지 구별하기위한 bool값 변수
    public SpriteRenderer spriter;

    private SpriteRenderer player;
    
    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    private Quaternion leftRot = Quaternion.Euler(0,0,-35);
    private Quaternion leftRotReverse = Quaternion.Euler(0,0,-135);
    void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    void LateUpdate()
    {
        bool isReverse = player.flipX;
        if (isLeft)
        { //근전무기
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else{ //원거리 무기
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;

        }
    }
}

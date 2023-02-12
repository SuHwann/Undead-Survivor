using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player; //플레이어 타입 공개 변수 선언

    private void Awake()
    {
        Instance = this; //Awake 생명주기에서 인스턴스 변수를 자기자신 this로 초기화
    }
}

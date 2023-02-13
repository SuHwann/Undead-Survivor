using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime; //게임시간 변수
    public float maxGameTime = 2 * 10f; // 최대게임시간을 담당할 변수 선언

    public PoolManager pool;
    public Player player; //플레이어 타입 공개 변수 선언
    private void Awake()
    {
        Instance = this; //Awake 생명주기에서 인스턴스 변수를 자기자신 this로 초기화
    }
    private void Update()
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}

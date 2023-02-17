using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("#Game Control")] 
    public float gameTime; //게임시간 변수
    public float maxGameTime = 2 * 10f; // 최대게임시간을 담당할 변수 선언
    [Header("#Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3,5,10,100,150,210,280,360,450,600 };//각 레벨의 필요경험치를 보관할 배열 변수 선언 및 초기화 
    [Header("#Game Object")]
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
    public void GetExp() //경험치 함수 
    {
        exp++;
        
        if(exp == nextExp[level]) {
            level++; //레벨업
            exp = 0; //경험치 초기화 

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Mono.CompilerServices.SymbolWriter;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("#Game Control")] public bool isLive;
    public float gameTime; //게임시간 변수
    public float maxGameTime = 2 * 10f; // 최대게임시간을 담당할 변수 선언
    [Header("#Player Info")] 
    public int playerId; //캐릭터ID를 저장할 변수선언
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3,5,10,100,150,210,280,360,450,600 };//각 레벨의 필요경험치를 보관할 배열 변수 선언 및 초기화 
    [Header("#Game Object")]
    public PoolManager pool;
    public Player player; //플레이어 타입 공개 변수 선언
    public LevelUp uiLevelUp; //게임매니저 레벨업 변수 선언
    public Result uiResult;
    public GameObject enemyCleaner;
    private void Awake()
    {
        Instance = this; //Awake 생명주기에서 인스턴스 변수를 자기자신 this로 초기화
    }
    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth; 
        player.gameObject.SetActive(true);//게임 시작할 때 플레이어 활성화 후 기본 무기 지급
        uiLevelUp.Select(playerId % 2);//임시 스크립트 (첫번째 캐릭터 선택)
        Resume();
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }
    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }
    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();//게임이 이기면 Victory함수 실행
        }
    }
    public void GetExp() //경험치 함수 
    {
        if (!isLive) return;
        exp++;
        
        if(exp == nextExp[Mathf.Min(level,nextExp.Length-1)]) {
            level++; //레벨업
            exp = 0; //경험치 초기화 
            uiLevelUp.Show();
        }
    }

    public void Stop() //게임 정지
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume() //게임 리스타트
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

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
    public float gameTime; //���ӽð� ����
    public float maxGameTime = 2 * 10f; // �ִ���ӽð��� ����� ���� ����
    [Header("#Player Info")] 
    public int playerId; //ĳ����ID�� ������ ��������
    public float health;
    public float maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3,5,10,100,150,210,280,360,450,600 };//�� ������ �ʿ����ġ�� ������ �迭 ���� ���� �� �ʱ�ȭ 
    [Header("#Game Object")]
    public PoolManager pool;
    public Player player; //�÷��̾� Ÿ�� ���� ���� ����
    public LevelUp uiLevelUp; //���ӸŴ��� ������ ���� ����
    public Result uiResult;
    public GameObject enemyCleaner;
    private void Awake()
    {
        Instance = this; //Awake �����ֱ⿡�� �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
    }
    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth; 
        player.gameObject.SetActive(true);//���� ������ �� �÷��̾� Ȱ��ȭ �� �⺻ ���� ����
        uiLevelUp.Select(playerId % 2);//�ӽ� ��ũ��Ʈ (ù��° ĳ���� ����)
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
            GameVictory();//������ �̱�� Victory�Լ� ����
        }
    }
    public void GetExp() //����ġ �Լ� 
    {
        if (!isLive) return;
        exp++;
        
        if(exp == nextExp[Mathf.Min(level,nextExp.Length-1)]) {
            level++; //������
            exp = 0; //����ġ �ʱ�ȭ 
            uiLevelUp.Show();
        }
    }

    public void Stop() //���� ����
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume() //���� ����ŸƮ
    {
        isLive = true;
        Time.timeScale = 1;
    }
}

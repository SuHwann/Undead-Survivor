using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("#Game Control")] public bool isLive;
    public float gameTime; //���ӽð� ����
    public float maxGameTime = 2 * 10f; // �ִ���ӽð��� ����� ���� ����
    [Header("#Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3,5,10,100,150,210,280,360,450,600 };//�� ������ �ʿ����ġ�� ������ �迭 ���� ���� �� �ʱ�ȭ 
    [Header("#Game Object")]
    public PoolManager pool;
    public Player player; //�÷��̾� Ÿ�� ���� ���� ����
    public LevelUp uiLevelUp; //���ӸŴ��� ������ ���� ����
    private void Awake()
    {
        Instance = this; //Awake �����ֱ⿡�� �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
    }
    private void Start()
    {
        health = maxHealth; 
        //�ӽ� ��ũ��Ʈ (ù��° ĳ���� ����)
        uiLevelUp.Select(0);
    }
    private void Update()
    {
        if (!isLive)
            return;
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
    public void GetExp() //����ġ �Լ� 
    {
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

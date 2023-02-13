using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float gameTime; //���ӽð� ����
    public float maxGameTime = 2 * 10f; // �ִ���ӽð��� ����� ���� ����

    public PoolManager pool;
    public Player player; //�÷��̾� Ÿ�� ���� ���� ����
    private void Awake()
    {
        Instance = this; //Awake �����ֱ⿡�� �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
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

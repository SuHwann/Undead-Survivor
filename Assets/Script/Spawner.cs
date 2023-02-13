using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    int level; //��ȯ ��ũ��Ʈ���� ���� ��� ���� 
    float timer; //���� ��ȯ Ÿ�̸�
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Instance.gameTime / 10f),spawnData.Length -1);

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.Instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}
[System.Serializable]
public class SpawnData 
{
    public float spawnTime; // ��ȯ�ð�
    public int spriteType; //��������Ʈ Ÿ�� 
    public int health; // ü��
    public float speed; //�ӵ� 
}


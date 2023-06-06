using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. ��������� ������ ����
    public GameObject[] prefabs;

    // .. ǰ ����� �ϴ� ����Ʈ ��
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; //pools ����Ʈ �ʱ�ȭ

        for(int index = 0; index < pools.Length; index++) //pools ����Ʈ ��� �ʱ�ȭ 
        {
            pools[index] = new List<GameObject>();
        }
    }
    public GameObject Get(int index)
    {
        GameObject select = null;
        // ... ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) ���ӿ�����Ʈ ���� ���� 
        foreach(GameObject item in pools[index]) // �迭 ����Ʈ�� �����͸� ���������� �����ϴ� �ݺ���
        {
            if(!item.activeSelf) //���빰 ������Ʈ�� ��Ȱ��ȭ���� Ȯ�� 
            {
                // ... �߰��ϸ�  select ������ �Ҵ�
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... �� ã������?
        if (!select)
        {
            // ...���Ӱ� �����ϰ�  select ������ �Ҵ�
            select = Instantiate(prefabs[index],transform);
            pools[index].Add(select);
        }
        return select;
    }
}

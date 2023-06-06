using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리펩들을 보관할 변수
    public GameObject[] prefabs;

    // .. 품 담당을 하는 리스트 들
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length]; //pools 리스트 초기화

        for(int index = 0; index < pools.Length; index++) //pools 리스트 요소 초기화 
        {
            pools[index] = new List<GameObject>();
        }
    }
    public GameObject Get(int index)
    {
        GameObject select = null;
        // ... 선택한 풀의 놀고 있는(비활성화 된) 게임오브젝트 선택 접근 
        foreach(GameObject item in pools[index]) // 배열 리스트의 데이터를 순차적으로 접근하는 반복문
        {
            if(!item.activeSelf) //내용물 오브젝트가 비활성화인지 확인 
            {
                // ... 발견하면  select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }
        // ... 못 찾았으면?
        if (!select)
        {
            // ...새롭게 생성하고  select 변수에 할당
            select = Instantiate(prefabs[index],transform);
            pools[index].Add(select);
        }
        return select;
    }
}

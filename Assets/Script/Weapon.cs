using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //���� ID , ������ ID , ������ , ���� , �ӵ� ����
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;
    private void Update()
    {
        
    }
    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = -150;
                Batch();
                break;
            default:
                break;
        }
    }
    void Batch()
    {
        for(int index =0; index < count; index++) {
            Transform bullet = GameManager.Instance.pool.Get(prefabID).transform;
            bullet.parent = transform; //bullet �θ� �Ӽ� ����

        }
    }
}

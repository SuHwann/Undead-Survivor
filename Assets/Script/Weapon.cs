using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Weapon : MonoBehaviour
{
    //무기 ID , 프리펩 ID , 데미지 , 개수 , 속도 변수
    public int id;
    public int prefabID;
    public float damage;
    public int count;
    public float speed;

    float timer; // bullet 타이머
    Player player;
    private void Awake()
    {
        player = GameManager.Instance.player; //부모 오브젝트 player.cs 가져오기 
    }
    private void Update()
    {
        if (!GameManager.Instance.isLive)
            return;
        switch(id)
        {
            case 0: //무기 회전 
                transform.Rotate(Vector3.back * (speed * Time.deltaTime));
                break;
            default:
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
        // .. Test Code
        if( Input.GetButtonDown("Jump"))
        {
            LevelUp(10, 1);
        }
    }
    public void LevelUp(float damage , int count)//레벨업 함수 
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if(id == 0) {Batch();}
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);

    }
    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        //Property Set
        id = data.itemId;
        damage = data.baseDamage * Character.Damage;
        count = data.baseCount+ Character.Count;

        for (int index = 0; index < GameManager.Instance.pool.prefabs.Length; index++)
        {
            if (data.projectile == GameManager.Instance.pool.prefabs[index])
            {
                prefabID = index;
                break;
            }
        }
        switch(id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;
            default:
                speed = 0.3f * Character.WeaponRate;
                break;
        }
        
        //Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }
    void Batch() //플레이어 무기 배치
    {
        for(int index =0; index < count; index++) {
            Transform bullet;
            if(index < transform.childCount) //기존 오브젝트를 먼저 활용하고 ,
            {
                bullet = transform.GetChild(index);
            }
            else // 모자란 것은 오브젝트 풀링에서 가져온다
            {
                bullet = GameManager.Instance.pool.Get(prefabID).transform;
                bullet.parent = transform;
            }
            bullet.parent = transform; //bullet 부모 자기자신으로 속성 변경

            bullet.localPosition = Vector3.zero; //위치 초기화 
            bullet.localRotation = Quaternion.identity; //회전값 초기화 

            Vector3 rotVec = Vector3.forward * (360 * index) / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f,Space.World); //space world 기준
            bullet.GetComponent<Bullet>().Init(damage , -1 , Vector3.zero); // -1 is Infinity Per.(-1은 무한으로 관통하는 무한 근접 공격)
        }
    }
    void Fire() //총알 생성 및 발사
    {
        if (!player.scaneer.nearestTarget) return;

        Vector3 targetPos = player.scaneer.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.Instance.pool.Get(prefabID).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up , dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);
    }
}

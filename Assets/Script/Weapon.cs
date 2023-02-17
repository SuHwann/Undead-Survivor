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

    float timer; // bullet Ÿ�̸�
    Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>(); //�θ� ������Ʈ player.cs �������� 
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        switch(id)
        {
            case 0: //���� ȸ�� 
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
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
    public void LevelUp(float damage , int count)//������ �Լ� 
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
        {
            Batch();
        }
    }
    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                speed = 0.3f;
                break;
        }
    }
    void Batch() //�÷��̾� ���� ��ġ
    {
        for(int index =0; index < count; index++) {
            Transform bullet;
            if(index < transform.childCount) //���� ������Ʈ�� ���� Ȱ���ϰ� ,
            {
                bullet = transform.GetChild(index);
            }
            else // ���ڶ� ���� ������Ʈ Ǯ������ �����´�
            {
                bullet = GameManager.Instance.pool.Get(prefabID).transform;
                bullet.parent = transform;
            }
            bullet.parent = transform; //bullet �θ� �ڱ��ڽ����� �Ӽ� ����

            bullet.localPosition = Vector3.zero; //��ġ �ʱ�ȭ 
            bullet.localRotation = Quaternion.identity; //ȸ���� �ʱ�ȭ 

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f,Space.World); //space world ����
            bullet.GetComponent<Bullet>().Init(damage , -1 , Vector3.zero); // -1 is Infinity Per.(-1�� �������� �����ϴ� ���� ���� ����)
        }
    }
    void Fire() //�Ѿ� ���� �� �߻�
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

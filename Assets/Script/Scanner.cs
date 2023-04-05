using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*���� ����� ���� Ÿ���� result �� �����Ͽ� ŸŶ�� �ڱ� �ڽ��� �Ÿ��� ������ִ� ��ũ��Ʈ*/
public class Scanner : MonoBehaviour
{
    public float scanRange; //���� 
    public LayerMask targetLayer; //���̾�
    public RaycastHit2D[] targets; //��ĵ ��� �迭 
    public Transform nearestTarget; // ���� ����� Ÿ�� ����

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0,targetLayer);
        nearestTarget = GetNearest(); //�ϼ��� �Լ��� ���� ���������� ���� ����� ��ǥ ������ ������Ʈ
    }
    Transform GetNearest() //���� ����� target �� result�� ���� 
    {
        Transform result = null;
        float diff = 100; //�Ÿ� 
        foreach(RaycastHit2D target in targets)//foreach ������ ĳ���� ��� ������Ʈ�� �ϳ��� ���� GetNearest()
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos); //�ڱ� �ڽŰ� target�� �Ÿ��� �����ش�
            if(curDiff < diff)// �ݺ����� ���� ������ �Ÿ��� ����� �Ÿ����� ������ ��ü
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
}

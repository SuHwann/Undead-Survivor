using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*가장 가까운 몬스터 타겟을 result 에 저장하여 타킷과 자기 자신의 거리를 계산해주는 스크립트*/
public class Scanner : MonoBehaviour
{
    public float scanRange; //범위 
    public LayerMask targetLayer; //레이어
    public RaycastHit2D[] targets; //스캔 결과 배열 
    public Transform nearestTarget; // 가장 가까운 타겟 변수

    private void FixedUpdate()
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0,targetLayer);
        nearestTarget = GetNearest(); //완성된 함수를 통해 지속적으로 가장 가까운 목표 변수를 업데이트
    }
    Transform GetNearest() //가장 가까운 target 을 result에 저장 
    {
        Transform result = null;
        float diff = 100; //거리 
        foreach(RaycastHit2D target in targets)//foreach 문으로 캐스팅 결과 오브젝트를 하나씩 접근 GetNearest()
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos); //자기 자신과 target의 거리를 구해준다
            if(curDiff < diff)// 반복문을 돌며 가져온 거리가 저장된 거리보다 작으면 교체
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
}

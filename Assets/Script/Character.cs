using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Speed //이동속도
    {
        get { return GameManager.Instance.playerId == 0 ? 1.1f : 1f; }
    }

    public static float WeaponSpeed //공격속도 
    {
        get { return GameManager.Instance.playerId == 1 ? 1.1f : 1f; }
    }
    public static float WeaponRate //공격범위
    {
        get { return GameManager.Instance.playerId == 1 ? 0.9f : 1f; }
    }
    public static float Damage //공격력
    {
        get { return GameManager.Instance.playerId == 2 ? 1.2f : 1f; }
    }
    public static int Count //무기 갯수
    {
        get { return GameManager.Instance.playerId == 3 ? 1 : 0; }
    }
}

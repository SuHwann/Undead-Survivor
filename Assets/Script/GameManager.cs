using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Player player; //�÷��̾� Ÿ�� ���� ���� ����

    private void Awake()
    {
        Instance = this; //Awake �����ֱ⿡�� �ν��Ͻ� ������ �ڱ��ڽ� this�� �ʱ�ȭ
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ��ũ��Ʈ�� ������Ʈ�� ��� �޾Ƽ� �پ��� �����͸� �����ϴ� ��� 
[CreateAssetMenu(fileName = "Item",menuName = "Scriptble Object/ItemData")] //Ŀ���� �޴��� �����ϴ� �Ӽ� 
public class ItemData : ScriptableObject //��ũ��Ʈ ��� ������Ʈ
{
    public enum ItemType{Melee, Range, Glove , Shoe , Heal}
    [Header("# Main Info")] 
    public ItemType itemType; //������ Ÿ�Ժ���
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    [Header("# Level Data")]
    public float baseDamage; //0���� ������
    public int baseCount;//���� Ƚ���� ������ ����
    public float[] damages;
    public int[] counts;
    [Header("# Weapon")] 
    public GameObject projectile;
}

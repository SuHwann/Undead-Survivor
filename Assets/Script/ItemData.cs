using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 스크립트블 오브젝트를 상속 받아서 다양한 데이터를 저장하는 기능 
[CreateAssetMenu(fileName = "Item",menuName = "Scriptble Object/ItemData")] //커스텀 메뉴를 생성하는 속성 
public class ItemData : ScriptableObject //스크립트 기반 오브젝트
{
    public enum ItemType{Melee, Range, Glove , Shoe , Heal}
    [Header("# Main Info")] 
    public ItemType itemType; //아이템 타입변수
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    [Header("# Level Data")]
    public float baseDamage; //0레벨 데미지
    public int baseCount;//관통 횟수를 저장할 변수
    public float[] damages;
    public int[] counts;
    [Header("# Weapon")] 
    public GameObject projectile;
}

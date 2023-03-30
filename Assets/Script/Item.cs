using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Item : MonoBehaviour
{
    public ItemData data; //아이템 관리에 필요한 변수
    public int level;
    public Weapon weapon;
    public Gear gear;
    
    private Image icon;
    private Text textLevel;
    private Text textName;
    private Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        textName = texts[1];
        textDesc = texts[2];
        textName.text = data.itemName;
    }
    void OnEnable()
    {
        textLevel.text = "Lv." + (level + 1);

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break; 
            default:
                textDesc.text = string.Format(data.itemDesc);
                break;
        }
    }

    public void OnClick()
    {
        switch (data.itemType)
        {
           case ItemData.ItemType.Melee:
           case ItemData.ItemType.Range:
               if (level == 0) {
                   GameObject newWeapon = new GameObject(); //새로운 게임 오브젝트 생성
                   weapon = newWeapon.AddComponent<Weapon>();
                   weapon.Init(data); //weapon 초기화 함수에 스크립트블 오브젝트를 매개변수로 받아 활용
               }
               else
               {
                   float nextdamage = data.baseDamage;
                   int nextCount = 0;

                   nextdamage += data.baseDamage * data.damages[level]; //처음 이후의 레벨업은 데미지와 횟수를 계산
                   nextCount += data.counts[level];
                   
                   weapon.LevelUp(nextdamage,nextCount);
               } 
               level++;
               break;
           case ItemData.ItemType.Glove:
           case ItemData.ItemType.Shoe:
               if (level == 0)
               {
                   GameObject newGear = new GameObject(); 
                   gear = newGear.AddComponent<Gear>();
                   gear.Init(data); 
               }
               else
               {
                   float nextRate = data.damages[level];
                   gear.LevelUp(nextRate);
               }
               level++;
               break;
           case ItemData.ItemType.Heal:
               GameManager.Instance.health = GameManager.Instance.maxHealth;
               break;
        }
        if (level == data.damages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

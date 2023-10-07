using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemData data;
    public Weapon weapon;
    public Gear gear;
    public int level;

    Image icon;
    Text textLvel;
    Text textName;
    Text textDesc;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];//0은 자기 자신임
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLvel = texts[0];
        textName = texts[1];
        textDesc = texts[2];

        textName.text = data.itemName;
    }

    private void OnEnable()//활성화 시 호출되는 함수
    {
        textLvel.text = "Lv." + (level + 1);//1레벨 부터 시작.

        switch (data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoes:
                textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                break;
            case ItemData.ItemType.Heal:
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
                if (level == 0)
                {
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    float nextDamage = data.baseDamage;
                    int nextCount = 0;

                    nextDamage += data.baseDamage * data.damages[level];
                    nextCount += data.counts[level];

                    weapon.LevelUp(nextDamage, nextCount);
                }
                level++;

                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoes:
                if (level == 0)
                {
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    float nextRate = data.damages[level];
                    gear.LevelUP(nextRate);
                }
                level++;

                break;
            case ItemData.ItemType.Heal:
                GameManager.Ins.heatlh = GameManager.Ins.maxHealth;
                break;
        }

        if (level == data.damages.Length)//최대 레벨 도달.
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

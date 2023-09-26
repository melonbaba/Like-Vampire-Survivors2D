using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public ItemData data;
    public Weapon weapon;
    public int level;

    Image icon;
    Text textLvel;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];//0은 자기 자신임
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLvel = texts[0];
    }

    private void LateUpdate()
    {
        textLvel.text = "Lv." + (level + 1);//1레벨 부터 시작.
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
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoes:

                break;
            case ItemData.ItemType.Heal:
                break;
        }
        level++;

        if(level == data.damages.Length)//최대 레벨 도달.
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

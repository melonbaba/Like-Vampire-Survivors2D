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
        icon = GetComponentsInChildren<Image>()[1];//0�� �ڱ� �ڽ���
        icon.sprite = data.itemIcon;

        Text[] texts = GetComponentsInChildren<Text>();
        textLvel = texts[0];
    }

    private void LateUpdate()
    {
        textLvel.text = "Lv." + (level + 1);//1���� ���� ����.
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

        if(level == data.damages.Length)//�ִ� ���� ����.
        {
            GetComponent<Button>().interactable = false;
        }
    }
}

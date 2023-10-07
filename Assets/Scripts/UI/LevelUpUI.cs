using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    RectTransform rect;

    ItemUI[] items;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<ItemUI>(true);
    }

    public void Show()
    {
        CtrlItems();
        rect.localScale = Vector3.one;
        GameManager.Ins.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.Ins.Resume();
    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void CtrlItems()//TODO : 만렙의 경우 소비아이템으로 대체 될 때 + 소비아이템 나오는경우 => 아이템 2개만 등장. 개선하기
    {
        foreach (ItemUI item in items)
        {
            item.gameObject.SetActive(false);
        }

        int[] rand = new int[3];
        while (true)
        {
            rand[0] = Random.Range(0, items.Length);
            rand[1] = Random.Range(0, items.Length);
            rand[2] = Random.Range(0, items.Length);

            if (rand[0] != rand[1] && rand[1] != rand[2] && rand[2] != rand[0])
                break;
        }

        for (int i = 0; i < rand.Length; i++)
        {
            ItemUI randItem = items[rand[i]];

            if(randItem.level == randItem.data.damages.Length)//만렙 아이템의 경우, 소비 아이템으로 대체
            {
                items[4].gameObject.SetActive(true);
            }
            else
            {
                randItem.gameObject.SetActive(true);
            }
        }
        
    }

}

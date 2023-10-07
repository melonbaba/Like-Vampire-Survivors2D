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

    void CtrlItems()//TODO : ������ ��� �Һ���������� ��ü �� �� + �Һ������ �����°�� => ������ 2���� ����. �����ϱ�
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

            if(randItem.level == randItem.data.damages.Length)//���� �������� ���, �Һ� ���������� ��ü
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

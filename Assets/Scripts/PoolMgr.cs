using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;//List �迭, pools�� GameObject���� ������ �ִ� �뵵.

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];//�迭�� �ʱ�ȭ�� ����

        for (int i = 0; i < pools.Length; i++)//�迭 ���� List �ʱ�ȭ
        {
            pools[i] = new List<GameObject>();
        }

    }

    public GameObject GetObj(int index)
    {
        GameObject select = null;

        foreach (GameObject obj in pools[index])//foreach : �迭, ����Ʈ�� �����͸� ���������� �����ϴ� �ݺ���
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        if (!select)//selct�� data�� ���� => select = null
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

}

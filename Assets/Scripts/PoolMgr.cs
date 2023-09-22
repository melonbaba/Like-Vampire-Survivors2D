using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMgr : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;//List 배열, pools는 GameObject들을 가지고 있는 용도.

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];//배열만 초기화한 상태

        for (int i = 0; i < pools.Length; i++)//배열 안의 List 초기화
        {
            pools[i] = new List<GameObject>();
        }

    }

    public GameObject GetObj(int index)
    {
        GameObject select = null;

        foreach (GameObject obj in pools[index])//foreach : 배열, 리스트의 데이터를 순차적으로 접근하는 반복문
        {
            if (!obj.activeSelf)
            {
                select = obj;
                select.SetActive(true);
                break;
            }
        }

        if (!select)//selct에 data가 없음 => select = null
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;//�����޸𸮿� �÷� ����ϱ� ����.
                                    //static���� ����� ������ Inspector�� ��Ÿ���� ����.

    public Player player;

    private void Awake()
    {
        if(Ins == null)
            Ins = this;
    }

}

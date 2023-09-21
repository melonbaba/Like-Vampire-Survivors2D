using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;//정적메모리에 올려 사용하기 위함.
                                    //static으로 선언된 변수는 Inspector에 나타나지 않음.

    public Player player;

    private void Awake()
    {
        if(Ins == null)
            Ins = this;
    }

}

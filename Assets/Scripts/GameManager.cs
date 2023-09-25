using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;//정적메모리에 올려 사용하기 위함.
                                    //static으로 선언된 변수는 Inspector에 나타나지 않음.

    [Header("# GameObject")]
    public Player player;
    public PoolMgr poolMgr;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int heatlh;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextexp = { 3, 5, 10, 100, 150, 210, 280, 360, 450, 600 };

    private void Awake()
    {
        if(Ins == null)
            Ins = this;
    }

    private void Start()
    {
        heatlh = maxHealth;
    }
    private void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;
        if(exp == nextexp[level])
        {
            level++;
            exp = 0;
        }
    }

}

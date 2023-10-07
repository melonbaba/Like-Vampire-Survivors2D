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
    public LevelUpUI uiLevelUp;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public bool isFlowTime;

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

        uiLevelUp.Select(0);//초기 Player에게 무기를 제공

    }
    private void Update()
    {
        if (!isFlowTime)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;
        if(exp == nextexp[Mathf.Min(level, nextexp.Length - 1)])//설정된 경험치 초과 방지
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isFlowTime = false;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isFlowTime = true;
        Time.timeScale = 1f;
    }
}

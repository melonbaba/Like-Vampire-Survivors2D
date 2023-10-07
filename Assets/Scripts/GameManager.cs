using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Ins;//�����޸𸮿� �÷� ����ϱ� ����.
                                    //static���� ����� ������ Inspector�� ��Ÿ���� ����.

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

        uiLevelUp.Select(0);//�ʱ� Player���� ���⸦ ����

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
        if(exp == nextexp[Mathf.Min(level, nextexp.Length - 1)])//������ ����ġ �ʰ� ����
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

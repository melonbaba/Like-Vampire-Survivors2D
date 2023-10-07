using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    float timer;
    int level;
    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if (!GameManager.Ins.isFlowTime)
            return;

        timer += Time.deltaTime;//deltaTime = 하나의 프레임이 소비한 시간
        level = Mathf.Min(Mathf.FloorToInt(GameManager.Ins.gameTime / 10f), spawnData.Length - 1);//Floor = 버림.

        if (timer > spawnData[level].spawnTime)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject enemy = GameManager.Ins.poolMgr.GetObj(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
